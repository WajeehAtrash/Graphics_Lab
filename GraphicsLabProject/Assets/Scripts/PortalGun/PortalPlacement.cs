using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlacement : MonoBehaviour
{
    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private LayerMask NoPortal;


    [SerializeField]
    private PortalGunCrosshair crosshair;

    private CameraMovement cameraMove;

    private void Awake()
    {
        cameraMove = GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FirePortal(0, transform.position, transform.forward, 250.0f);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            FirePortal(1, transform.position, transform.forward, 250.0f);
        }
    }

    private void FirePortal(int portalID, Vector3 pos, Vector3 dir, float distance)
    {
        RaycastHit hit,noPortalhit;
        Physics.Raycast(pos, dir, out hit, 250.0f, layerMask);
        Physics.Raycast(pos, dir, out noPortalhit, 250.0f, NoPortal);
        if (noPortalhit.collider != null)
        {
            Debug.Log(noPortalhit.collider.name);
            Debug.Log("noportal");
        }
        else if (hit.collider!=null)
        {
            //placing the portal according to camera look and surface direction
            //----------------------------------------------------------------------------------------------------
            //calculating the right direction for the portal
            var cameraRotation = cameraMove.GetRotation();
            //var cameraRotation = cameraMove.TargetRotation;
            var portalRight = cameraRotation * Vector3.right; //geting the player right direction
            //rounding the vector to the nearest 90 degree by comparing  it's x and z components
            if(Mathf.Abs(portalRight.x)>=Mathf.Abs(portalRight.z))
            {
                portalRight = (portalRight.x >= 0) ? Vector3.right : -Vector3.right;
            }
            else
            {
                portalRight = (portalRight.z >= 0) ? Vector3.forward : -Vector3.forward;
            }
            //now portalright variable gives us avector pointing along the desired x-axis of the portal
            //----------------------------------------------------------------------------------------------------
            //portal forward direction of the portal
            //the negative of the raycast’s intersection poin
            var portalForward = -hit.normal;
            //----------------------------------------------------------------------------------------------------
            //the up direction for the portal
            //cross product of two vectors returns a new vector perpendicular to those two (portalforward ,portal right)
            var portalUp = -Vector3.Cross(portalForward,portalRight);
            //----------------------------------------------------------------------------------------------------
            //constructing the rotation 
            var portalRotation = Quaternion.LookRotation(portalForward, portalUp);

            // Attempt to place the portal.
            portals.Portals[portalID].PlacePortal(hit.collider, hit.point, portalRotation);

            crosshair.SetPortalPlaced(portalID,true );
        }
        
    }

}
