using UnityEngine;
using UnityEngine.UI;

public class BuildMenuController : MonoBehaviour
{
    public Button buildHouseButton;
    public Button buildMonumentButton;
    
    private bool isMenuActive = false;

    private void Start()
    {
        buildHouseButton.interactable = false;
        
        buildMonumentButton.interactable = false;
    }
    
    public void OnBuildButtonPressed()
    {
        SetMenuActiveState(!isMenuActive);
    }

    public void SetCardsActive(bool houseCard, bool monumentCard)
    {
        buildHouseButton.interactable = houseCard;
        buildMonumentButton.interactable = monumentCard;
    }

    private void SetMenuActiveState(bool isActive)
    {
        isMenuActive = isActive;
        buildHouseButton.gameObject.SetActive(isActive);
        buildMonumentButton.gameObject.SetActive(isActive);
    }

    public void OnBuildHouseButtonPressed()
    {
        SetMenuActiveState(false);
        GameMode.Instance.StartBuildingHouse();
    }

    public void OnBuildMonumentButtonPressed()
    {
        SetMenuActiveState(false);
        GameMode.Instance.StartBuildingMonument();
    }
}
