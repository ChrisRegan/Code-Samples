using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float m_HoveringSpeed = 1f;
    public float m_WalkingSpeed = 1f;
    public Vector3 m_jumpStrength = new Vector3(0, 50f, 0);
    int touchingGround = 0;
    bool onGround = false;
    Camera playerCamera;
    Vector2 rotation = new Vector2(0.0f,0.0f);
    public float mouseSensitivity = 1.0f;
    public float clampX = 30f;
    public bool isHovering = false;
    public float dragIncrease = 100;
    private float drag;
    public float LookUpCap = 0.0f;
    public float LookDownCap = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        drag = m_Rigidbody.drag;
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x * mouseSensitivity, 0, 0);
    }

    private void Update()
    {

    }

    void ToggleHoverMode()
    {
        isHovering = !isHovering;
    }

    void HoveringMode()
    {
        m_Rigidbody.AddForce(Input.GetAxis("Vertical") * transform.forward * m_HoveringSpeed);
        m_Rigidbody.AddForce(Input.GetAxis("Horizontal") * transform.right * m_HoveringSpeed);
    }

    void WalkingMode()
    {

        Vector3 forward = Input.GetAxis("Vertical") * transform.forward;
        Vector3 side = Input.GetAxis("Horizontal") * transform.right;
        Vector3 direction = (forward + side) * m_WalkingSpeed;
        m_Rigidbody.MovePosition(transform.position + direction);

    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        // Movement
        if (!isHovering)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            WalkingMode();
        }
        else
        {
            m_Rigidbody.constraints = RigidbodyConstraints.None;
            HoveringMode();
        }


        if (onGround && Input.GetButton("Jump"))
        {
            m_Rigidbody.AddForce(m_jumpStrength, ForceMode.Impulse);
        }


        // Camera and character rotation

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, LookDownCap, LookUpCap);
        transform.eulerAngles = new Vector2(0, rotation.y) * mouseSensitivity;
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x * mouseSensitivity, 0, 0);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            touchingGround++;
        }

        onGround = touchingGround != 0;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            touchingGround--;
        }

        onGround = touchingGround != 0;
    }


}
