using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PortalableObject
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpforce = 15f;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject player;
    private Rigidbody rBody;
    private float horizontalInput;
    private float verticalInput;
    private CapsuleCollider capsuleCollider;
    private bool isControled = true;
    private CameraMovement cam;
    protected override void Awake()
    {
        base.Awake();
        cam = GetComponent<CameraMovement>();
    }
    public override void Warp()
    {
        base.Warp();
    }
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        if (player.name == "playerB")
            isControled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isControled)
        {
            //--------------watch this video for more info:
            //https://www.youtube.com/watch?v=C70QxpI9F5Y
            Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed;//getting the directions of the movement
            Vector3 forwad = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);//the direction the camera looks at
            Vector3 wishDirector = (forwad * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rBody.velocity.y);//final calculation ,where to move and distance
            rBody.velocity = wishDirector;
            rBody.rotation = cam.transform.rotation;
            if (Input.GetKey(KeyCode.Space) && IsGrounded())
            {
                Jump();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isControled = !isControled;
        }

    }
    private void Jump()
    {
        rBody.velocity = new Vector3(rBody.velocity.x, jumpforce, rBody.velocity.z);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }
    public bool GetIsControlled()
    {
        return isControled;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
