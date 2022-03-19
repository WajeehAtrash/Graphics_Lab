using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    public float interactionDistance = 2f;
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    private void Update()
    {
        
    }
    void InterActionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;
        bool hitSomething = false;
        if(Physics.Raycast(ray,out hit,interactionDistance))
        {
            IInteractble interactble = hit.collider.GetComponent<IInteractble>();
            if(interactble!=null)
            {
                hitSomething = true;
                interactionText.text = interactble.GetDescription();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactble.Interact();
                }
            }
        }
        interactionUI.SetActive(hitSomething);
    }
}
