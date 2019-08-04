using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
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
