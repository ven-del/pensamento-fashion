using UnityEngine;
using UnityEngine.InputSystem;

namespace FashionThoughts {

    public class PlayerAttack : MonoBehaviour {
        public float hitDisplayDuration = 0.3f;
        public float attackRange = 1.5f;
        public int damage = 10;

        public void OnAttack( InputValue value ) {
            if (value.isPressed)
                Attack();
        }

        void Attack() {
            Debug.Log( "Atacou!" );

            Vector3 hitPos = transform.position + transform.forward * attackRange;

            GameObject indicator = GameObject.CreatePrimitive( PrimitiveType.Sphere );
            indicator.transform.position = hitPos;
            indicator.transform.localScale = new Vector3( 1.6f, 0.05f, 0.6f );
            Destroy( indicator.GetComponent<Collider>() );
            Destroy( indicator, hitDisplayDuration );

            Collider[] hits = Physics.OverlapSphere( hitPos, 0.8f );

            // foreach (var hit in hits)
            // {
            //     if (hit.CompareTag("Enemy"))
            //         Debug.Log($"Acertou {hit.name} por {damage}");
            // } isso aqui vem depois, quando implementar inimigo pra testar
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere( transform.position + transform.forward * attackRange, 0.8f );
        }
    }
}
