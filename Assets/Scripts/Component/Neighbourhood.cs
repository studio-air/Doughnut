using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mattatz.Triangulation2DSystem;

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

public class Neighbourhood : MonoBehaviour
{
    public List<Point> points = new List<Point>();
    public List<Path> paths = new List<Path>();

    public Doughnut doughnut;

    public GameObject blockPrefab;

    private void Awake() 
    {
        Block[] pBlocks = GetComponentsInChildren<Block>();
        foreach (Block block in pBlocks)
        {
            block.neighbourhood = this;
        }

        //Rotate
        transform.Rotate(90f, 0f, 0f);

    }

    public void Build(int blockID, Entity entity)
    {
        Block[] pBlocks = GetComponentsInChildren<Block>();
        foreach (Block block in pBlocks)
        {
            if(block.id == blockID)
            {
                block.AddEntity(entity);
                break;
            }
        }
    }

    public void DeletePoint(Point p)
    {
        List<Path> pathsToDelete = new List<Path>();
        foreach (Path path in paths)
        {
            if (path.from == p || path.to == p)
            {
                pathsToDelete.Add(path);
            }
        }
        foreach (Path path in pathsToDelete)
        {
            DeletePath(path);
        }

        points.Remove(p);

    }

    public void DeletePath(Path p)
    {
        paths.Remove(p);
    }

    public Path[] GetNeighbours(Path origin, Point dir)
    {
        List<Path> collection = new List<Path>();
        foreach (Path path in paths)
        {
            if ((path.to == dir || path.from == dir) && origin != path)
                collection.Add(path);
                
            
        }
        //Go through all of the paths
        //Check if any nodes that are connected to the point
        Debug.Log(collection.Count);
        return collection.ToArray();
    }

    public float GetAngle(Path primary, Path secondary, Point dir)
    {

        //NOTE: p1 and s1 must be at same position
        //NOTE: anti clockwise
        Vector2 p1 = new Vector2();
        Vector2 p2 = new Vector2();
        if(primary.to == dir)
        {
            p1 = primary.to.position;
            p2 = primary.from.position;
        }
        else
        {
            p1 = primary.from.position;
            p2 = primary.to.position;
        }
        Vector2 p = p1 - p2;
        //Debug.Log(p.ToString());

        Vector2 s1 = new Vector2();
        Vector2 s2 = new Vector2();
        if(secondary.to == dir)
        {
            s1 = secondary.to.position;
            s2 = secondary.from.position;
        }
        else
        {
            s1 = secondary.from.position;
            s2 = secondary.to.position;
        }
        Vector2 s = s1 - s2;
        //Debug.Log(s.ToString());

        float angle = Vector2.SignedAngle(p, s);

        if (angle < 0)
            angle = 360 + angle;
        
        return angle;
    }

    public void GenerateBlock(Vector2[] ps)
    {
        GameObject block = Instantiate(blockPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
        block.transform.SetParent(transform);

        // construct Polygon2D 
        Polygon2D polygon = Polygon2D.Contour(ps);

        // construct Triangulation2D with Polygon2D and threshold angle (18f ~ 27f recommended)
        Triangulation2D triangulation = new Triangulation2D(polygon, 22.5f);
 
        // Create the mesh
        Mesh msh = triangulation.Build();
        msh.RecalculateNormals();
        msh.RecalculateBounds();
 
        // Set up game object with mesh;
        block.AddComponent(typeof(MeshRenderer));
        MeshFilter filter = block.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;
        
        block.GetComponent<Block>().neighbourhood = this;
        block.AddComponent<MeshCollider>();
    }

}
