using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriger2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject door;
    private void OnTriggerEnter(Collider other)
    {   
       door.transform.position += new Vector3(0, 10, 0); 
    }
    private void OnTriggerExit(Collider other)
    {
        door.transform.position += new Vector3(0, -10, 0);
    }

}
