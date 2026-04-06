using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FashionThoughts {

    public class PlayerAttack : MonoBehaviour {

        const string k_attackAnim = "Attack";

        [SerializeField] PlayerMovement movement;
        [SerializeField] Animator anim;
        [SerializeField] Collider targetCollider;
        [SerializeField] float collderAliveTime = .3f;

        public bool IsAttacking { get; private set; }

        Camera _cam;
        CancellationTokenSource _tokenSource;

        private void Awake() => _cam = Camera.main;
        private void OnDisable() => DisposeToken();

        public void OnAttack( InputValue value ) {
            if (value.isPressed)
                Attack();
        }

        void Attack() {
            var mp = Mouse.current.position.ReadValue();
            if (!Physics.Raycast( _cam.ScreenPointToRay( mp ), out var hit, 1000 )) return;
            var dir = ( hit.point - transform.position ).normalized;
            targetCollider.transform.LookAt( transform.position + dir, Vector3.up );
            AttackCollider().Forget();
        }

        async UniTask AttackCollider() {
            _tokenSource = new CancellationTokenSource();
            IsAttacking = true;
            anim.Play( k_attackAnim );
            try {
                targetCollider.enabled = true;
                await UniTask.WaitForSeconds( collderAliveTime, cancellationToken: _tokenSource.Token );
            } catch { }
            IsAttacking = false;
            DisposeToken();
            targetCollider.enabled = false;
            movement.SetDirection( movement.LastRegisteredDirection );
        }

        void DisposeToken() {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
        }
    }
}
