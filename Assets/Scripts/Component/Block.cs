using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Neighbourhood neighbourhood;

    public int id;

    public Material defaultBlock;
    public Material highlightedBlock;

    private bool highlighted = false;

    public bool occupied = false;

    public void AddEntity(Entity entity)
    {
        //TODO: Confirm you can place these.

        switch(entity.prefabDensity)
        {
            case PREFAB_DENSITY.UNIQUE:
                BuildUnique(entity.prefabs);
                break;
            case PREFAB_DENSITY.LOW:
                BuildLowDensity(entity.prefabs);
                break;
            case PREFAB_DENSITY.HIGH:
                BuildHighDensity(entity.prefabs);
                break;
            default:
                BuildUnique(entity.prefabs);
                break;
        }

        //Start casting rays from the centre of the render
        //If array hits, 


        //marker.GetComponent<Marker>().color = entity.color;
        //Apply entity to Doughnut
        occupied = true;
         
        foreach (Affects affect in entity.affects)
        {
            neighbourhood.doughnut.Modify(affect.target, affect.val);
        }
    }

    private void BuildUnique(GameObject[] prefabs)
    {
        int val = Random.Range(0, prefabs.Length);
        Vector3 centre = GetComponent<Renderer>().bounds.center;
        GameObject marker = Instantiate(prefabs[val], centre, Quaternion.Euler(0, 0, 0));
    }

    private void BuildHighDensity(GameObject[] prefabs)
    {

        Vector3 centre = GetComponent<Renderer>().bounds.center;
        Vector3 extents = GetComponent<Renderer>().bounds.extents;

        float eSize = 0.28f;
        float padding = 0.1f;

        int xMax = Mathf.RoundToInt(extents.x*2 / (eSize + padding));
        int zMax = Mathf.RoundToInt(extents.z*2 / (eSize + padding));

        //*
        for (int x = 0; x <= xMax; x++)
        {
            for (int z = 0; z <= zMax; z++)
            {
                int val = Random.Range(0, prefabs.Length);
                Debug.Log(prefabs.Length - 1);

                float nx = centre.x - extents.x + x * (eSize+padding);
                float nz = centre.z - extents.z + z * (eSize+padding);
                Vector3 pos = new Vector3(nx, centre.y, nz);

                RaycastHit hit;
                Ray ray = new Ray(pos + Vector3.up, Vector3.down * 2f);
                if (Physics.Raycast(ray, out hit)) 
                {
                    if(hit.collider == GetComponent<Collider>())
                    {
                        GameObject marker = Instantiate(prefabs[val], pos, Quaternion.Euler(0, 0, 0));
                    }
                }
            }
        }
        //*/
    }

    private void BuildLowDensity(GameObject[] prefabs)
    {

        Vector3 centre = GetComponent<Renderer>().bounds.center;
        Vector3 extents = GetComponent<Renderer>().bounds.extents;

        float eSize = 0.28f;
        float padding = 0.1f;

        int xMax = Mathf.RoundToInt(extents.x*2 / (eSize + padding));
        int zMax = Mathf.RoundToInt(extents.z*2 / (eSize + padding));

        //*
        for (int x = 0; x <= xMax; x++)
        {
            for (int z = 0; z <= zMax; z++)
            {
                if(Random.value < 0.3)
                    continue;
                
                int val = Random.Range(0, prefabs.Length);

                float nx = centre.x - extents.x + x * (eSize+padding);
                float nz = centre.z - extents.z + z * (eSize+padding);
                Vector3 pos = new Vector3(nx, centre.y, nz);

                RaycastHit hit;
                Ray ray = new Ray(pos + Vector3.up, Vector3.down * 2f);
                if (Physics.Raycast(ray, out hit)) 
                {
                    if(hit.collider == GetComponent<Collider>())
                    {
                        GameObject marker = Instantiate(prefabs[val], pos, Quaternion.Euler(0, 0, 0));
                    }
                }
            }
        }
        //*/
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
