using UnityEngine;

public class Stone : ResourceBase
{
    protected override void Awake()
    {
        base.Awake();
        resourceType = ResourceType.Stone;
        resourceAmount = 50;
        collectionValue = 10;
        interactionTime = 10;
    }
}