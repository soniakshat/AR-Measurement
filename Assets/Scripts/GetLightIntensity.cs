using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class GetLightIntensity : MonoBehaviour
{
    public LightSensor ls;
    public Text txt;

    private void Start()
    {
        InputSystem.EnableDevice(ls);
    }

    void Update()
    {
        var LS = ls.lightLevel;
        print("Light Intensity: "+LS);
        txt.text = "Light Intensity: " + LS;
    }
}