using UnityEngine;
using Unity.Cinemachine;
public class Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Rigidbody capsuleRigidBody;
    [SerializeField] public Collider capsuleCollider;
    [SerializeField] public float capsuleSpeed;
    [SerializeField] public float distToGround, jumpSpeed,jumpGain,doubleInputCD, doubleJumpCD,dashSpeed, dashGain;
    [SerializeField] private CinemachineCamera freeLookCamera;
    private float defaultspeed;
    private int jumpCharges = 1;
    
    void Start()
    {
        defaultspeed = capsuleSpeed;
        distToGround = capsuleCollider.bounds.extents.y;
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.forward = freeLookCamera.transform.forward;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
       
        capsuleSpeed = isGrounded()? defaultspeed :  defaultspeed/20;
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
        if (Input.GetKey(KeyCode.Space)&& isGrounded() && doubleInputCD <=0)
        {
            jumpSpeed = 1;
            doubleInputCD = 0.05f;
            doubleJumpCD = 0.3f;
            jumpCharges = 1;
        }

        if (Input.GetKey(KeyCode.Space) && !isGrounded() && doubleInputCD <= 0 && doubleJumpCD <=0 && jumpCharges>0)
        {
            jumpSpeed = 1.25f;
            doubleInputCD = 0.05f;
            jumpCharges =0;
        }
        if (Input.GetKey(KeyCode.LeftShift) && doubleInputCD <= 0 && doubleJumpCD <= 0 && jumpCharges > 0)
        {
            dashSpeed = 1;
            doubleInputCD = 0.05f;
            jumpCharges = 0;
        }


        Vector3 inputXYZPlane = new Vector3(inputVector.x, 0, inputVector.y).normalized;
        Debug.Log("Resultant Vector: " + inputXYZPlane);
        capsuleRigidBody.AddRelativeForce(inputXYZPlane*capsuleSpeed);
        capsuleRigidBody.AddForce(0, jumpSpeed*jumpGain, 0,ForceMode.VelocityChange);
        capsuleRigidBody.AddRelativeForce(0,0,dashSpeed*dashGain, ForceMode.Impulse);
    }

    bool isGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
    }
 
        
    
}
