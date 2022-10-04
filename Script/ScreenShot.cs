using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    public GameObject UI;
  
    public IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D _texture = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);

        _texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _texture.Apply();

        string name = "Screenshot_AR" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

        //Mobile
        //NativeGallery

        Destroy(_texture);
        UI.SetActive(true);
    }

    public void TakeScreenShot()
    {
        UI.SetActive(false);
        StartCoroutine(Screenshot());
    }
}
