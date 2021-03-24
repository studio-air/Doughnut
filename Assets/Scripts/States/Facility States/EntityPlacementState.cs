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
        private Plane plane;

        public EntityPlacementState(Entity e)
        {
            plane = new Plane(Vector3.up, Vector3.zero);
            entity = e;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Now placing: " + entity.entityName);
            
            Vector3 mp = GetMouseWorldPos();
            marker = Instantiate(entity.prefabs[0], mp);
            //marker.GetComponent<Marker>().color = entity.color;
        }

        public override void Update() 
        {
            base.Update();
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float enter;

            if(plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter) + Vector3.up * 0.5f;
                marker.transform.position = hitPoint;
            }

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
            if(!owner.owner.networkManager.connected)
                return;
            
            //Raycast for block
            //If hit block get the block 
            //Add icon to the blockRaycastHit hit;
            //network.AddApartment();

            //Add new icon to the centre of mesh
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Block block = hit.transform.GetComponent<Block>();

                if(block.occupied)
                    return;

                neighbourhood.Build(block.id, entity);
    
                //Temp placement
                DestroyObj(marker);
                owner.owner.networkManager.SendBuild(block.id, entity.id);
                owner.owner.networkManager.EndTurn();
                owner.Switch(new LockGameState());
            }

        }

        protected override void OnRightClick()
        {
            //network.ApartmentHide();
            DestroyObj(marker);
            owner.Switch(new PlayerTurnState());
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
