using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blip : MonoBehaviour
{
    public Image BlipImage;

    private void Update()
    {
        Color c = this.BlipImage.color;
        this.BlipImage.color = new Color(c.r, c.g, c.b, c.a * 0.98f);
    }

    public void DoBlip(bool emphasize = false) {
        if (emphasize) {
            this.BlipImage.color = Color.white;
        } else {
            this.BlipImage.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
        }
    }
}
