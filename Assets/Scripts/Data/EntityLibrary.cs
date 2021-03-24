using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityToolset
{
    public string name;
    public Entity[] entities;
}

public class EntityLibrary : MonoBehaviour
{
    public EntityToolset[] toolsets;

    public Entity GetEntity(string id)
    {
        foreach (EntityToolset toolset in toolsets)
        {
            foreach (Entity entity in toolset.entities)
            {
                if(entity.id == id)
                {
                    return entity;
                }
            }
        }
        return null;
    }

}
