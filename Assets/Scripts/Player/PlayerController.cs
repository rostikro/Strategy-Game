using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [CanBeNull] private VillagerController _villagerSelected;
    
    // Update is called once per frame
    void Update()
    {
        if (!Mouse.current.leftButton.isPressed)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HandleRaycastHit(hit);
        }
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;
        
        Debug.Log(hitObject.name);

        if (hitObject.CompareTag("Villager"))
        {
            var villager = hitObject.GetComponent<VillagerController>();
            if (!villager || villager == _villagerSelected)
                return;
            
            _villagerSelected?.Deselect();

            _villagerSelected = villager;
            _villagerSelected.Select();
            
            return;
        }

        if (!_villagerSelected)
            return;

        if (hitObject.CompareTag("Resource"))
        {
            var resource = hitObject.GetComponent<ResourceBase>();
            _villagerSelected.SetCollectWork(resource);
            
            _villagerSelected.Deselect();
            _villagerSelected = null;

            return;
        }

        if (hitObject.CompareTag("Building"))
        {
            var building = hitObject.GetComponent<Building>();
            _villagerSelected.SetBuildingWork(building);
            
            _villagerSelected.Deselect();
            _villagerSelected = null;
        }
    }
}
