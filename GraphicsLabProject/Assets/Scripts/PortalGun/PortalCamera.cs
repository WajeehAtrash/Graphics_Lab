using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private Portal[] portals = new Portal[2];
    [SerializeField] private Camera portalCamera;
    [SerializeField] private Material portalMaterial;
    private Camera mainCamera;
    private RenderTexture tempTexture;
    private const int maskID1 = 1;
    private const int maskID2 = 2;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        tempTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalCamera.targetTexture = tempTexture;
    }

    // Start is called before the first frame update
    void Start()
    {
        portals[0].SetMaskId(maskID1);//using stencil reference calue of 1
        portals[1].SetMaskId(maskID2);//using stencil reference calue of 2
        //so the portals will not interfere with one another while rendering
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        /*
         * if the player can see part of the first portal then render that portal�s viewpoint Using a mask ID of 1
         * copy the portal texture to the main texture but only where the portal can be seen on the main texture
         * then do the same thing for the second portal and a mask ID of 2 
         * finally output the resulting main texture 
        */
        if (!portals[0].IsPlaced() || !portals[1].IsPlaced())
        {
            Graphics.Blit(source, destination);
            return;
        }

        if (portals[0].IsRendererVisible())
        {
            // Render the first portal output onto the image.
            RenderCamera(portals[0], portals[1]);
            portalMaterial.SetInt("_MaskID", maskID1);
            Graphics.Blit(tempTexture, source, portalMaterial);
        }
        if (portals[1].IsRendererVisible())
        {
            // Render the first portal output onto the image.
            RenderCamera(portals[1], portals[0]);
            portalMaterial.SetInt("_MaskID", maskID2);
            Graphics.Blit(tempTexture, source, portalMaterial);
        }

        Graphics.Blit(source, destination);

    }

    //rendering the view for the in portal
    private void RenderCamera(Portal inPortal, Portal outPortal)
    {
        Transform inTransform = inPortal.transform;
        Transform outTransform = outPortal.transform;

        // Position the camera behind the other portal.
        Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
        relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
        portalCamera.transform.position = outTransform.TransformPoint(relativePos);

        // Rotate the camera to look through the other portal.
        Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
        relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
        portalCamera.transform.rotation = outTransform.rotation * relativeRot;

        Plane p = new Plane(-outTransform.forward, outTransform.position);
        Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlane;
        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        portalCamera.projectionMatrix = newMatrix;

        // Render the camera to its render target.
        portalCamera.Render();
    }
}
