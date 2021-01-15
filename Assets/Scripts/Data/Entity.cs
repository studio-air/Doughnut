using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RESIDENTIAL_DENSITY
{
    NONE = 0,
    LOW = 1,
    MID = 2,
    HIGH = 3
};

[Serializable]
public struct Affects
{
    public DOUGHNUT target;
    public float val;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Entity", order = 1)]
public class Entity : ScriptableObject
{
    public string entityName;

    public Color color;

    public GameObject prefab;

    public RESIDENTIAL_DENSITY minDensity;
    public RESIDENTIAL_DENSITY maxDensity;

    public Affects[] affects;
}
