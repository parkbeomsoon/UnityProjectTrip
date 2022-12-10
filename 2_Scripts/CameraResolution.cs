using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    Rect rect;
    float scaleHeight;
    float scaleWidth;

    void Start()
    {
        rect = Camera.main.rect;
        scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        scaleWidth = 1f / scaleHeight;

        if(scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        Camera.main.rect = rect;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
    
}
