using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAMovement : MonoBehaviour
{
    [SerializeField] private float speed=5f;
    [SerializeField] private float jumpforce = 5f;
    [SerializeField]Transform groundCheck;
    private Rigidbody rBody;
    private float horizontalInput;
    private float verticalInput;
    private CapsuleCollider capsuleCollider;
    [SerializeField]private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rBody.velocity = new Vector3(horizontalInput * speed, rBody.velocity.y, verticalInput * speed);
     
        if (Input.GetKey(KeyCode.Space)&&IsGrounded())
        {
            Jump();
        }
    }
    private void Jump()
    {
        rBody.velocity = new Vector3(rBody.velocity.x, jumpforce, rBody.velocity.z);
    }

    private bool  IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
    }
}
