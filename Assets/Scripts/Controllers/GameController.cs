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
    public Callback endTurnCallback;

    [Header("Objects References")]
    public Neighbourhood neighbourhood;
    public NetworkManager networkManager;
    public GameUI gameUI;

    public StateMachine machine;
    
    private Vector3 mouseStart;

    private void Start() {

        machine = new StateMachine(this);
        // Default tool which doesn't do anything when left clicking
        machine.Switch(new LockGameState());

        facilityOptions.clickEvent += OnFacilityClick;
    }

    private void OnFacilityClick(object sender, InfoEventArgs<Entity> e)
    {
        machine.Switch(new EntityPlacementState(e.info));
    }

    private void Update() {
        machine.Update();
    }

    private void OnDestroy() {
        facilityOptions.clickEvent -= OnFacilityClick;
    }

    public void LockGame()
    {
        gameUI.Lock();
    }
    
    public void UnlockGame()
    {
        gameUI.Unlock();
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
