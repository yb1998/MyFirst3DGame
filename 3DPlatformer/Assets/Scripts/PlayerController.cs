using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody rig;
    private AudioSource audioSource;

    void Awake()
    {
        //get the rigidbody component
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if(!GameManager.instance.paused)
        {
            Move();

            if(Input.GetButtonDown("Jump"))
            {
                TryJump();
            }
        }
    }

    void Move()
    {
        //getting the inputs
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        //calculating direction to move in
        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        dir.y = rig.velocity.y;

        //setting the rigidbody velocity
        rig.velocity = dir;

        Vector3 facingDir = new Vector3 (xInput, 0, zInput);

        if(facingDir.magnitude > 0)
        {
            transform.forward = facingDir;
        }        
    }

    void TryJump()
    {
        bool grounded = false;

        Ray[] rays = {new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down), new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down), 
                    new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down), new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down)};

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, 0.7f))
            {
                grounded = true;
            }
        }

        if (grounded)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
        else if(other.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(1);
            Destroy(other.gameObject);
            audioSource.Play();
        }
        else if(other.CompareTag("Goal"))
        {
            GameManager.instance.LevelEnd();
        }
    }
}
