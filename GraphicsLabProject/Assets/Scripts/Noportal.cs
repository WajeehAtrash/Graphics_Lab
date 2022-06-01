using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noportal : MonoBehaviour
{
    //TODO: make it more generic dont use the object name in the if statment
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("playerB"))
        {
            other.gameObject.GetComponent<PlayerMovement>().SetNoPortal(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("playerB"))
        {
            other.gameObject.GetComponent<PlayerMovement>().SetNoPortal(false);
        }
    }
}
