using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] float distance = 1.0f;
    [SerializeField] float angle = 0f;
    [SerializeField] float xSpeed = 120.0f;
    [SerializeField] float ySpeed = 120.0f;
 
    [SerializeField] float yMinLimit = -90f;
    [SerializeField] float yMaxLimit = 90f;
 
    [SerializeField] float distanceMin = 1f;
    [SerializeField] float distanceMax = 15f;
 
    float x = 0.0f;
    float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = pivot.eulerAngles;
        x = 0;
        y = angle;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (pivot) 
        {
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);

            if(Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * distance * 0.02f;
    
                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }else if(Input.GetMouseButton(2))
            {

                Vector2 pan = new Vector2(Input.GetAxis("Mouse X") * xSpeed * 0.005f,  Input.GetAxis("Mouse Y") * ySpeed * 0.005f);

                Vector3 relative = transform.TransformDirection(-pan.x, pan.y, 0f);

                pivot.position = pivot.position + relative;

            }
    
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + pivot.position;

            transform.rotation = rotation;
            transform.position = position;
        }
        
    }
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
