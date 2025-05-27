using UnityEngine;

public class House : Building
{
    public override void Interact()
    {
        meshFilter.mesh = builtMesh;
        meshRenderer.material = builtMaterial;
        
        // Spawn villager
        GameMode.Instance.SpawnVillager(transform.position + Vector3.back * 5);
        
        isInteractable = false;
    }
}
