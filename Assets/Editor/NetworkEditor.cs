using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Network))]
public class NetworkEditor : Editor
{
    Network network;
    Point lastPoint = null;

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {

            Undo.RecordObject(network, "Add Point");
            Point newPoint = new Point(mousePos.x, mousePos.y);

            if (lastPoint != null)
            {
                Path path = new Path(lastPoint, newPoint);
                network.paths.Add(path);
            }

            network.points.Add(newPoint);
            lastPoint = newPoint;

        }
    }

    void Draw()
    {
        Handles.color = Color.red;
        for (int i = 0; i < network.paths.Count; i++)
        {
            Path path = network.paths[i];
            Handles.DrawLine(path.from.position, path.to.position);
        }

        Handles.color = Color.red;
        for (int i = 0; i < network.points.Count; i++)
        {
            Point point = network.points[i];
            Vector2 newPos = Handles.FreeMoveHandle(point.position, Quaternion.identity, .2f, Vector2.zero, Handles.CubeHandleCap);
            if(point.position != newPos)
            {
                Undo.RecordObject(network, "Move Point");
                network.points[i].position = newPos;
            }
        }
    }

    void OnEnable()
    {
        network = (Network)target;
    }
}
