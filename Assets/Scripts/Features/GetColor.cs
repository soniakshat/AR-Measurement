using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColor : MonoBehaviour
{
    public RenderTexture texRender;
    public Text colorCode;
    public Image detectedColor;
    private Color cc = Color.white;

    private void Start()
    {
        texRender.width = Screen.width;
        texRender.height = Screen.height;
    }

    void Update()
    {
        GetColorFromTexture(texRender);
        colorCode.text = "Color: " + cc;
        detectedColor.color = cc;
        // print("ABC::: Color: " + cc);
    }

    public void GetColorFromTexture(RenderTexture texRender)
    {
        Texture2D tex = new Texture2D(texRender.width, texRender.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, texRender.width, texRender.height), 0, 0);
        tex.Apply();
        cc = tex.GetPixel(0, 0);
        Destroy(tex);
    }
}
