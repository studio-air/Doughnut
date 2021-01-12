using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class State
    {
        protected StateMachine owner;

        public virtual void Init(StateMachine owner)
        {
            this.owner = owner;
        }

        public virtual void Enter()
        {
            AddListeners();
        }

        public virtual void Update() {
            
        }

        public virtual void Exit()
        {
            RemoveListeners();
        }

        protected virtual void AddListeners()
        {
            
        }

        protected virtual void RemoveListeners()
        {
            
        }
    }
}
