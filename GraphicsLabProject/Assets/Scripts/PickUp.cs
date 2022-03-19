using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour,IInteractble
{
    // Start is called before the first frame update
    public float pickUpRange = 5.0f;
    private GameObject heldObj;
    [SerializeField] float objectDrag = 10.0f;
    [SerializeField] float moveForce = 250.0f;
    [SerializeField] private LayerMask pickUpLayer;
    public Transform holdParent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, pickUpLayer))
                {
                    Debug.Log("Press E yo pickUp");//change to text on the screen
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
            objRigidbody.useGravity = false;
            objRigidbody.drag = objectDrag;
            objRigidbody.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }
    void DropObject()
    {
        Rigidbody rBody = heldObj.GetComponent<Rigidbody>();
        rBody.useGravity = true;
        rBody.drag = 1;
        heldObj.transform.parent = null;
        heldObj = null;
    }

    public void Interact()
    {
        return;
    }

    public string GetDescription()
    {
        return "Press E to Pick Up";
    }
}
