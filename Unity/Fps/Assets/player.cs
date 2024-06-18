using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float m_Speed = 1f;
    public Vector3 m_jumpStrength = new Vector3(0, 50f, 0);
    int touchingGround = 0;
    bool onGround = false;
    Camera playerCamera;
    Vector2 rotation = new Vector2(0.0f,0.0f);
    public float mouseSensitivity = 1.0f;
    public float clampX = 30f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x * mouseSensitivity, 0, 0);
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Movement
        if (Input.GetAxis("Vertical") != 0.0f)
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.AddForce(Input.GetAxis("Vertical") * transform.forward * m_Speed);
        }

        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.AddForce(Input.GetAxis("Horizontal") * transform.right * m_Speed);
        }

        if (onGround && Input.GetButton("Jump"))
        {
            m_Rigidbody.AddForce(m_jumpStrength, ForceMode.Impulse);
        }


        // Camera and character rotation

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -clampX, clampX);
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
