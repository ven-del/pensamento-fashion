using UnityEngine;

namespace FashionThoughts {

    public class Character : MonoBehaviour {

        [SerializeField] internal Transform model;
        [SerializeField] internal Rigidbody rb;

        [SerializeField] CharacterBehaviourBase[] behaviours;

        private void Awake() {
            foreach (var behaiour in behaviours)
                behaiour.Initialize( this );
        }

    }

}
