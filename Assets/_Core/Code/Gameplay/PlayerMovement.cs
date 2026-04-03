using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FashionThoughts {
    public class PlayerMovement : MonoBehaviour {

        const string k_idleAnim = "Idle", k_walkAnim = "Walk";

        public float speed = 5f;
        private Rigidbody rb;
        private Vector2 moveInput;
        private PlayerDash playerDash;
        [SerializeField] Transform model;
        [SerializeField] float turnSpeed = .25f;
        [SerializeField] Animator anim;

        Camera _cam;
        Tweener _turnTween;
        float _scale;
        Vector3 _lastDirection;

        public Vector2 LastRegisteredDirection { get; private set; }
        public Vector3 LastDirection => _lastDirection;

        void Start() {
            _cam = Camera.main;
            rb = GetComponent<Rigidbody>();
            playerDash = GetComponent<PlayerDash>();
            rb.freezeRotation = true;
            _scale = model.transform.localScale.x;
        }

        public void OnMove( InputValue value ) => SetDirection( value.Get<Vector2>() );

        internal void SetDirection( Vector2 newDir ) {
            LastRegisteredDirection = newDir;
            if (playerDash != null && playerDash.IsDashing)
                return;
            var requestTurn = ( moveInput.x >= 0 && newDir.x < 0 ) || ( moveInput.x <= 0 && newDir.x > 0 );
            moveInput = newDir;
            anim.Play( moveInput.magnitude > 0 ? k_walkAnim : k_idleAnim );
            if (!requestTurn) return;
            _turnTween?.Kill();
            _turnTween = model.DOScale( new Vector3( moveInput.x < 0 ? -_scale : _scale, model.localScale.y, model.localScale.z ), turnSpeed );
        }

        void FixedUpdate() {
            if (playerDash != null && playerDash.IsDashing)
                return;

            Vector3 dir = ( _cam.transform.right * moveInput.x ) + ( _cam.transform.forward * moveInput.y );
            if (moveInput.magnitude > 0)
                _lastDirection = ( new Vector3( dir.x, 0, dir.z ) ).normalized;
            rb.linearVelocity = speed * ( new Vector3( dir.x, 0, dir.z ) ).normalized;
        }
    }
}
