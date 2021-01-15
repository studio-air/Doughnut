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

    private void Awake() 
    {
        Block[] pBlocks = GetComponentsInChildren<Block>();
        foreach (Block block in pBlocks)
        {
            block.network = this;
        }
    }

}
