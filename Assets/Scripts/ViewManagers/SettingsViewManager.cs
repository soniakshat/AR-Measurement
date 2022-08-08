using UnityEngine;
using TMPro;
public class SettingsViewManager : MonoBehaviour
{

    [SerializeField] private TMP_Text currentUnit;

    private void OnEnable()
    {
        currentUnit.text = "Current Unit: " + PlayerPrefs.GetString("MeasurementUnit");
    }

    public void SetUnits(string unit)
    {
        PlayerPrefs.SetString("MeasurementUnit", unit);
        currentUnit.text = "Current Unit: " + PlayerPrefs.GetString("MeasurementUnit");
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
    }

}
