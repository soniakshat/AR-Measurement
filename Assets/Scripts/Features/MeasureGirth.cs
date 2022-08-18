using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class MeasureGirth : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TextMeshProUGUI girthText;
    public GameObject GirthPopup;



    private string _currentUnit;
    private float unitConverter = 1;
    public ARSession arSession;
    private void ResetArSession()
    {
        arSession.Reset();
    }

    private void Start()
    {
        ResetArSession();
        if (GirthPopup.activeSelf)
            GirthPopup.SetActive(false);

        placementInteractable.objectPlaced.AddListener(DrawLine);

        _currentUnit = PlayerPrefs.GetString("MeasurementUnit");

        unitConverter = _currentUnit switch
        {
            "m" => 1f,
            "cm" => 100,
            "in" => 39.37f,
            "ft" => 3.2808f,
            _ => 1f
        };
    }

    private void DrawLine(ARObjectPlacementEventArgs args)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);
        if (lineRenderer.positionCount <= 2) return;
        GetGirth();
        lineRenderer.positionCount = 0;
    }

    public void GetGirth()
    {
        girthText.text = "";
        int totalPoints = lineRenderer.positionCount;
        if (totalPoints > 1)
        {
            float dist = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1)) * unitConverter;
            print($"Distance between Point-A and Point-B is {dist} {_currentUnit}");
            girthText.text += $"\nDistance between Point-A and Point-B is {dist} {_currentUnit}";
        }
        GirthPopup.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(AppConstants.MAIN_MENU);
        SceneManager.UnloadSceneAsync(AppConstants.MEASURE_GIRTH);
    }
}
