using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviourPun
{
    public float moveSpeed = 5f;  // Movement speed
    private Rigidbody rb;
    private Vector3 input;

    // Camera follow
    private Transform playerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Camera and audio for remote players
        if (!photonView.IsMine)
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null) cam.gameObject.SetActive(false);

            AudioListener audio = GetComponentInChildren<AudioListener>();
            if (audio != null) audio.enabled = false;
        }
        else
        {
            // Local player camera reference
            playerCamera = GetComponentInChildren<Camera>().transform;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;  // Only local player controls

        // Get input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        input = new Vector3(h, 0f, v).normalized;

        // Camera follow update
        if (playerCamera != null)
        {
            playerCamera.position = transform.position + new Vector3(0, 3, -5); // Adjust offset
            playerCamera.LookAt(transform.position + Vector3.up); // Look at player head
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        // Move player
        Vector3 move = input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(move));
    }
}
