using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Utilities;

namespace Game.States.Tools
{
    // Creates roads
    public class EntityPlacementState : ToolState
    {
        private Entity entity;
        private GameObject marker;

        public EntityPlacementState(Entity e)
        {
            entity = e;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Now placing: " + entity.entityName);
            
            Vector3 mp = GetMouseWorldPos();
            marker = Instantiate(entity.prefab, mp);
            marker.GetComponent<Marker>().color = entity.color;
        }

        public override void Update() 
        {
            base.Update();

            Vector3 mp = GetMouseWorldPos();
            marker.transform.position = mp;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Block block = hit.transform.GetComponent<Block>();
                block.Highlight();
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.y = 0f;
            return worldPos;
        }

        protected override void OnLeftClick() 
        {
            //Raycast for block
            //If hit block get the block 
            //Add icon to the blockRaycastHit hit;
            //network.AddApartment();

            //Add new icon to the centre of mesh
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Block block = hit.transform.GetComponent<Block>();
                block.AddEntity(entity);
            }

        }

        protected override void OnRightClick()
        {
            //network.ApartmentHide();
            DestroyObj(marker);
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
