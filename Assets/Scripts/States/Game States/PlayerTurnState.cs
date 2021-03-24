using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.States
{
    public class PlayerTurnState : State
    {
        public override void Enter()
        {
            base.Enter();
            owner.owner.gameUI.Unlock();
            owner.owner.gameUI.EndTurnActive(true);
            Debug.Log("My Turn!");
        }

        protected override void AddListeners()
        {
            base.AddListeners();
            owner.owner.endTurnCallback.clickEvent += OnTurnEnd;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            owner.owner.endTurnCallback.clickEvent -= OnTurnEnd;
        }
        
        protected void OnTurnEnd(object sender, EventArgs e)
        {
            //Network manager send info
            owner.Switch(new LockGameState());
            owner.owner.networkManager.EndTurn();
        }
    }
}