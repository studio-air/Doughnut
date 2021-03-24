using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.States;
using Socket.Quobject.SocketIoClientDotNet.Client;

public class BuildData
{
    public int blockID;
    public string entityID;
}

public class TurnData
{
    public int role;
}

public class ChatData
{
    public int role;
    public string message;
}

public enum PlayerRole
{
    NONE = -1,
    DEVELOPER = 0,
    ENVIRONMENTALIST = 1,
    ENTREPRENEUR = 2
}

public class NetworkManager : MonoBehaviour
{
    [SerializeField] GameController controller;
    [SerializeField] Neighbourhood neighbourhood;
    [SerializeField] RoleUI roleUI;
    [SerializeField] EntityLibrary library;

    public bool connected = false;
    private QSocket socket;

    private bool ready = false;
    public PlayerRole role = PlayerRole.NONE;
    private BuildData bMsg = null;
    private TurnData tMsg = null;
    
    //[DllImport("__Internal")]
    //private static extern void Hello();

    private void Start() 
    {
        //Application.ExternalCall ("connect", ""); 
        //Hello();
    }

    public void Connect()
    {
        Debug.Log("Starting Up Client...");
        socket = IO.Socket("http://localhost:3000");

        socket.On(QSocket.EVENT_CONNECT, () => {
            connected = true;
            Debug.Log("Connected to the Server!");
        });

        socket.On(QSocket.EVENT_DISCONNECT, () => {
            connected = false;
            Debug.Log("Disconnected from the Server!");
        });

        socket.On("role", roleData => {
            if (Int32.TryParse(roleData.ToString(), out int r))
            {
                role = (PlayerRole)r;
            }
        });

        socket.On("ready", (rData) => {
            ready = true;
            Debug.Log("READY!");
        });

        socket.On("turn", (tData) => {
            lock(tMsg) { tMsg = JsonUtility.FromJson<TurnData>(tData.ToString()); }
        });

        socket.On("build", (bData) => {
            lock(bMsg) { bMsg = JsonUtility.FromJson<BuildData>(bData.ToString()); }
        });

        socket.On("chat", (cData) => {
            //TODO: How to client's states are modyfied
            Debug.Log("data: " + cData);
        });

        socket.On("reconnect-q", () => {
            if(role != PlayerRole.NONE)
            {
                socket.Emit("reconnect-a");
            }
        });
    }

    private void Update() 
    {
        //To make sure messages don't derail main thread, they are proccessed in Update function

        if(role != PlayerRole.NONE)
        {
            roleUI.gameObject.SetActive(true);
            roleUI.ShowRoleInfo(role);
        }

        if(ready)
        {
            roleUI.Activate();
            ready = false;
        }

        if(bMsg != null)
        {
            Build(bMsg.blockID, bMsg.entityID);
            lock (bMsg) { bMsg = null; }
        }

        if(tMsg != null)
        {
            Turn(tMsg.role);
            lock (tMsg) { tMsg = null; }
        }

    }

    public void Build(int blockID, string entityID)
    {
        neighbourhood.Build(blockID, library.GetEntity(entityID));
    }

    public void SendBuild(int id, string name)
    {
        BuildData data = new BuildData{blockID = id, entityID = name};
        string msg = JsonUtility.ToJson(data);
        socket.Emit("build", msg);
    }

    private void Turn(int index)
    {
        if(index == (int)role)
        {
            //Our turn
            controller.machine.Switch(new PlayerTurnState());
        }
    }

    public void EndTurn()
    {
        TurnData data = new TurnData { role = -1 };
        string msg = JsonUtility.ToJson(data);
        socket.Emit("turn", msg);
    }

    private void OnDestroy()
    {
        socket.Disconnect();
    }

}