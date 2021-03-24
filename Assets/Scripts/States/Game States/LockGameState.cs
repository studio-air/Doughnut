using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class LockGameState : State
    {
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Locked Game!");
            owner.owner.gameUI.Lock();
            owner.owner.gameUI.EndTurnActive(false);
        }
    }
}