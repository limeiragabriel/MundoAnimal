using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Text pointsText;

    private void Start()
    {
        pointsText.text = "pontos: " + PlayerPrefs.GetInt("points", 0).ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void ToggleSound()
    {
        AudioManager.instance.ToggleSound();
    }
}
