using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Game.Utilities;   

namespace Game.States.Tools
{
    //Doesn't do anything
    public class ToolState : State
    {
        protected Network network { get { return owner.owner.network; } set { owner.owner.network = value; } }

        protected GameObject Instantiate(GameObject obj, Vector3 pos)
        {
            return owner.owner.Instantiate(obj, pos);
        }
        
        protected void DestroyObj(GameObject obj)
        {
            owner.owner.DestroyObj(obj);
        }

        public override void Update() 
        {
            base.Update();

            if (Input.GetMouseButtonUp(0))
            {
                if(EventSystem.current.IsPointerOverGameObject())
                    return;

                OnLeftClick();
            }
            if (Input.GetMouseButtonUp(1))
            {
                if(EventSystem.current.IsPointerOverGameObject())
                    return;
                
                OnRightClick();
                
            }

        }
        protected virtual void OnLeftClick()
        {
            
        }

        protected virtual void OnRightClick()
        {
            
        }

    }
}
