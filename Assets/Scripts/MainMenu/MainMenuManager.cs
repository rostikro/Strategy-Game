using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject aboutPanel;
    
    public void OnAboutButtonClicked()
    {
        aboutPanel.SetActive(true);
    }

    public void OnAboutPanelOkButtonClicked()
    {
        aboutPanel.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
