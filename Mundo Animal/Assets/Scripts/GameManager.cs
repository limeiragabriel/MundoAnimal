using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public PageSwiper top;
    public PageSwiper center;
    public PageSwiper bottom;

    public GameObject pages;

    public int animalsPerGame = 4;
    public bool CanMove { get; private set; }

    public List<Animal> allAnimals;
    private List<Animal> gameAnimals;

    public List<Color> bgColors;
    private List<Color> shuffledColors;

    public static GameManager instance;

    [Header("Banner")]
    public Animal winner;
    public GameObject winnerBanner;
    public Image winnerBannerImage;

    [Header("Info")]
    public GameObject animalInfo;
    public Image InfoImage;
    public Text animalText;
    public Text animalName;

    [Header("SFX")]
    public AudioClip snap;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            System.Random r = new System.Random();
            gameAnimals = Util.ShuffleList(allAnimals, r.Next()).GetRange(0, animalsPerGame);

            SetAnimalColors();

            CanMove = true;
        }
    }

    private void Start()
    {

        top.animals = Util.ShuffleList(gameAnimals, 7);
        center.animals = Util.ShuffleList(gameAnimals, 117);
        bottom.animals = Util.ShuffleList(gameAnimals, 15);

    }

    public void SetMove(bool state)
    {
        this.CanMove = state;
    }

    public bool CheckWinCondition()
    {

        bool win = top.GetAnimalId() == center.GetAnimalId() && top.GetAnimalId() == bottom.GetAnimalId();

        //Util.ClearConsole();
        //print("top: " + top.GetAnimalId() +
        //      " center: " + center.GetAnimalId() +
        //      " bottom: " + bottom.GetAnimalId() +
        //      " condition: " + win);

        return win;
    }

    public void SetAnimalColors()
    {
        System.Random r = new System.Random();
        shuffledColors = Util.ShuffleList(bgColors, r.Next());

        for (int i = 0; i < gameAnimals.Count; i++)
        {
            gameAnimals[i].SetAnimalColor(shuffledColors[i]);
        }
    }

    public void SetWinBanner(Color c, Sprite s)
    {
        winnerBanner.GetComponent<Image>().color = c;
        winnerBannerImage.sprite = s;
        winnerBannerImage.preserveAspect = true;
    }

    public void WinGame(Animal a)
    {
        winner = a;

        SetWinBanner(a.GetAnimalColor(), a.image);

        pages.SetActive(false);
        winnerBanner.SetActive(true);

        AudioManager.instance.SetVolume(.1f, AudioManager.AudioChannel.Music);
        AudioManager.instance.SetVolume(.7f, AudioManager.AudioChannel.Sfx);
        AudioManager.instance.PlaySound(winner.audioName, Vector3.zero);
    }

    public void GoToDetail()
    {
        winnerBanner.SetActive(false);
        animalInfo.SetActive(true);
        InfoImage.sprite = winner.image;
        InfoImage.preserveAspect = true;
        animalText.text = winner.animalText.text;
        animalName.text = winner.name_;
        animalInfo.GetComponent<Image>().color = winner.GetAnimalColor();

        AudioManager.instance.PlaySound(winner.description, Vector3.zero);

    }

    public void ReloadGame()
    {
        AudioManager.instance.SetVolume(.5f, AudioManager.AudioChannel.Music);
        AudioManager.instance.SetVolume(.5f, AudioManager.AudioChannel.Sfx);
        SceneManager.LoadScene("Game");
    }

    public void ToggleSound()
    {
        AudioManager.instance.ToggleSound();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
