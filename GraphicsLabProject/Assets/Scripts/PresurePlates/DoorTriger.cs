using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject door;
    bool isOpened = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isOpened)
        {
            door.transform.position += new Vector3(0, 10, 0);
            isOpened = true;
        }
    }
}
