using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;
using TMPro;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    public TMP_Text mText;
    // Start is called before the first frame update
    void Start()
    {
        placementInteractable.objectPlaced.AddListener(DrawLine);
    }

    void DrawLine(ARObjectPlacementEventArgs args)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);
        if (lineRenderer.positionCount > 1)
        {
            Vector3 pointA = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
            Vector3 pointB = lineRenderer.GetPosition(lineRenderer.positionCount - 2);

            float dist = Mathf.Round(Vector3.Distance(pointA, pointB) * 100);
            print($"Distance between point {pointA} and {pointB} is {dist} cm");

            TMP_Text distText = Instantiate(mText);
            distText.text = dist.ToString() + " cm";

            Vector3 directionVector = (pointB - pointA);
            Vector3 normals = args.placementObject.transform.up;

            Vector3 upDirn = Vector3.Cross(directionVector, normals).normalized;
            Quaternion rotation = Quaternion.LookRotation(normals, upDirn);

            // distText.transform.rotation = rotation;
            // distText.transform.position = (pointA + directionVector) + upDirn;
        }
    }
}
