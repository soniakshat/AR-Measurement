using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class MeasureHeight : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TextMeshProUGUI heightText;
    public GameObject HeightPopup;



    private string _currentUnit;
    private float unitConverter = 1;



    void Start()
    {
        if (HeightPopup.activeSelf)
            HeightPopup.SetActive(false);

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
        if (lineRenderer.positionCount == 2)
        {
            lineRenderer.SetPosition(1, lineRenderer.GetPosition(0) + new Vector3(lineRenderer.GetPosition(0).x, lineRenderer.GetPosition(0).y + 0.1f, lineRenderer.GetPosition(0).z));
        }
        else if (lineRenderer.positionCount > 2)
        {
            lineRenderer.positionCount = 2;
        }
    }

    public void OnUpButtonPress()
    {
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(1) + Vector3.up * 0.1f);
    }

    public void OnDownButtonPress()
    {
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(1) + Vector3.down * 0.1f);
    }

    public void GetHeight()
    {
        heightText.text = "";
        int totalPoints = lineRenderer.positionCount;
        if (totalPoints > 1)
        {
            float dist = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1)) * unitConverter;
            print($"Distance between Point-A and Point-B is {dist} {_currentUnit}");
            heightText.text += $"\nDistance between Point-A and Point-B is {dist} {_currentUnit}";
        }
        HeightPopup.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(AppConstants.MAIN_MENU);
        SceneManager.UnloadSceneAsync(AppConstants.MEASURE_HEIGHT);
    }
}
