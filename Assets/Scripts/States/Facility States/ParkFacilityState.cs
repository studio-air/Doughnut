using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Utilities;

namespace Game.States.Tools
{
    // Creates roads
    public class ParkFacilityState : ToolState
    {

        public override void Enter()
        {
            Debug.Log("Now placing park!");
            //Show road options
            base.Enter();
        }

        public override void Update() 
        {
            base.Update();


            Vector3 mp = GetMouseWorldPos();

            network.ParkShow(mp);

        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        protected override void OnLeftClick() 
        {
            //Raycast for block
            //If hit block get the block 
            //Add icon to the blockRaycastHit hit;
            network.AddPark();
        }

        protected override void OnRightClick()
        {
            network.ParkHide();
            owner.Switch(new DefaultToolState());
        }

        public override void Exit()
        {
            base.Exit();

            // Clean up
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
