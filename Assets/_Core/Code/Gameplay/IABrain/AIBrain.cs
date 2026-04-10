using System;
using System.Collections.Generic;
using UnityEngine;

namespace FashionThoughts {

    public class AIBrain : MonoBehaviour {
        
        public Transform Target { get; internal set; }
        [SerializeField] AIState[] states;

        // i believe that decisions and actions may need a initialization, but that is up to you

        AIState _currentState;
        Dictionary<string, AIState> _stateMap;

        private void Awake() { 
            _currentState = states.Length > 0 ? states[ 0 ] : null;
            if (_currentState == null) return;
            _stateMap = new();
            foreach (var state in states)
                _stateMap.Add( state.Name, state );
        }

        private void Update() {
            if (_currentState == null) return;
            var nextState = _currentState.Update();
            if (string.IsNullOrEmpty( nextState )) return;
            _stateMap.TryGetValue( nextState, out _currentState );
        }


        [Serializable]
        internal class AIState {

            public string Name;
            public AIActionBase[] Actions;
            public AIDecisionData[] Decisions;

            internal string Update() {
                foreach (var action in Actions) action.Act();
                foreach (var decision in Decisions) {
                    var result = decision.Validate();
                    if (!string.IsNullOrEmpty( result ))
                        return result;
                }
                return null;
            }

        }

        [Serializable]
        internal class AIDecisionData {

            public AIDecisionBase Decision;
            public string OnTrueState, OnFalseState;

            internal string Validate() => Decision.Validate() ? OnTrueState : OnFalseState;

        }

    }

}
