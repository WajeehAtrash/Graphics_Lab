using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coloredPresurePlate : MonoBehaviour
{
    [SerializeField] GameObject cube;
    private bool isPressed = false;

    protected void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.Equals(cube))
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

    public bool GetIsPressed()
    {
        return isPressed;
    }

    protected void OnTriggerExit(Collider other)
    {

        isPressed = false;
    }
}
