using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes

    public bool isGrounded;
    

    public float gravity = -12f;
    public float jumpHeight = 2;
    private Vector3 velocity;

    bool toggle = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;
    }

    private void FixedUpdate()
    {
        //Increase Speed
        if (toggle)
        {
            toggle = false;
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.1f * Time.fixedDeltaTime;
        }
        else
        {
            toggle = true;
            if (Time.timeScale < 2f)
                Time.timeScale += 0.005f * Time.fixedDeltaTime;
        }
    }

    void Update()
    {
        
        move.z = forwardSpeed;

        if(transform.position.y <= 0.8)
        {
            isGrounded = true;
        }
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Jump();
                isGrounded = false;
            }

        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
                          

        }   
        controller.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            
            if (desiredLane > 2)
            {
                desiredLane = 2;
            }

            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            
            if (desiredLane < 0)
            {
                desiredLane = 0;
            }
            
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;


        //transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(move * Time.deltaTime); 
    }

    private void Jump()
    {   
        controller.center = Vector3.zero;
        controller.height = 2;
   
        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.PointUp();
        }
        else if(other.gameObject.CompareTag("spawn"))
        {
            int index = Random.Range(0, SpawnManager.Instance.tilePrefabs.Length);
            SpawnManager.Instance.SpawnTile(index);
            SpawnManager.Instance.numberOfTile++;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.enabled = false;
        }
        
    }
    

    
    
}
