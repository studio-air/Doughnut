using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public Color color;
 
    private new Renderer renderer;
    private MaterialPropertyBlock propBlock;
 
    void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        renderer = GetComponent<Renderer>();
    }
 
    void Update()
    {
        // Get the current value of the material properties in the renderer.
        renderer.GetPropertyBlock(propBlock);
        // Assign our new value.
        propBlock.SetColor("_Color", color);
        // Apply the edited values to the renderer.
        renderer.SetPropertyBlock(propBlock);
    }
}
