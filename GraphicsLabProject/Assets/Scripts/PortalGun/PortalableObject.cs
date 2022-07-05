using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PortalableObject : MonoBehaviour
{
    private Portal inPortal;
    private Portal outPortal;
    private GameObject cloneObject;

    private int inPortalCount = 0;
    private new Rigidbody rigidbody;
    protected new Collider collider;

    //const variable (variables of type quatrnion can't be const so we declared it as a readonly)
    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    protected virtual void Awake()
    {
        cloneObject = new GameObject();
        cloneObject.SetActive(false);
        var meshFilter = cloneObject.AddComponent<MeshFilter>();
        var meshRenderer = cloneObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer.materials = GetComponent<MeshRenderer>().materials;
        cloneObject.transform.localScale = transform.localScale;

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        if (inPortal == null || outPortal == null)
        {
            return;
        }

        if (cloneObject.activeSelf && inPortal.IsPlaced() && outPortal.IsPlaced())
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            // Update position of clone.
            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            cloneObject.transform.position = outTransform.TransformPoint(relativePos);

            // Update rotation of clone.
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            cloneObject.transform.rotation = outTransform.rotation * relativeRot;
        }
        else
        {
            cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
        }
    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        this.inPortal = inPortal;
        this.outPortal = outPortal;
        if(transform.tag.Equals("Player"))
        {
            PlayerMovement pM = transform.GetComponent<PlayerMovement>();
            
            if(!(inPortal.GetForwardNormal().Equals(new Vector3(1.00f, 0.00f, 0.00f))&&pM.IsGrounded()))
            {
                Physics.IgnoreCollision(collider, wallCollider);
            }
            else
            {
                pM.ApplyVerticalForce(0.7f);
                Physics.IgnoreCollision(collider, wallCollider);
            }
        }
        else
            Physics.IgnoreCollision(collider, wallCollider);
        cloneObject.SetActive(true);

        ++inPortalCount;
    }

    public void ExitPortal(Collider wallCollider)
    {
        Physics.IgnoreCollision(collider, wallCollider, false);
        --inPortalCount;
        if (inPortalCount == 0)
        {
            cloneObject.SetActive(false);
        }
        Pickupable pickupable = transform.GetComponent<Pickupable>();
        if (pickupable != null)
        {
            PickUp player = pickupable.GetComponentInParent<PickUp>();
            if (player != null)
                player.DropObject();
        }
    }

    public virtual void Warp()
    {
        var inTransform = inPortal.transform;
        var outTransform = outPortal.transform;

        // Update position of object.
        Vector3 relativePos = inTransform.InverseTransformPoint(transform.position); //Transforms position from world space to local space.
        relativePos = halfTurn * relativePos;//rotating the object to face the sxit 
        transform.position = outTransform.TransformPoint(relativePos);

        // Update rotation of object.
        Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
        relativeRot = halfTurn * relativeRot;
        transform.rotation = outTransform.rotation * relativeRot;

        // Update velocity of rigidbody.
        Vector3 relativeVel = inTransform.InverseTransformDirection(rigidbody.velocity);//Transforms a direction from world space to local space
        relativeVel = halfTurn * relativeVel;
        rigidbody.velocity = outTransform.TransformDirection(relativeVel);

        // Swap portal references.
        var tmp = inPortal;
        inPortal = outPortal;
        outPortal = tmp;
    }
}