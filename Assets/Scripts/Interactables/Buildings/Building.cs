using UnityEngine;

public class Building : Interactable
{
    [SerializeField] protected Mesh builtMesh;
    [SerializeField] protected Material builtMaterial;
    
    [SerializeField] private Mesh underConstructionMesh;
    [SerializeField] private Material underConstructionMaterial;

    public int woodAmountForBuild;
    public int stoneAmountForBuild;

    public bool isInteractable = true;
    
    protected MeshRenderer meshRenderer;
    protected  MeshFilter meshFilter;

    protected override void Awake()
    {
        base.Awake();
        
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    public void SetValidMaterial()
    {
        meshRenderer.material.color = Color.white;
    }

    public void SetInvalidMaterial()
    {
        meshRenderer.material.color = Color.red;
    }

    public void ApplyPlacing()
    {
        meshFilter.mesh = underConstructionMesh;
        meshRenderer.material = underConstructionMaterial;
        
        GameMode.Instance.RemoveWood(woodAmountForBuild);
        GameMode.Instance.RemoveStone(stoneAmountForBuild);
    }
}
