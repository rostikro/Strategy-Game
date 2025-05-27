using UnityEngine;

public class Monument: Building
{
    public override void Interact()
    {
        meshFilter.mesh = builtMesh;
        meshRenderer.material = builtMaterial;

        GameMode.Instance.WinGame();
    }
}
