using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioClip mainTheme;
    public AudioClip menuTheme;

    public AudioClip[] themes;

    string sceneName;

    void Start()
    {
        OnLevelWasLoaded(0);
    }


    void OnLevelWasLoaded(int sceneIndex)
    {
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != sceneName)
        {
            sceneName = newSceneName;
            Invoke("PlayMusic", .2f);
        }
    }

    void PlayMusic()
    {
        AudioClip clipToPlay = null;

        if (sceneName == "Menu")
        {
            clipToPlay = menuTheme;
        }
        else if (sceneName == "Game")
        {
            System.Random r = new System.Random();
            clipToPlay = themes[r.Next(0, themes.Length)];
        }

        if (clipToPlay != null)
        {
            AudioManager.instance.PlayMusic(clipToPlay, 1);
            Invoke("PlayMusic", clipToPlay.length);
        }

    }

}