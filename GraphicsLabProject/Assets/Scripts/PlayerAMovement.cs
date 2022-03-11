using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAMovement : MonoBehaviour
{
    [SerializeField] private float speed=5f;
    [SerializeField] private float jumpforce = 5f;
    private Rigidbody rBody;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rBody.velocity = new Vector3(horizontalInput * speed, rBody.velocity.y, verticalInput * speed);
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        rBody.velocity = new Vector3(rBody.velocity.x, jumpforce, rBody.velocity.z);
    }
}
