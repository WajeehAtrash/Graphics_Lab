using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolderMovment : MonoBehaviour
{
    // Start is called before the first frame update
    private CameraMovement cam;
    private float dist;
    void Start()
    {
        cam = transform.parent.GetComponentInChildren<CameraMovement>();
       
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(cam.transform.position, transform.position);
        float holderZ = transform.position.z;
        Vector3 direction = (-cam.transform.right - cam.transform.forward).normalized;
        Vector3 holderPosition = cam.transform.position + cam.transform.forward * dist;
        //holderPosition.z = holderZ;
        transform.position = holderPosition;
    }
}
