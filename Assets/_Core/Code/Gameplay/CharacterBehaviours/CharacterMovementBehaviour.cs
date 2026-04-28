using UnityEngine;

namespace FashionThoughts {

    public class CharacterMovementBehaviour : CharacterBehaviourBase {

        [SerializeField] float speed;

        Camera _cam;
        Vector2 _moveInput;

        void Awake() {
            Active = true;
            _cam = Camera.main;
        }

        internal override void Initialize( Character source ) { 
            base.Initialize( source );
            source.DirectionChanged += val => _moveInput = val;
        }

        void FixedUpdate() {
            if (!Active) return;
            Vector3 dir = ( _cam.transform.right * _moveInput.x ) + ( _cam.transform.forward * _moveInput.y );
            character.rb.linearVelocity = speed * ( new Vector3( dir.x, 0, dir.z ) ).normalized;
        }

    }

}
