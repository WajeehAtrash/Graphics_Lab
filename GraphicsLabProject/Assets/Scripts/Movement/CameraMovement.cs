using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private float mouseSensitivity=100f;
    [SerializeField] Transform place;
    public Transform controledPlayer;
    public Transform unControledPlayer;
    private const float cameraSpeed = 3.0f;
    private Rigidbody rBody;
    public Quaternion TargetRotation { private set; get; }
    [SerializeField] private crosshair portalCrosshair;
    [SerializeField] private crosshair grappleCrosshair;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;//locking the cursor into the middle of the screen
        TargetRotation = transform.rotation;
        rBody = GetComponentInParent<Rigidbody>();
        SetupPlayers();
    }

    void Start()
    {
        if (transform.parent.name.Equals("playerB"))
        {
            transform.GetComponent<PortalPlacement>().enabled = true;
            portalCrosshair.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        var targetEuler = TargetRotation.eulerAngles + (Vector3)rotation * cameraSpeed;
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }

        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
        TargetRotation = Quaternion.Euler(targetEuler);

        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation,Time.deltaTime * 15.0f);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapPlayers();
        }

        SetupPlayers();
    }

    void SwapPlayers()//Todo: change grappler gun parameters so the effect will take place on the seconed player or disable it
    {
        
        CameraMovement cam = GetComponentInChildren<CameraMovement>();
        cam.transform.SetParent(unControledPlayer);
        cam.transform.localPosition = new Vector3(0, 1, 0);
        Transform temp = controledPlayer;
        controledPlayer = unControledPlayer;
        unControledPlayer = temp;
        if (transform.parent.name.Equals("playerB"))
        {
            portalCrosshair.gameObject.SetActive(true);
            grappleCrosshair.gameObject.SetActive(false);
        }
        else
        {
            portalCrosshair.gameObject.SetActive(false);
            grappleCrosshair.gameObject.SetActive(true);
        }
    }
 
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public void ResetRotation()
    {
        TargetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }

    public Quaternion GetCamRotation()
    {
        return transform.rotation;
    }

    private void SetupPlayers()
    {
        GameObject GrapplingGun = FindByName(this.gameObject, "GunPosition");

        if (transform.parent.name.Equals("playerA"))
        {
            transform.GetComponent<PortalPlacement>().enabled = false;
            portalCrosshair.gameObject.SetActive(false);
            if (GrapplingGun != null)
            {
                grappleCrosshair.gameObject.SetActive(true);
                GrapplingGun.SetActive( true);
            }
        }
        if (transform.parent.name.Equals("playerB"))
        {
            transform.GetComponent<PortalPlacement>().enabled = true;
            portalCrosshair.gameObject.SetActive(true);
            if (GrapplingGun != null)
            {
                GrapplingGun.SetActive (false);
                grappleCrosshair.gameObject.SetActive(false);
            }
        }
    }

    public GameObject FindByName(GameObject parent, string name)
    {
        if (parent.name == name) return parent;
        foreach (Transform child in parent.transform)
        {
            GameObject result = FindByName(child.gameObject, name);
            if (result != null) return result;
        }
        return null;
    }

    public Vector3 GetDirection()
    {
        return (transform.position - place.position).normalized;
    }
}
