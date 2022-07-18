using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredDoor : MonoBehaviour
{
    [SerializeField] List<coloredPresurePlate> list;
    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool toOpen = true;
        foreach(coloredPresurePlate presure in list)
        {
            if (!presure.GetIsPressed())
            {
                toOpen = false;
            }
        }
        if(toOpen&& !isOpened)
        {
            transform.position += new Vector3(0, 10, 0);
            isOpened = true;
        }
        if(isOpened && !toOpen)
        {
            transform.position -= new Vector3(0, 10, 0);
        }
    }
}
