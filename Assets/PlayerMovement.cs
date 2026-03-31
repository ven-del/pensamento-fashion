using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private PlayerDash playerDash;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDash = GetComponent<PlayerDash>();
        rb.freezeRotation = true;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (playerDash != null && playerDash.IsDashing)
            return;

        Vector3 dir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (dir != Vector3.zero)
            transform.forward = dir;
    }
}