using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private bool grounded = true;
    private bool canDoubleJump = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0; 
        rb = GetComponent <Rigidbody>(); 
        SetCountText();
        winTextObject.SetActive(false);
    }

    

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>(); 

        movementX = movementVector.x; 
        movementY = movementVector.y;

    }

    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= 9)
       {
           winTextObject.SetActive(true);
       }
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed); 

        if (Input.GetButtonDown("Jump")) 
        {
            if (grounded) 
            {
                rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                grounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump) 
            {
                rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                canDoubleJump = false;
            }
            // rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        canDoubleJump = true;
    }

    void OnTriggerEnter(Collider other) 
    {
        
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    
        other.gameObject.SetActive(false);
        
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
