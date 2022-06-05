using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsV2 : Platform
{
    [SerializeField] private Vector3 initLocation;

    void Start()
    {
        platform.SetActive(false);
    }

    private void OnCollisionExit(Collision collision)
    {
        platform.SetActive(false);
        platform.transform.position = initLocation;
    }
}
