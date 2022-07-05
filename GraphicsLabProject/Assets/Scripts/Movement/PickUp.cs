using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public float pickUpRange = 10.0f;
    private GameObject heldObj;
    [SerializeField] float objectDrag = 10.0f;
    [SerializeField] float moveForce = 250.0f;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private Transform holdParent;
    private PlayerMovement player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&player.GetIsControlled()==true)
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast( ray,out hit, pickUpRange, pickUpLayer))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
    }
    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }
    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRigidbody = pickObj.GetComponent<Rigidbody>();
            PortalableObject portalObject = pickObj.GetComponent<PortalableObject>();
            //if(portalObject!=null)
            //{
            //    pickObj.GetComponent<PortalableObject>().enabled = false;
            //}
            objRigidbody.useGravity = false;
            objRigidbody.drag = 10;
            objRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            objRigidbody.drag = objectDrag;
            objRigidbody.transform.parent = holdParent;
            heldObj = pickObj;
            Pickupable obj = heldObj.GetComponent<Pickupable>();
            obj.SetInteracting(true);
        }
    }
    public void DropObject()
    {
        PortalableObject portalObject = heldObj.GetComponent<PortalableObject>();
        Rigidbody rBody = heldObj.GetComponent<Rigidbody>();
        rBody.useGravity = true;
        rBody.drag = 1;
        rBody.constraints = RigidbodyConstraints.None;
        Pickupable obj = heldObj.GetComponent<Pickupable>();
        obj.SetInteracting(false);
        heldObj.transform.parent = null;
        heldObj = null;
        
    }
    private void Awake()
    {
        player = transform.GetComponent<PlayerMovement>();
    }
}
