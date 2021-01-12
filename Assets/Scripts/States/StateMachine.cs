using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class StateMachine
    {
        public GameController owner;
        
        public StateMachine(GameController owner)
        {
            this.owner = owner;
        }

        public virtual State CurrentState
        {
            get { return _currentState; }
        }

        protected State _currentState;
        protected bool _inTransition;

        public virtual State Switch(State state)
        {
            State newState = state;

            newState.Init(this);

            //Check type if same then ignore

            //Check if already in transition and if the new state is current state

            if(_currentState != null)
                if(_currentState.GetType() == newState.GetType())
                    return null;

            if(_inTransition)
                return null;
            
            //Not current state
            _inTransition = true;

            if(_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter();

            _inTransition = false;

            return newState;

        }

        public virtual void Update() {
            _currentState.Update();
        }
        
    }
}
