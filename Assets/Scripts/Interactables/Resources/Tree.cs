using UnityEngine;

public class Tree : ResourceBase
{
    protected override void Awake()
    {
        base.Awake();
        resourceType = ResourceType.Wood;
        resourceAmount = 50;
        collectionValue = 10;
        interactionTime = 3;
    }
}