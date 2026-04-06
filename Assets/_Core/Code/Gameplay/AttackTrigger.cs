using UnityEngine;

namespace FashionThoughts {

    public class AttackTrigger : MonoBehaviour {

        private void OnTriggerEnter( Collider other ) {
            if (!other.TryGetComponent<Damageble>( out var damageble )) return;
            damageble.TakeDamage();
        }

    }

}
