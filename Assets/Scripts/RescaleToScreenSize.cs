using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescaleToScreenSize : MonoBehaviour
{
   
    private void Awake()
    {
        ScaleBackgroundToFitScreenSize();
    }


    private void ScaleBackgroundToFitScreenSize()
    {
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);

        print(deviceScreenResolution);

        float srcHeight = Screen.height;
        float srcWidth = Screen.width;

        float deviceScreenAspect = srcWidth / srcHeight;
        print(deviceScreenAspect.ToString());

        GetComponent<Camera>().aspect = deviceScreenAspect;

        float camHeight = 100.0f + GetComponent<Camera>().orthographicSize * 2.0f;
        float camWidth = camHeight * deviceScreenAspect;

        Debug.Log("CamHeight:" + camHeight);
        Debug.Log("CamHeight:" + camWidth);

        SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer spritex in sprites)
        {
            float spriteH = spritex.sprite.rect.height;
            float spriteW = spritex.sprite.rect.width;

            float spirteHR = camHeight / spriteH;
            float spirteWR = camWidth / spriteW;

            spritex.transform.parent.gameObject.transform.localScale = new Vector3(spirteWR, spirteHR, 1);
            //spritex.transform.localScale = new Vector3(spirteWR, spirteHR, 1);

        }
    }
}
