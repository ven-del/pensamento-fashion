using System;
using System.Collections.Generic;
using UnityEngine;

namespace FashionThoughts.StateMachineLogic {

    public class StateMachine {

        State _current;

        public StateMachine( State startState ) => _current = startState;

        public void Update() {
            var nextState = _current.Update();
            if (nextState == null) return;
            ChangeState( nextState );
        }

        void ChangeState( State newState ) {
            _current.OnExit();
            _current = newState;
            _current.OnEnter();
        }

    }

    public class State {

        public readonly string Name;
        readonly List<Link> _links;
        readonly Action _onEnter, _onExit;


        public State( Action onEnter, Action onExit, string name = null ) {
            Name = name;
            _links = new();
            _onEnter = onEnter;
            _onExit = onExit;
        }

        public State AddLink( params Link[] links ) {
            foreach (var link in links) 
                _links.Add( link );
            return this;
        }

        public virtual void OnEnter() => _onEnter?.Invoke();

        public virtual State Update() {
            foreach (var link in _links) {
                if (link.Validate( out var state ))
                    return state;
            }
            return null;
        }

        public virtual void OnExit() => _onExit?.Invoke();

    }

    public class Link {

        readonly Func<bool> _condition;
        readonly State _targetState;

        private Link() { }
        private Link( State taregtState, Func<bool> condition ) => (_targetState, _condition) = (taregtState, condition);

        public bool Validate( out State state ) {
            state = null;
            var result = _condition();
            if (result) state = _targetState;
            return result;
        }

        internal static Link Create( State targetState, Func<bool> condition ) {
            if (condition == null || targetState == null) {
                Debug.LogError( $"Can't create link, the target state or condition is null -> state is null? {targetState == null}, condition is null? {condition == null}" );
                return null;
            }
            return new( targetState, condition );
        }

    }

}
