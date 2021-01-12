using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

using Game.States;
using Game.States.Tools;
using Game.Utilities;
using Game.UI;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    [Header("UI Refrences")]
    // Tools UI
    public Callback facilityOptions;

    [Header("Objects References")]
    public Network network;

    // TODO: Pass on self to the state machine in order to allow for states to modify world
    private StateMachine facilitiesStates;
    
    private Vector3 mouseStart;

    private void Start() {

        facilitiesStates = new StateMachine(this);
        // Default tool which doesn't do anything when left clicking
        facilitiesStates.Switch(new DefaultToolState());

        facilityOptions.clickEvent += OnFacilityClick;
    }

    private void OnFacilityClick(object sender, InfoEventArgs<int> e)
    {
        switch (e.info)
        {
            case 0:
                //Mix Use Commercial
                facilitiesStates.Switch(new ApartmentFacilityState());
                break;

            case 1:
                //Park
                facilitiesStates.Switch(new ParkFacilityState());
                break;

            default:
                //facilitiesStates.Switch(new DefaultToolState());
                break;
        }
    }

    private void Update() {
        facilitiesStates.Update();
    }

    private void OnDestroy() {
        facilityOptions.clickEvent -= OnFacilityClick;
    }
}
