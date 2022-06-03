using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    private bool isPressed = false;
    public bool GetIsPressed()
    {
        return isPressed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter pre");
        isPressed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit pre");
        isPressed = false;
    }
}
