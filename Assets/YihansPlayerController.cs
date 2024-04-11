using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class YihansPlayerController : MonoBehaviour
{
    Animator animator;

    public float speed = 7f;

    public TextMeshProUGUI countText;

    private float count;

    public GameObject winPanel;


    //New variable here - for the movement
    [SerializeField] float mouseSense = 250f;
    private Camera cam;
    private float xRotation = 0f;
    private CharacterController characterController;

    //Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        count = 0;
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }else if (other.gameObject.CompareTag("Death"))
        {
            other.gameObject.SetActive(false);
            count -= 1;

            SetCountText();

            animator.SetTrigger("touchOfDead");
        }
    }

    public void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        CheckScore();
    }

    public void CheckScore()
    {
        if (count >= 5)
        {
            winPanel.SetActive(true);
        }
    }

    void Update()
    {
        bool forward = Input.GetKey("w");
        
        bool jumped = Input.GetKey("h");
        
        if (jumped)
        {
            animator.SetBool("Jumping", true);
        }
        if(!jumped)
        {
            animator.SetBool("Jumping", false);
        }

        if (forward )
        {
            animator.SetBool("Walking", true);
        }
        if (!forward)
        {
            animator.SetBool("Walking", false);
        }

        //new script code

        float horizontal = Input.GetAxis("Horizontal");

        float verticle = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * verticle + transform.right * horizontal;
        characterController.Move(movement * Time.deltaTime * speed);

        bool rightButton = Input.GetMouseButton(1);

        if (rightButton)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;
            //rotate camera based on y input 
            xRotation = xRotation - mouseY;
            //limit camera rotation
            xRotation = Mathf.Clamp(xRotation, -70, 80);
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            //rotate player based on the x input
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
