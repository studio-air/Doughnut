using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doughnut : MonoBehaviour
{
    public Image[] outter;
    public Image[] inner;

    public void Outter(int index, float val)
    {
        float h, s, v;
        Color.RGBToHSV(outter[index].color, out h, out s, out v);

        Color col = Color.HSVToRGB(0, s+val, 1);
        Debug.Log(s);
        outter[index].color = col;
    }

    public void Inner(int index, float val)
    {
        float h, s, v;
        Color.RGBToHSV(inner[index].color, out h, out s, out v);

        Color col =  Color.HSVToRGB(0, s+val, 1);
        inner[index].color = col;
    }
}
