using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    protected bool isPressed = false;
    private List<GameObject> onPlateObjects = new List<GameObject>();
    public bool GetIsPressed()
    {
        return isPressed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if(obj!=null)
        { 
            onPlateObjects.Add(obj);
        }
        isPressed = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        var obj = collision.gameObject;
        if(onPlateObjects.Contains(obj))
        {
            onPlateObjects.Remove(obj);
        }
        if(onPlateObjects.Count==0)
            isPressed = false;
    }
}
