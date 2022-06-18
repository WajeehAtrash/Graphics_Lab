using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGrapple : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.name.Equals("playerA"))
        {
            GrapplingGun gGun = other.gameObject.GetComponentInChildren<GrapplingGun>();
            if (gGun != null)
            {
                other.gameObject.GetComponentInChildren<GrapplingGun>().enabled = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.name.Equals("playerA"))
        {
            GrapplingGun gGun = other.gameObject.GetComponentInChildren<GrapplingGun>();
            if (gGun != null)
            {
                other.gameObject.GetComponentInChildren<GrapplingGun>().enabled = true;
            }
        }
    }
}
