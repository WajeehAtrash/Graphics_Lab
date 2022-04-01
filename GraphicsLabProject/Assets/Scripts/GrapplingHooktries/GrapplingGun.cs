using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    //source code and Implemetation from DanisTutorials
    //https://www.youtube.com/watch?v=Xgh4v1w5DxU&t=6s
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;  
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))//clicking the right mouse button 
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate()//Drwing the robe using late update to prevent robe sttutring and appearing in aglitchy way
    {
        DrawRope();
    }

    //Grappling method called when the playe clicking the right mouse button
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))//checking if the traget is grappelable
        {
            grapplePoint = hit.point;                               // the coordinate where we want to grappel
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;             //telling unity that we want to add the anchor point manualy
            joint.connectedAnchor = grapplePoint;                   //seting the anchor point  

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.25f;


            joint.spring = 4f;      //The spring force used to keep the two objects together.
            joint.damper = 5f;      //The damper force used to dampen the spring force.
            joint.massScale = 100f;

            // setting the number of vertices in the line
            lr.positionCount = 2;

            currentGrapplePosition = gunTip.position;
        }
    }


    
    // Call whenever we want to stop a grapple
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        //if (!isGrappel) return;
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);       //interpolated vector (Linear interpolation) for  move an object gradually between

        //seting the line points to draw
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
