using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    Transform orientation;
    Transform playerTransform;
    Transform playerObject;

    Rigidbody rb;
    public float rotationSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        playerTransform = player.transform;
        orientation = player.GetComponent<PlayerMovement>().getOrientation();
        playerObject = player.GetComponent<PlayerMovement>().getPlayerObject().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, rotationSpeed * Time.deltaTime);

    }
}
