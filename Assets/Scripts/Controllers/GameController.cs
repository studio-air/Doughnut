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
    public EntityCallback facilityOptions;

    [Header("Objects References")]
    public Network network;

    private StateMachine facilitiesStates;
    
    private Vector3 mouseStart;

    private void Start() {

        facilitiesStates = new StateMachine(this);
        // Default tool which doesn't do anything when left clicking
        facilitiesStates.Switch(new DefaultToolState());

        facilityOptions.clickEvent += OnFacilityClick;
    }

    private void OnFacilityClick(object sender, InfoEventArgs<Entity> e)
    {
        facilitiesStates.Switch(new EntityPlacementState(e.info));
    }

    private void Update() {
        facilitiesStates.Update();
    }

    private void OnDestroy() {
        facilityOptions.clickEvent -= OnFacilityClick;
    }

    public GameObject Instantiate(GameObject prefab, Vector3 pos)
    {
        return Instantiate(prefab, pos, Quaternion.Euler(0, 0, 0));
    }

    public void DestroyObj(GameObject obj)
    {
        Destroy(obj);
    }
}
