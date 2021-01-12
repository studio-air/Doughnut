using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States.Tools
{
    // Doesn't do anything
    // Acts as a template
    public class DefaultToolState : ToolState
    {
        public override void Enter()
        {
            Debug.Log("Switched back to default selection!");
            base.Enter();
        }

        public override void Update() 
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }

        protected override void AddListeners()
        {
            base.AddListeners();
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
        }
    }
}
