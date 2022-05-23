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
        
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement parent = GetComponentInParent<PlayerMovement>();
        Debug.Log(parent);
        if(parent.GetIsControlled()==true)
        {
            cam = transform.parent.GetComponentInChildren<CameraMovement>();
            dist = Vector3.Distance(cam.transform.position, transform.position);
            Vector3 holderPosition = cam.transform.position + -cam.GetDirection() * dist;

            transform.position = holderPosition;
        }
    }
}
