using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace FashionThoughts {

    public class Damageble : MonoBehaviour {

        [SerializeField] MeshRenderer meshRender;

        List<Tweener> _dmgAnim;
        Color[] _originalColors;

        private void Awake() { 
            _dmgAnim = new(); 
            _originalColors = new Color[meshRender.materials.Length];
            for (int i = 0; i < meshRender.materials.Length; i++)
                _originalColors[ i ] = meshRender.materials[ i ].color;
        }

        public void TakeDamage() {
            transform.DOShakePosition( .5f, .5f, 20 );
            _dmgAnim.Clear();
            for (int i = 0; i < meshRender.materials.Length; i++) {
                int index = i;
                var mat = meshRender.materials[ index ];
                _dmgAnim.Add( mat.DOColor( Color.red, .5f ).OnComplete( () => mat.DOColor( _originalColors[ index ], .5f ) ) );
            }
        }

    }

}
