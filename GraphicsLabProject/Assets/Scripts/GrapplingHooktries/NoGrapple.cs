using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGrapple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GrapplingGun gGun = other.gameObject.GetComponent<GrapplingGun>();
        if(gGun!=null)
        {
            other.gameObject.GetComponent<GrapplingGun>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        GrapplingGun gGun = other.gameObject.GetComponent<GrapplingGun>();
        if (gGun != null)
        {
            other.gameObject.GetComponent<GrapplingGun>().enabled = true;
        }
    }
}
