using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FashionThoughts {

    public class PlayerDash : MonoBehaviour {

        const string k_dashAnim = "Dash", k_idleAnim = "Idle";

        public float dashDistance = 14f;
        public float dashDuration = 0.15f;
        public float dashCooldown = 1f;

        private Rigidbody rb;
        private bool isDashing = false;
        private float cooldownTimer = 0f;

        [SerializeField] PlayerMovement movement;
        [SerializeField] Animator anim;

        public bool IsDashing => isDashing;

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            cooldownTimer -= Time.deltaTime;
        }

        public void OnSprint( InputValue value ) {
            if (value.isPressed && !isDashing && cooldownTimer <= 0f)
                StartCoroutine( Dash() );
        }

        IEnumerator Dash() {
            anim.Play( k_dashAnim );
            isDashing = true;
            cooldownTimer = dashCooldown;
            Debug.Log( "Dash executado" );

            Vector3 startPosition = rb.position;
            Vector3 targetPosition = startPosition + (movement.LastDirection * dashDistance);
            float elapsed = 0f;

            rb.linearVelocity = Vector3.zero;

            while (elapsed < dashDuration) {
                elapsed += Time.fixedDeltaTime;
                float t = Mathf.Clamp01( elapsed / dashDuration );
                rb.MovePosition( Vector3.Lerp( startPosition, targetPosition, t ) );
                yield return new WaitForFixedUpdate();
            }

            rb.MovePosition( targetPosition );
            rb.linearVelocity = Vector3.zero;
            isDashing = false;
            movement.SetDirection( movement.LastRegisteredDirection );
            //anim.Play( k_idleAnim );
        }
    }
}
