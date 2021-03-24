using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Neighbourhood))]
public class NeighbourhoodEditor : Editor
{
    Neighbourhood neighbourhood;
    Point lastPoint = null;
    bool nodeClicked = false;
    bool connectingPoints = false;
    Vector2 projectionOnPath = new Vector2(0, 0);

    void OnSceneGUI()
    {
        if(connectingPoints)
        {
            ConnectingInput();
        }
        else
        {
            CreateInput();
            DeleteInput();
        }
        Draw();
    }
    
    public override void OnInspectorGUI () {
		DrawDefaultInspector();
		if (GUILayout.Button("Generate Gameboard")) 
        {
            Debug.Log("Generating!");
            GenerateGameboard();
		}

        if (GUILayout.Button("Clear Gameboard")) 
        {            
            int childs = neighbourhood.transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                DestroyImmediate(neighbourhood.transform.GetChild(i).gameObject);
            }
		}
	}

    void GenerateGameboard()
    {
        //*
        List<Path> startingPaths = new List<Path>(neighbourhood.paths);
        //Debug.Log("Starting Paths: " + startingPaths.Count);

        List<Vector2[]> polygons = new List<Vector2[]>();

        while(startingPaths.Count > 0)
        {
            Path s = startingPaths[0];
            List<Path> polygon = new List<Path>();
            Path current = s;

            Path pTo = GetSmallestAngle(current, s.to);
            Path pFrom = GetSmallestAngle(current, s.from);

            //FIXME: Issue with angles, for some reason its non dependant of location
            Point dir = null;

            float aTo = neighbourhood.GetAngle(current, pTo, s.to);
            float aFrom = neighbourhood.GetAngle(current, pFrom, s.from);
            
            {
                Debug.Log("Current: " + current.from.position.ToString() + current.to.position.ToString());
                Debug.Log("To: " + pTo.from.position.ToString() + pTo.to.position.ToString());
                Debug.Log("To Angle: " + aTo);
                Debug.Log("From: " + pFrom.from.position.ToString() + pFrom.to.position.ToString());
                Debug.Log("From Angle: " + aFrom);
            }

            if ( aTo < aFrom )
                dir = s.to;
            else
                dir = s.from;

            //*
            do
            {
                Debug.Log("Paths Count: " + startingPaths.Count);
                //*
                Path next = GetSmallestAngle(current, dir);

                //TODO: Based on the direction, pick other point from new path
                if (next.to == dir)
                    dir = next.from;
                else
                    dir = next.to;

                if(startingPaths.Contains(current))
                    startingPaths.Remove(current);
                
                current = next;
                polygon.Add(next);
            } while (current != s);
            //*/

            List<Vector2> p = new List<Vector2>();
            foreach (Path path in polygon)
            {
                if(!p.Contains(path.to.position))
                    p.Add(path.to.position);
                
                if(!p.Contains(path.from.position))
                    p.Add(path.from.position);
            }

            polygons.Add(p.ToArray());

        }

        Debug.Log(polygons.Count);
        foreach (Vector2[] polygon in polygons)
        {
            neighbourhood.GenerateBlock(polygon);
        }
        //*/

    }

    Path GetSmallestAngle(Path current, Point dir)
    {
        Path[] neighbours = neighbourhood.GetNeighbours(current, dir);
        float bAngle = 360;
        Path next = null;
        foreach (Path neighbour in neighbours)
        {
            float cAngle = neighbourhood.GetAngle(current, neighbour, dir);
            if(cAngle < bAngle)
            {
                bAngle = cAngle;
                next = neighbour;
            }
        }
        return next;
    }

    void CreateInput()
    {
        Event guiEvent = Event.current;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            Path path = getPathByPosition(mousePos);
            Point point = getPointByPosition(mousePos);

            if ( point != null)
            {
                nodeClicked = true;
                lastPoint = null;
                Debug.Log("Clicked on point!");
                //TODO: start connecting paths
                connectingPoints = true;
                lastPoint = point;
                
            }
            else if (path != null)
            {
                Undo.RecordObject(neighbourhood, "Add Point On Path");
                lastPoint = null;
                Debug.Log("Clicked on path!");
                
                Point f = path.from;
                Point t = path.to;

                neighbourhood.DeletePath(path);

                Point newPoint = new Point(projectionOnPath.x, projectionOnPath.y);

                neighbourhood.paths.Add(new Path(f, newPoint));
                neighbourhood.paths.Add(new Path(newPoint, t));

                neighbourhood.points.Add(newPoint);

                lastPoint = newPoint;

                //Create point at mouse button

            }
            else
            {
                Debug.Log("Added point!");
                Undo.RecordObject(neighbourhood, "Add Point");
                Point newPoint = new Point(mousePos.x, mousePos.y);

                if (lastPoint != null)
                {
                    Path newPath = new Path(lastPoint, newPoint);
                    neighbourhood.paths.Add(newPath);
                }

                neighbourhood.points.Add(newPoint);
                lastPoint = newPoint;
            }
        }
    }

    void ConnectingInput()
    {
        Event guiEvent = Event.current;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            Point point = getPointByPosition(mousePos);

            if ( point != null && lastPoint != point)
            {
                
                neighbourhood.paths.Add(new Path(lastPoint, point));
                lastPoint = null;
                connectingPoints = false;
            }
        }else if(guiEvent.type == EventType.MouseDown && guiEvent.button == 1 && guiEvent.shift)
        {
            lastPoint = null;
            connectingPoints = false;
        }
    }

    void DeleteInput()
    {
        Event guiEvent = Event.current;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.control)
        {
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            Path path = getPathByPosition(mousePos);
            Point point = getPointByPosition(mousePos);

            if ( point != null)
            {
                neighbourhood.DeletePoint(point);
            }
            else if (path != null)
            {
                neighbourhood.DeletePath(path);
            }
        }
    }

    void Draw()
    {
        Event guiEvent = Event.current;
        Handles.color = Color.red;
        for (int i = 0; i < neighbourhood.paths.Count; i++)
        {
            Path path = neighbourhood.paths[i];
            Handles.DrawLine(path.from.position, path.to.position);
        }

        if(connectingPoints)
        {
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            Handles.DrawLine(lastPoint.position, mousePos);
        }

        Handles.color = Color.red;
        for (int i = 0; i < neighbourhood.points.Count; i++)
        {
            Point point = neighbourhood.points[i];
            Vector2 newPos = Handles.FreeMoveHandle(point.position, Quaternion.identity, .2f, Vector2.zero, Handles.CubeHandleCap);
            if(point.position != newPos)
            {
                Undo.RecordObject(neighbourhood, "Move Point");
                if (!nodeClicked)
                    neighbourhood.points[i].position = newPos;
                
                nodeClicked = false;
            }
        }
    }

    Point getPointByPosition(Vector2 position)
    {
        for (int i = 0; i < neighbourhood.points.Count; i++)
        {
            if(Vector3.Distance(position, neighbourhood.points[i].position) < .2f)
                return neighbourhood.points[i];
        }

        return null;
    }

    Path getPathByPosition(Vector2 position)
    {
        for (int i = 0; i < neighbourhood.paths.Count; i++)
        {
            Path p = neighbourhood.paths[i];
            Vector2 v = p.from.position;
            Vector2 w = p.to.position;
            float distance = 0f;
            Vector2 offset = w - v;
            float l2 = offset.sqrMagnitude;
            //float l2 = Vector2.Distance(v, w);

            if (l2 == 0.0f)
            {
                distance = Vector2.Distance(position, v);
                projectionOnPath = v;
            }
            else
            {
                float t = Mathf.Max(0, Mathf.Min(1, Vector2.Dot(position - v, w - v) / l2));
                Vector2 projection = v + t * (w - v);
                projectionOnPath = projection;
                distance = Vector2.Distance(position, projection);
            }

            Debug.Log(i + " : " + distance);

            if(distance < .2f)
                return neighbourhood.paths[i];
        }

        return null;
    }

    void OnEnable()
    {
        neighbourhood = (Neighbourhood)target;
    }
}
