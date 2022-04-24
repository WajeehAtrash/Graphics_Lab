using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycasting : MonoBehaviour
{
    [SerializeField] private float range=10.0f;
    private IInteractble currentTarget;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastForInteractable();
    }

    private void RaycastForInteractable()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit,range))
        {
            IInteractble interactble = hit.collider.GetComponent<IInteractble>();
            if(interactble!=null)
            {
                if(hit.distance<=interactble.MaxRange)
                {
                    if(interactble==currentTarget)
                    {
                        return;
                    }
                    else if(currentTarget!=null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = interactble;
                        currentTarget.OnStartHover();
                        return;
                    }
                    else
                    {
                        currentTarget = interactble;
                        currentTarget.OnStartHover();
                        return;
                    }
                }
                else
                {
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }
    }
}
