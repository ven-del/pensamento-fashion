using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 8, -20); // ajusta pra visão isométrica
    public float smoothSpeed = 5f;
    public float dashFollowMultiplier = 0.35f;
    public float rotationSmoothSpeed = 6f;

    private PlayerDash playerDash;

    void Start()
    {
        if (target != null)
            playerDash = target.GetComponent<PlayerDash>();
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        bool isDashing = playerDash != null && playerDash.IsDashing;
        float followSpeed = isDashing ? smoothSpeed * dashFollowMultiplier : smoothSpeed;

        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);

        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}