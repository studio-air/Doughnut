using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Utilities;

namespace Game.UI
{
    public class Callback : MonoBehaviour
    {
        public event EventHandler<EventArgs> clickEvent;

        public void OnClick()
        {
            if(clickEvent != null)
                clickEvent(this, new EventArgs());
        }
    }
}