using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    protected bool isPressed = false;
    public bool GetIsPressed()
    {
        return isPressed;
    }
    protected void OnTriggerEnter(Collider other)
    {
        
        isPressed = true;
    }

    protected void OnTriggerExit(Collider other)
    {
        
        isPressed = false;
    }
}
