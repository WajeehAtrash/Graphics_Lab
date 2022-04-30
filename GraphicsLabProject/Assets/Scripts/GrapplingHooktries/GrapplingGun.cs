using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    //source code and Implemetation from DanisTutorials
    //https://www.youtube.com/watch?v=Xgh4v1w5DxU&t=6s
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask GrappleLayer;
    public LayerMask Pickup;
    public Transform gunTip, cam, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool grappling = false;
    [SerializeField] private PlayerMovement controlledPlayer;
    private Transform pickupTranform;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    //Late update used to draw the rope correctlly and prevent wierd behavior 
    void LateUpdate()
    {
        DrawRope();
    }


    void StartGrapple()//calling the function when clicking the right mouse button
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, GrappleLayer))
        {
            grapplePoint = hit.point;
            //controlledPlayer.SetSpeed(15);
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 3f;
            joint.massScale = 450f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
        else if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, Pickup))
        {
            grapplePoint = hit.point;
            float distance = Vector3.Distance(player.position, grapplePoint);
            Rigidbody rbPickup = hit.transform.gameObject.GetComponent<Rigidbody>();
            Vector3 normal = (player.position - grapplePoint).normalized;
            Vector3 force=new Vector3(30 * normal.x, 30 * normal.y, 30 * normal.z);
            rbPickup.AddForce(force, ForceMode.Impulse);
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            grappling = true;
            pickupTranform = hit.transform;
            rbPickup.velocity = Vector3.zero;
            rbPickup.angularVelocity = Vector3.zero;
        }
    }



    void StopGrapple()
    {
        lr.positionCount = 0;
        grappling = false;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint&&grappling==false) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        if (grappling == false)
        {
            //giving the rope pointes to draw a line
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentGrapplePosition);
        }
        else
        {
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, pickupTranform.position);
        }
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
