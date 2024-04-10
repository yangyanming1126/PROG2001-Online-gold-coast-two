using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float mouseSense = 250f;
    // Rigidbody of the player.
    private Rigidbody rb;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Initialize count to zero.
        count = 0;

        // Update the count display.
       // SetCountText();

        // Initially set the win text to be inactive.
        //winTextObject.SetActive(false);
    }


    private void Update()
    {
        
        
            //float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
            //float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;
            //Debug.Log(mouseX);
            ////rotate camera based on y input
            ////xRotation = xRotation - mouseY;
            //////limit camera rotation
            ////xRotation = Mathf.Clamp(xRotation, -70, 80);
            ////cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            //////rotate player based on the x input
            //transform.Rotate(Vector3.up * mouseX);
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2  m_Look = context.ReadValue<Vector2>();
        //Debug.Log(m_Look.x);
        transform.Rotate(Vector3.up * m_Look.x);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnMove(InputAction.CallbackContext movementValue)
    {
      
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.ReadValue<Vector2>();
      

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
        
    }

    

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = transform.right * movementX + transform.forward * movementY;   // new Vector3(movementX, 0.0f, movementY);
        //Debug.Log(movementY);
        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);



        
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            // Update the count display.
            SetCountText();
        }
    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 12)
        {
            // Display the win text.
            winTextObject.SetActive(true);
        }
    }
}