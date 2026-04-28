using FashionThoughts.StateMachineLogic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FashionThoughts {

    public class Character : MonoBehaviour {

        const string k_idle = "Idle";

        [SerializeField] internal Transform model;
        [SerializeField] internal Rigidbody rb;
        [SerializeField] internal Animator animator;

        [SerializeField] CharacterBehaviourBase[] behaviours;

        Vector2 _direction;
        internal Vector2 Direction { 
            get => _direction; 
            private set => DirectionChanged?.Invoke( _direction = value );
        }

        internal event Action<Vector2> DirectionChanged;

        StateMachine _stateMachine;
        State _idleState;

        Dictionary<string, State> _registeredStates;

        private void Awake() {
            _idleState = new ( () => animator.Play( k_idle ), null, k_idle );
            _stateMachine = new( _idleState );
            _registeredStates = new();
            foreach (var behaiour in behaviours)
                behaiour.Initialize( this );
        }

        internal void AddLinkToIdleState( Link link ) => _idleState.AddLink( link );

        internal void AddLinkToRegisteredState( string stateName, Link link ) {
            if (!_registeredStates.ContainsKey( stateName )) return;
            _registeredStates[ stateName ].AddLink( link );
        }

        internal void RegisterState( State state ) {
            if (_registeredStates.ContainsKey( state.Name )) return;
            _registeredStates.Add( state.Name, state );
        }

        public void SetDirection( Vector2 dir ) => Direction = dir;


    }

}
