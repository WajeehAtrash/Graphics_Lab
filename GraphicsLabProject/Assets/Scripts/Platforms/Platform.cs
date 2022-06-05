using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] protected GameObject platform;
    [SerializeField] protected Vector3 platformLoc;
    // Start is called before the first frame update
    void Start()
    {
        platform.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        platform.SetActive(true);
        platform.transform.position = platformLoc;
    }
}
