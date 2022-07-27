using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
    }

    StateManager stateManager;

    public float PivotAngle = 35f;
    Vector3 pivotVelocity;

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        //float theAngle = this.transform.rotation.eulerAngles.y;

        //if(theAngle > 180)
        //{
        //    theAngle -= 360f;
        //}

        //theAngle = Mathf.SmoothDamp(
        //    theAngle, 
        //    stateManager.CurrentPlayerID == 0 ? PivotAngle : -PivotAngle,
        //    ref pivotVelocity, 
        //    0.25f);

        //this.transform.rotation = Quaternion.Euler( new Vector3(0, theAngle, 0) );

        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPostion, ref pivotVelocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
