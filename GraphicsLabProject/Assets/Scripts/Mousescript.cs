using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousescript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float mouseSensitivity=100f;
    public Transform controledPlayer;
    public Transform unControledPlayer;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//locking the cursor into the middle of the screen
    }

    // Update is called once per frame
    void Update()
    {
        //Time.delta time is the differnce between two calls for the function update
        //by multiplying by the delta we assure that we will not rotate faster than our fps rate
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;//it's - because when we used + it's rotated to the opposet direction
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//preventing the player from over rotating and lock behind the player
        transform.localRotation = Quaternion.Euler(xRotation,0f, 0f);
        controledPlayer.Rotate(Vector3.up * mouseX);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapPlayers();
        }
    }
    void SwapPlayers()
    {
        Transform cam = controledPlayer.Find("Main Camera");
        cam.SetParent(unControledPlayer);
        cam.transform.localPosition = new Vector3(0, 1, 0);
        Transform temp = controledPlayer;
        controledPlayer = unControledPlayer;
        unControledPlayer = temp;
    }
}
