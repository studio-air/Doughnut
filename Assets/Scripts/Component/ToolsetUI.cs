using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.UI;

public class ToolsetUI : MonoBehaviour
{
    [SerializeField] NetworkManager network;
    [SerializeField] EntityLibrary library;
    [SerializeField] GameObject option;

    EntityCallback cb;

    private void Start() 
    {
        cb = GetComponent<EntityCallback>();
    }

    public void CreateToolset()
    {
        Entity[] entities = library.toolsets[(int)network.role].entities;
        foreach (Entity entity in entities)
        {
            GameObject o = Instantiate(option, transform);
            o.GetComponent<Button>().onClick.AddListener(delegate{cb.OnClick(entity);});
        }
    }
}
