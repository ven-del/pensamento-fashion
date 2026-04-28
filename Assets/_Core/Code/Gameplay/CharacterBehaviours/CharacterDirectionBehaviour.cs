using DG.Tweening;
using UnityEngine;

namespace FashionThoughts {

    public class CharacterDirectionBehaviour : CharacterBehaviourBase {

        [SerializeField] float turnSpeed = .25f;

        Vector2 _direction = Vector2.right;
        Tweener _turnTween;
        float _scale;

        internal override void Initialize( Character source ) {
            base.Initialize( source );
            _scale = character.model.transform.localScale.x;
            source.DirectionChanged += ChangeDirection;
        }

        void ChangeDirection( Vector2 dir ) {
            var prevDir = _direction;
            _direction = dir.magnitude > 0 ? dir : _direction;
            if (prevDir == _direction) return;
            var requestFlip = _direction.x > 0 && prevDir.x < 0 || _direction.x < 0 && prevDir.x > 0;
            if (!requestFlip) return;
            var model = character.model;
            _turnTween?.Kill();
            _turnTween = model.DOScale( new Vector3( _scale * (_direction.x < 0 ? -1 : 1), model.localScale.y, model.localScale.z ), turnSpeed );
        }

    }

}
