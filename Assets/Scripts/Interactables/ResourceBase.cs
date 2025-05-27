using System;
using UnityEngine;

public class ResourceBase: Interactable
{
    public ResourceType resourceType;
    public int resourceAmount;
    public int collectionValue;

    // Collect Resource
    public override void Interact()
    {
        resourceAmount -= collectionValue;

        Debug.Log(resourceAmount);
        if (resourceAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum ResourceType
{
    None,
    Food,
    Wood,
    Stone,
}
