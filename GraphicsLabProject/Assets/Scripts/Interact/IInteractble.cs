using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractble
{
    float MaxRange { get; }

    void OnStartHover();
    //void OnInteract();
    void OnEndHover();
}
