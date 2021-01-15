using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Utilities;

namespace Game.UI
{
    public class EntityCallback : MonoBehaviour
    {
        public event EventHandler<InfoEventArgs<Entity>> clickEvent;

        public void OnClick(Entity index)
        {
            if(clickEvent != null)
                clickEvent(this, new InfoEventArgs<Entity>(index));
        }
    }
}