using UnityEngine;
using Unity.Cinemachine;
public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Rigidbody capsuleRigidBody;
    [SerializeField] public Collider capsuleCollider;
    [SerializeField] public float capsuleSpeed;
    [SerializeField] public float distToGround, jumpSpeed,jumpGain,doubleInputCD, doubleJumpCD,dashSpeed, dashGain,gravityGain, frictionGain,maxSpeed;
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private CheckGround checker;
    private float defaultspeed;
    private int jumpCharges = 1;
    private bool isGrounded;
    void Start()
    {
        defaultspeed = capsuleSpeed;
        distToGround = capsuleCollider.bounds.extents.y;
        
    }

    // Update is called once per frame
    void Update()
    { 
        isGrounded = checker.isOnground;
        if (isGrounded)
        {
            capsuleCollider.material.dynamicFriction = frictionGain;
        }
        else {
            capsuleCollider.material.dynamicFriction = 0.1f*frictionGain;
        }
            transform.forward = freeLookCamera.transform.forward;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
       
        capsuleSpeed = isGrounded? defaultspeed :  1;
        doubleInputCD = Mathf.Max(0f, doubleInputCD - Time.deltaTime);
        doubleJumpCD = Mathf.Max(0f, doubleJumpCD - Time.deltaTime);
        Vector2 inputVector = Vector2.zero; // intialize our input vector
        jumpSpeed = 0;
        dashSpeed = 0;
        
        if (Input.GetKey(KeyCode.W))
            {
            inputVector += Vector2.up; // "a += b" <=> "a = a + b"
            }
        
        if (Input.GetKey(KeyCode.S))
             {
           inputVector += Vector2.down;
            }
        
        if (Input.GetKey(KeyCode.D))
             {
             inputVector += Vector2.right;
             }
        
        if (Input.GetKey(KeyCode.A))
             {
             inputVector += Vector2.left;
             }
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded && doubleInputCD <=0)
        {
            jumpSpeed = 1;
            doubleInputCD = 0.05f;
            doubleJumpCD = 0.2f;
            jumpCharges = 1;
            
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && doubleInputCD <= 0 && doubleJumpCD <=0 && jumpCharges>0)
        {
            jumpSpeed = 1.25f;
            doubleInputCD = 0.05f;
            jumpCharges =0;
            capsuleRigidBody.linearVelocity = new Vector3(capsuleRigidBody.linearVelocity.x,0,capsuleRigidBody.linearVelocity.z);
        }
        if (Input.GetKey(KeyCode.LeftShift) && doubleInputCD <= 0 && doubleJumpCD <= 0 && jumpCharges > 0)
        {
            dashSpeed = 1;
            doubleInputCD = 0.05f;
            jumpCharges = 0;
        }


        Vector3 inputXYZPlane = new Vector3(inputVector.x, 0, inputVector.y).normalized;
        Debug.Log("Resultant Vector: " + inputXYZPlane);
        if (capsuleRigidBody.linearVelocity.magnitude < maxSpeed) {
            capsuleRigidBody.AddRelativeForce(inputXYZPlane * capsuleSpeed);
        }
        

        capsuleRigidBody.AddForce(0, jumpSpeed * jumpGain, 0, ForceMode.VelocityChange);
        capsuleRigidBody.AddRelativeForce(0,0,dashSpeed*dashGain, ForceMode.Impulse);
    }
    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity*gravityGain, ForceMode.Acceleration);
    }

   



}
