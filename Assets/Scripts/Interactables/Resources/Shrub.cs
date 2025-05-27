using UnityEngine;

public class Shrub : ResourceBase
{
    protected override void Awake()
    {
        base.Awake();
        resourceType = ResourceType.Food;
        resourceAmount = 30;
        collectionValue = 10;
        interactionTime = 3;
    }
}
