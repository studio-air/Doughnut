using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour
{
    [SerializeField]Button continueButton;
    [SerializeField]GameObject[] panels;

    [SerializeField]string activationText = "Ready To Play!";

    public void ShowRoleInfo(PlayerRole r)
    {
        int ind = (int)r;

        panels[ind].SetActive(true);

        Debug.Log("Assigned Role: " + r);
    }

    public void Activate()
    {
        continueButton.interactable = true;
        continueButton.GetComponentInChildren<Text>().text = activationText;
    }
}
