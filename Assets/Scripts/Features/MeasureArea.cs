using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class MeasureArea : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TextMeshProUGUI mText, distinfo, areaText;
    public GameObject AreaPopup, distancePopup;



    private string _currentUnit;
    private float unitConverter = 1;


    private void Start()
    {
        if (AreaPopup.activeSelf)
            AreaPopup.SetActive(false);
        if (distancePopup.activeSelf)
            distancePopup.SetActive(false);

        placementInteractable.objectPlaced.AddListener(DrawLine);
        distinfo.text = "";

        _currentUnit = PlayerPrefs.GetString("MeasurementUnit");

        unitConverter = _currentUnit switch
        {
            "m" => 1f,
            "cm" => 100f,
            "in" => 39.37f,
            "ft" => 3.2808f,
            _ => 1f
        };
    }

    private void DrawLine(ARObjectPlacementEventArgs args)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);
        if (lineRenderer.positionCount > 1)
        {
            Vector3 pointA = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            Vector3 pointB = lineRenderer.GetPosition(lineRenderer.positionCount - 2);

            float dist = Mathf.Round(Vector3.Distance(pointA, pointB)) * unitConverter;
            distinfo.text += $"\nDistance between point {pointA} and {pointB} is {dist} {_currentUnit}";
            print($"Distance between point {pointA} and {pointB} is {dist} {_currentUnit}");

            TextMeshProUGUI distText = Instantiate(mText);
            distText.text = dist.ToString();

            Vector3 directionVector = (pointB - pointA);
            Vector3 normal = args.placementObject.transform.up;

            Vector3 upd = Vector3.Cross(directionVector, normal).normalized;
            Quaternion rotation = Quaternion.LookRotation(normal, upd);

            distText.transform.rotation = rotation;
            distText.transform.position = (pointA + directionVector * 0.5f) + upd * 0.2f;
        }
    }

    public void OnMeasureButtonPress()
    {
        print("Measure Button Clicked");
        print("Current Measurement Unit is: " + _currentUnit);
        distinfo.text = "";
        int totalPoints = lineRenderer.positionCount;
        float proA = 0, proB = 0, area = 0;
        if (totalPoints > 2)
        {
            for (int i = 0; i < totalPoints; i++)
            {
                if (i < (totalPoints - 1))
                {
                    proA += lineRenderer.GetPosition(i).x * lineRenderer.GetPosition(0).z;
                    proB += lineRenderer.GetPosition(0).x * lineRenderer.GetPosition(i).z;
                }
                else
                {
                    proA += lineRenderer.GetPosition(i).x * lineRenderer.GetPosition(i + 1).z;
                    proB += lineRenderer.GetPosition(i + 1).x * lineRenderer.GetPosition(i).z;
                }
            }

            area = (Mathf.Abs(proA - proB) / 2) * unitConverter * unitConverter;
            area = (float)Math.Round((decimal)area, 2);
            print(area + $" in square  {_currentUnit}");
            areaText.text = area.ToString() + $" in {_currentUnit}<sup>2</sup>";
            AreaPopup.SetActive(true);
        }
        else
        {
            print("Invalid Shape to measure Area");
            areaText.text = "Invalid Shape to measure Area";
            AreaPopup.SetActive(true);
        }
    }

    public void GetDistances()
    {
        distinfo.text = "";
        int totalPoints = lineRenderer.positionCount;
        if (totalPoints > 1)
        {
            for (int i = 0; i < totalPoints - 1; i++)
            {
                float dist = Vector3.Distance(lineRenderer.GetPosition(i + 1), lineRenderer.GetPosition(i)) * unitConverter;
                print($"Distance between Point-{i} and Point-{i + 1} is {dist} {_currentUnit}");
                distinfo.text += $"\nDistance between Point-{i} and Point-{i + 1} is {dist} {_currentUnit}";
            }
        }
        distancePopup.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(AppConstants.MAIN_MENU);
        SceneManager.UnloadSceneAsync(AppConstants.MEASURE_AREA);
    }
}
