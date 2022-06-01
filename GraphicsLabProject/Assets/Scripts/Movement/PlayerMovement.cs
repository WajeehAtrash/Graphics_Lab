using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool noPortal = false;

    protected override void Awake()
    {
        base.Awake();
        cam = GetComponentInChildren<CameraMovement>();
    }

    public override void Warp()
    {
        base.Warp();
        cam.ResetRotation();
        transform.rotation = new Quaternion(0, 0, 0, 0);
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
        cam = GetComponentInChildren<CameraMovement>();
        if (isControled)
        {
            //--------------watch this video for more info:
            //https://www.youtube.com/watch?v=C70QxpI9F5Y
            Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed;//getting the directions of the movement
            Vector3 forwad = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);//the direction the camera looks at
            Vector3 wishDirector = (forwad * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rBody.velocity.y);//final calculation ,where to move and distance
            rBody.velocity = wishDirector;
            if (Input.GetKey(KeyCode.Space) && IsGrounded())
            {
                Jump();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            isControled = !isControled;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    public void SetNoPortal(bool val)
    {
        this.noPortal = val;
    }
    public bool GetNoPortal()
    {
        return this.noPortal;
    }
}
