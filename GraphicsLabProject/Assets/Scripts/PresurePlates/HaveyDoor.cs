using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveyDoor : MonoBehaviour
{
    [SerializeField] private PresurePlate p1;
    [SerializeField] private PresurePlate p2;
    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(p1.GetIsPressed()&&p2.GetIsPressed()&&!isOpened)
        {
            pos.y += 25;
            transform.position = pos;
            isOpened = true;
        }
        else if(isOpened&&(!p1.GetIsPressed()||!p2.GetIsPressed()))
        {
            pos.y -= 25;
            transform.position = pos;
            isOpened = false;
        }
    }
}
