using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour,IInteractble
{
    float IInteractble.MaxRange => 10.0f;
    public GameObject text;
    void IInteractble.OnEndHover()
    {
        text.SetActive(false);
    }

    void IInteractble.OnStartHover()
    {
        text.SetActive(true);
    }

}
