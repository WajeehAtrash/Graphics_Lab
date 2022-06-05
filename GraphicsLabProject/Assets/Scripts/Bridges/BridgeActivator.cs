using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivator : MonoBehaviour
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private bool initState;
    // Start is called before the first frame update
    void Start()
    {
        bridge.SetActive(initState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bridge.SetActive(!initState);
    }

    private void OnCollisionExit(Collision collision)
    {
        bridge.SetActive(initState);
    }
}
