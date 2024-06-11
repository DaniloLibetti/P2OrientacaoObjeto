using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask groundLayer;

    public GameObject winGameScreen;

    bool isGrounded;

    int speed = 8;
    int jumpForce = 5;

    float mouseSense = 1f;
    float mouseX, mouseY;


    void Start()
    {
        GameManager.Instance.life = 30;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        //movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 inputs = new Vector3(moveX, 0f, moveZ).normalized;
        Vector3 movement = transform.TransformDirection(inputs) * speed;
        
        movement.y = playerRB.velocity.y;
        
        playerRB.velocity = movement;

        //mouse
        mouseX += Input.GetAxis("Mouse X") * mouseSense;
        mouseY += -Input.GetAxis("Mouse Y") * mouseSense;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.rotation = Quaternion.Euler(0f, mouseX, 0f);

        playerCamera.transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);

        //Jump

        isGrounded = IsGrounded();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, jumpForce, playerRB.velocity.z);
        }
    }

    private bool IsGrounded()
    {

        return Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Win"))
        {
            winGameScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
