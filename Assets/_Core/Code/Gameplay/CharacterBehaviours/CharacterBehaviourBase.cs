using UnityEngine;

namespace FashionThoughts {

    public abstract class CharacterBehaviourBase : MonoBehaviour {

        public bool Active { get; internal set; }
        protected Character character;

        internal virtual void Initialize( Character source ) => character = source;

    }

}
