using System.Collections.Generic;
using UnityEngine;

public class PlaceBuildingController : MonoBehaviour
{
    private bool isPlacing = false;

    public GameObject housePrefab;
    public GameObject monumentPrefab;
    
    private Building buildingInstance;
    
    public LayerMask groundLayer;
    public float placementRadius = 1f;

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            UpdatePreviewPosition();

            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceBuilding();
            }

            if (Input.GetMouseButtonDown(1)) // Right-click to cancel
            {
                CancelPlacement();
            }
        }
    }
    
    public void StartPlacingHouse()
    {
        StartPlacing(housePrefab);
    }

    public void StartPlacingMonument()
    {
        StartPlacing(monumentPrefab);
    }

    private void StartPlacing(GameObject prefab)
    {
        isPlacing = true;
        
        GameObject instance = Instantiate(prefab);
        buildingInstance = instance.GetComponent<Building>();
    }
    
    void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            if (UnityEngine.AI.NavMesh.SamplePosition(hit.point, out UnityEngine.AI.NavMeshHit navHit, placementRadius, UnityEngine.AI.NavMesh.AllAreas))
            {
                buildingInstance.transform.position = navHit.position;
                buildingInstance.SetValidMaterial();
            }
            else
            {
                buildingInstance.transform.position = hit.point;
                buildingInstance.SetInvalidMaterial();
            }
        }
    }

    void TryPlaceBuilding()
    {
        if (buildingInstance == null) return;

        // Check again for NavMesh validity
        if (UnityEngine.AI.NavMesh.SamplePosition(buildingInstance.gameObject.transform.position, out UnityEngine.AI.NavMeshHit navHit, placementRadius, UnityEngine.AI.NavMesh.AllAreas))
        {
            buildingInstance.ApplyPlacing();
            isPlacing = false;
        }
    }

    void CancelPlacement()
    {
        if (buildingInstance != null)
        {
            Destroy(buildingInstance.gameObject);
        }
        isPlacing = false;
    }
}
