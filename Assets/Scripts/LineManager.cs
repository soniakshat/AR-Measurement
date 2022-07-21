using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class LineManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ARPlacementInteractable placementInteractable;
    // Start is called before the first frame update
    void Start()
    {
        placementInteractable.objectPlaced.AddListener(DrawLine);
    }

    void DrawLine(ARObjectPlacementEventArgs args)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, args.placementObject.transform.position);
    }
}
