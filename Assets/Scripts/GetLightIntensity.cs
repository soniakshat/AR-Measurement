using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GetLightIntensity : MonoBehaviour
{
    LightSensor lightSensor;
    public Text textLightSensor;

    private void Start()
    {
        lightSensor = LightSensor.current;
    }

    void Update()
    {
        if (lightSensor != null)
        {
            InputSystem.EnableDevice(lightSensor);
            textLightSensor.text = "LightSensor Sensor: enabled";
            print("LightSensor Sensor: enabled");
            textLightSensor.text = lightSensor.lightLevel.ReadValue().ToString();
            print("LightSensor Sensor Value: " + textLightSensor.text);
        }
        else
        {
            textLightSensor.text = "LightSensor Sensor: null";
            print("LightSensor Sensor: null");
        }
    }
}