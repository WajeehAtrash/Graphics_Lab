using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour, IInteractble
{
    float IInteractble.MaxRange => 10.0f;
    public GameObject text;
    private bool interacting = false;

    void IInteractble.OnEndHover()
    {
        text.SetActive(false);
    }

    public void SetInteracting(bool val)
    {
        interacting = val;
    }

    void IInteractble.OnStartHover()
    {
        if (interacting == false)
        {
            text.SetActive(true);
            interacting = true;
        }
    }

}
