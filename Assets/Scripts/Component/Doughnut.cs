using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DOUGHNUT
{
    CLIMATE_CHANGE = 0,
    OCEAN_ACID = 1,
    CHEMICAL_POL = 2,
    NAP_LOADING = 3,
    FRESHWATER_WITHDRAWALS = 4,
    LAND_CONV = 5,
    BIO_LOSS = 6,
    AIR_POL = 7,
    OZONE_LAYER_DEPLETION = 8,

    FOOD_SECURITY = 9,
    HEALTH = 10,
    EDUCATION = 11,
    INCOME_AND_WORK = 12,
    PEACE_AND_JUSTICE = 13,
    POLITICAL_VOICE = 14,
    SOCIAL_EQUITY = 15,
    GENDER_EQUALITY = 16,
    HOUSING = 17,
    NETWORKS = 18,
    ENERGY = 19,
    WATER = 20
};

public class Doughnut : MonoBehaviour
{
    public GameObject outterHolder;
    public GameObject innerHolder;

    public Image[] outter;
    public Image[] inner;

    private void Awake() 
    {
        outter = outterHolder.GetComponentsInChildren<Image>();
        inner = innerHolder.GetComponentsInChildren<Image>();
    }

    public void Modify (DOUGHNUT index, float val)
    {
        int ind = (int)index;
        if(ind < outter.Length)
        {
            outter[ind].color = ChangeColour(outter[ind].color, val);
        }
        else if(ind < inner.Length + outter.Length)
        {
            ind = ind - 9;
            inner[ind].color = ChangeColour(outter[ind].color, val);
        }
    }

    public void Outter(int index, float val)
    {
        outter[index].color = ChangeColour(outter[index].color, val);
    }

    public void Inner(int index, float val)
    {
        inner[index].color = ChangeColour(inner[index].color, val);
    }

    Color ChangeColour(Color c, float value)
    {
        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);

        Color col =  Color.HSVToRGB(0, s+value, 1);
        return col;
    }
}
