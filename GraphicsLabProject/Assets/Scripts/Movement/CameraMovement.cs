using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float mouseSensitivity=100f;
    public Transform controledPlayer;
    public Transform unControledPlayer;
    private float xRotation = 0f;
    private const float cameraSpeed = 3.0f;
    private Rigidbody rBody;
    public Quaternion TargetRotation { private set; get; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;//locking the cursor into the middle of the screen
        TargetRotation = transform.rotation;
        rBody=GetComponentInParent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Time.delta time is the differnce between two calls for the function update
        //by multiplying by the delta we assure that we will not rotate faster than our fps rate
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //xRotation -= mouseY;//it's - because when we used + it's rotated to the opposet direction
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);//preventing the player from over rotating and lock behind the player
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //controledPlayer.Rotate(Vector3.up * mouseX);
        var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        var targetEuler = TargetRotation.eulerAngles + (Vector3)rotation * cameraSpeed;
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }
        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
        TargetRotation = Quaternion.Euler(targetEuler);

        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation,
            Time.deltaTime * 15.0f);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapPlayers();
        }
    }
    void SwapPlayers()//Todo: change grappler gun parameters so the effect will take place on the seconed player or disable it
    {
        Transform cam = controledPlayer.Find("Main Camera");
        cam.SetParent(unControledPlayer);
        cam.transform.localPosition = new Vector3(0, 1, 0);
        Transform temp = controledPlayer;
        controledPlayer = unControledPlayer;
        unControledPlayer = temp;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
    public void ResetRotation()
    {
        TargetRotation= Quaternion.LookRotation(transform.forward, Vector3.up);
    }
    public Quaternion GetCamRotation()
    {
        return transform.rotation;
    }
}
