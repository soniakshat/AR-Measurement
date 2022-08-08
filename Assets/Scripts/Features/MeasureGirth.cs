using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class MeasureGirth : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TextMeshProUGUI girthText;
    public GameObject GirthPopup;



    private string _currentUnit;
    private float unitConverter = 1;



    void Start()
    {
        if (GirthPopup.activeSelf)
            GirthPopup.SetActive(false);

        placementInteractable.objectPlaced.AddListener(DrawLine);

        _currentUnit = PlayerPrefs.GetString("MeasurmentUnit");

        switch (_currentUnit)
        {
            case "cm":
                {
                    unitConverter = 100;
                    break;
                }
            case "in":
                {
                    unitConverter = 39.37f;
                    break;
                }
            case "ft":
                {
                    unitConverter = 3.2808f;
                    break;
                }
            default:
                {
                    unitConverter = 1;
                    break;
                }
        }
    }

    void DrawLine(ARObjectPlacementEventArgs args)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);
        if (lineRenderer.positionCount == 2)
        {
            lineRenderer.SetPosition(1, lineRenderer.GetPosition(0) + (Vector3.up * 0.2f));
        }
        else if (lineRenderer.positionCount > 2)
        {
            lineRenderer.positionCount = 2;
            GetGirth();
        }
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
        SceneManager.LoadScene(0);
    }
}
