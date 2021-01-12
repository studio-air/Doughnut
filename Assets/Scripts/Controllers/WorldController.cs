using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject housePrefab;
    public GameObject pivotPrefab;

    public List<GameObject> roads = new List<GameObject>();
    public List<GameObject> houses = new List<GameObject>();

    public Material draftMaterial;
    private GameObject draftRoad;

    public void AddRoad(Vector3 start, Vector3 end)
    {
        GameObject newRoad = Instantiate(roadPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
        newRoad.transform.SetParent(transform);

        UpdateRoad(newRoad, start, end);

        roads.Add(newRoad);

        CancelDraft();
    }

    public void DraftRoad(Vector3 start, Vector3 end)
    {
        if(draftRoad == null)
        {
            //new draft
            draftRoad = Instantiate(roadPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0));
            draftRoad.transform.SetParent(transform);
            UpdateRoad(draftRoad, start, end);
            draftRoad.GetComponent<Renderer>().material = draftMaterial;
        }
        else
        {
            UpdateRoad(draftRoad, start, end);
        }
    }

    public Vector3 CheckForSnap(Vector3 pos)
    {
        Vector3 last = pos;
        float distance = 0.2f;
        float lastDistance = distance*2;
        foreach (GameObject road in roads)
        {
            LineRenderer lr = road.GetComponent<LineRenderer>();
            Vector3 p = lr.GetPosition(0);
            float d = Vector3.Distance(p, pos);
            
            if(d <= distance && d < lastDistance)
            {
                last = p;
                lastDistance = d;
            }

            p = lr.GetPosition(1);
            d = Vector3.Distance(p, pos);
            if(d <= distance && d < lastDistance)
            {
                last = p;
                lastDistance = d;
            }
            
        }
        return last;
    }

    public void UpdateRoad(GameObject road, Vector3 start, Vector3 end)
    {
        LineRenderer lr = road.GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public void CancelDraft()
    {
        Destroy(draftRoad);
        draftRoad = null;
    }

    public void AddHouse(Vector3 start, Vector3 end)
    {
        float dist = Vector3.Distance(start, end);
        int count = (int)Mathf.Floor(dist/0.15f);

        GameObject pivot = Instantiate(pivotPrefab, start, Quaternion.identity);
        for (int i = 0; i < count; i++)
        {
            Vector3 newStart = new Vector3(start.x + i * 0.15f, start.y, start.z);
            GameObject newHouse = Instantiate(housePrefab, newStart, Quaternion.identity);
            newHouse.transform.SetParent(pivot.transform);
            houses.Add(newHouse);
            
        }
        Vector3 dir = (start - end).normalized;
        pivot.transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90, 0);
        pivot.transform.SetParent(transform);
        CancelDraft();
    }

}
