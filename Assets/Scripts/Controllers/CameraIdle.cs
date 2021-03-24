using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIdle : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    void LateUpdate()
    {
        transform.Rotate(new Vector3(0, speed, 0), Space.World); 
    }
}
