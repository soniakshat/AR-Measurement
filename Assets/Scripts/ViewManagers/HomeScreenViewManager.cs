using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenViewManager : MonoBehaviour
{
    public GameObject SettingsPopup;

    public void OnMeasureHeightButtonClick()
    {
        print("Measure Height Button Clicked");
        SceneManager.LoadScene("MeasurePlantHeight");
    }

    public void OnMeasureGirthButtonClick()
    {
        print("Measure Girth Button Clicked");
        SceneManager.LoadScene("MeasurePlantGirth");
    }

    public void OnMeasureLeafAreaButtonClick()
    {
        print("Measure Leaf Area Button Clicked");
        SceneManager.LoadScene("MeasureLeafArea");

    }

    public void OnSettingsButtonClick()
    {
        SettingsPopup.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
