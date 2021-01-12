using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Utilities;

namespace Game.UI
{
    public class Callback : MonoBehaviour
    {
        public event EventHandler<InfoEventArgs<int>> clickEvent;

        public void OnClick(int index)
        {
            if(clickEvent != null)
                clickEvent(this, new InfoEventArgs<int>(index));
        }
    }
}