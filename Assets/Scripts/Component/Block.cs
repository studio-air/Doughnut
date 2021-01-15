using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Network network;

    public Material defaultBlock;
    public Material highlightedBlock;

    private bool highlighted = false;

    public void AddEntity(Entity entity)
    {
        //TODO: Confirm you can place these.
        Vector3 centre = GetComponent<Renderer>().bounds.center;
        GameObject marker = Instantiate(entity.prefab, centre, Quaternion.Euler(0, 0, 0));
        marker.GetComponent<Marker>().color = entity.color;
        //Apply entity to Doughnut
        foreach (Affects affect in entity.affects)
        {
            network.doughnut.Modify(affect.target, affect.val);
        }
    }

    private void Update() {
        if(highlighted)
        {
            GetComponent<Renderer>().material = highlightedBlock;
            highlighted = false;
        }
        else
        {
            GetComponent<Renderer>().material = defaultBlock;
        }
    }

    public void Highlight()
    {
        highlighted = true;
    }

}
