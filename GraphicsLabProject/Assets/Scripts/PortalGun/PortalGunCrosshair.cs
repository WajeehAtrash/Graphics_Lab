using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalGunCrosshair : crosshair
{
    [SerializeField]
    private PortalPair portalPair;

    [SerializeField]
    private Image inPortalImg;

    [SerializeField]
    private Image outPortalImg;

    private void Start()
    {


        inPortalImg.color = new Color(0,99,255);
        outPortalImg.color = new Color(255,192,0);

        inPortalImg.gameObject.SetActive(false);
        outPortalImg.gameObject.SetActive(false);
    }

    public void SetPortalPlaced(int portalID, bool isPlaced)
    {
        if (portalID == 0)
        {
            inPortalImg.gameObject.SetActive(isPlaced);
        }
        else
        {
            outPortalImg.gameObject.SetActive(isPlaced);
        }
    }
}
