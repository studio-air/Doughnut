using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Vector2 position;
    public Point(float x, float y)
    {
        position = new Vector2(x, y);
    }
}

public class Path
{
    public Point from;
    public Point to;
    public Path(Point f, Point t)
    {
        from = f;
        to = t;
    }
}

public class Network : MonoBehaviour
{
    public List<Point> points = new List<Point>();
    public List<Path> paths = new List<Path>();

    public Doughnut doughnut;

    public GameObject apartmentPrefab;
    public GameObject parkPrefab;

    public Material defaultBlock;
    public Material highlightedBlock;

    GameObject current;

    public void ApartmentShow(Vector3 pos)
    {
        if (current == null)
        {
            current = Instantiate(apartmentPrefab, pos, Quaternion.Euler(0, 0, 0));
            //icon.transform.SetParent(transform);
            //icon.GetComponent<Renderer>().material = draftMaterial;
        }
        else
        {
            current.transform.position = pos;
        }
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Block b = objectHit.GetComponent<Block>();
            
            Block[] blocks = GetComponentsInChildren<Block>();
            foreach(var block in blocks)
            {
                if(block == b)
                {
                    block.GetComponent<MeshRenderer>().material = highlightedBlock;
                }
                else
                {
                    block.GetComponent<MeshRenderer>().material = defaultBlock;
                }
            }
        }
    }

    public void ApartmentHide()
    {
        Destroy(current); 

        MeshRenderer[] blocks = GetComponentsInChildren<MeshRenderer>();
            foreach(var block in blocks)
            {
                block.material = defaultBlock;
            }
    }

    public void AddApartment()
    {
        //Add new icon to the centre of mesh
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Vector3 p = objectHit.GetComponent<Renderer>().bounds.center;
            GameObject obj = Instantiate(apartmentPrefab, p, Quaternion.Euler(0, 0, 0));
        }
        //Modify dough nut
        doughnut.Inner(1, 0.2f);
        doughnut.Inner(5, -0.1f);
        doughnut.Outter(0, -0.2f);
    }
    
    public void ParkShow(Vector3 pos)
    {
        if (current == null)
        {
            current = Instantiate(parkPrefab, pos, Quaternion.Euler(0, 0, 0));
            //icon.transform.SetParent(transform);
            //icon.GetComponent<Renderer>().material = draftMaterial;
        }
        else
        {
            current.transform.position = pos;
        }
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Block b = objectHit.GetComponent<Block>();
            
            Block[] blocks = GetComponentsInChildren<Block>();
            foreach(var block in blocks)
            {
                if(block == b)
                {
                    block.GetComponent<MeshRenderer>().material = highlightedBlock;
                }
                else
                {
                    block.GetComponent<MeshRenderer>().material = defaultBlock;
                }
            }
        }
    }

    public void ParkHide()
    {
        Destroy(current); 

        MeshRenderer[] blocks = GetComponentsInChildren<MeshRenderer>();
            foreach(var block in blocks)
            {
                block.material = defaultBlock;
            }
    }

    public void AddPark()
    {
        //Add new icon to the centre of mesh
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Vector3 p = objectHit.GetComponent<Renderer>().bounds.center;
            GameObject obj = Instantiate(parkPrefab, p, Quaternion.Euler(0, 0, 0));
        }
        
        doughnut.Inner(3, -0.2f);
        doughnut.Inner(5, -0.1f);
        doughnut.Outter(0, -0.2f);
        //Modify dough nut
    }
}
