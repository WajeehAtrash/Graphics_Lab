using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlate : MonoBehaviour
{
    private bool isOnFinish = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            isOnFinish = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOnFinish = false;
    }
    public bool GetIsOnFinish()
    {
        return isOnFinish;
    }
}
