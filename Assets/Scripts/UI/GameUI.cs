using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]GameObject toolsUI;
    [SerializeField]GameObject endTurnUI;

    public void Unlock()
    {
        toolsUI.SetActive(true);
    }

    public void Lock()
    {
        toolsUI.SetActive(false);
    }

    public void EndTurnActive(bool v)
    {
        endTurnUI.SetActive(v);
    }
}
