using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    
    public TextMeshProUGUI timerText;

    public void UpdateStats(int food, int wood, int stone)
    {
        foodText.text = $"Food: {food}";
        woodText.text = $"Wood: {wood}";
        stoneText.text = $"Stone: {stone}";
    }

    public void SetTimerText(float elapsedTime)
    {
        timerText.text = $"Time: {FormatTime(elapsedTime)}";
    }
    
    private string FormatTime(float time)
    {
        int hours = Mathf.FloorToInt(time / 3600f);
        int minutes = Mathf.FloorToInt((time % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return $"{hours:0}:{minutes:00}:{seconds:00}";
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
