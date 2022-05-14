using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float mouseSensitivity=100f;
    public Transform controledPlayer;
    public Transform unControledPlayer;
    private const float cameraSpeed = 3.0f;
    private Rigidbody rBody;
    public Quaternion TargetRotation { private set; get; }

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

        targetEuler.x = Mathf.Clamp(targetEuler.x, -90.0f, 90.0f);
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
        if(cam==null)
        {
            Debug.Log("camera is null");
        }
        cam.transform.SetParent(unControledPlayer);
        cam.transform.localPosition = new Vector3(0, 1, 0);
        Transform temp = controledPlayer;
        controledPlayer = unControledPlayer;
        unControledPlayer = temp;
    }
 
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public Quaternion ResetRotation()
    {
        TargetRotation= Quaternion.LookRotation(transform.forward, Vector3.up);
        return TargetRotation;
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

            if (GrapplingGun != null)
            {
                GrapplingGun.SetActive( true);
            }
        }
        if (transform.parent.name.Equals("playerB"))
        {
            transform.GetComponent<PortalPlacement>().enabled = true;
            if (GrapplingGun != null)
            {
                GrapplingGun.SetActive (false);
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
}
