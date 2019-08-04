using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;

    private int totalPages;

    //default positions
    private Vector3 leftLocation;
    private Vector3 centerLocation;
    private Vector3 rightLocation;
    
    private int leftIndex = 0;
    private int centerIndex = 1;
    private int rightIndex = 2;

    public enum Position { TOP, CENTER, BOTTOM }
    public Position position = Position.TOP;

    public Page[] pages;
    public List<Animal> animals;

    // Start is called before the first frame update
    void Start()
    {
        totalPages = animals.Count;

        SetAnimals();

        CheckIfWin();

        leftLocation = pages[0].transform.position;
        centerLocation = pages[1].transform.position;
        rightLocation = pages[2].transform.position;

        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        if (GameManager.instance.CanMove)
        {
            float difference = data.pressPosition.x - data.position.x;
            transform.position = panelLocation - new Vector3(difference, 0, 0);
        }
    }
    public void OnEndDrag(PointerEventData data)
    {
        if (GameManager.instance.CanMove)
        {
            float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
            if (Mathf.Abs(percentage) >= percentThreshold)
            {
                Vector3 newLocation = panelLocation;
                if (percentage > 0)
                {
                    //Page++;
                    newLocation += new Vector3(-Screen.width, 0, 0);

                }
                else if (percentage < 0)
                {
                    //Page--;
                    newLocation += new Vector3(Screen.width, 0, 0);

                }
                StartCoroutine(SmoothMove(transform.position, newLocation, easing, percentage));
                panelLocation = newLocation;

            }
            else
            {
                StartCoroutine(SmoothMove(transform.position, panelLocation, easing, percentage));
            }
        }
    }

    public void SetAnimals()
    {
        if(position == Position.TOP) { 
            pages[0].SetAnimal(animals[leftIndex].top, animals[leftIndex].name_, animals[leftIndex].GetAnimalColor());
            pages[1].SetAnimal(animals[centerIndex].top, animals[centerIndex].name_, animals[centerIndex].GetAnimalColor());
            pages[2].SetAnimal(animals[rightIndex].top, animals[rightIndex].name_, animals[rightIndex].GetAnimalColor());
        }
        else if (position == Position.CENTER)
        {
            pages[0].SetAnimal(animals[leftIndex].center, animals[leftIndex].name_, animals[leftIndex].GetAnimalColor());
            pages[1].SetAnimal(animals[centerIndex].center, animals[centerIndex].name_, animals[centerIndex].GetAnimalColor());
            pages[2].SetAnimal(animals[rightIndex].center, animals[rightIndex].name_, animals[rightIndex].GetAnimalColor());
        }
        else if (position == Position.BOTTOM)
        {
            pages[0].SetAnimal(animals[leftIndex].bottom, animals[leftIndex].name_, animals[leftIndex].GetAnimalColor());
            pages[1].SetAnimal(animals[centerIndex].bottom, animals[centerIndex].name_, animals[centerIndex].GetAnimalColor());
            pages[2].SetAnimal(animals[rightIndex].bottom, animals[rightIndex].name_, animals[rightIndex].GetAnimalColor());
        }
    }

    private void UpdateAnimal(float direction)
    {
        if (direction > 0)
        {
            leftIndex = (leftIndex + 1) % totalPages;
            centerIndex = (centerIndex + 1) % totalPages;
            rightIndex = (rightIndex + 1) % totalPages;
        }
        else
        {
            leftIndex = (leftIndex - 1) % totalPages;
            centerIndex = (centerIndex - 1) % totalPages;
            rightIndex = (rightIndex - 1) % totalPages;

            if (leftIndex < 0)
                leftIndex += totalPages;
            if (centerIndex < 0)
                centerIndex += totalPages;
            if (rightIndex < 0)
                rightIndex += totalPages;
        }

        SetAnimals();

    }

    public string GetAnimalId()
    {
        return pages[1].GetImageId();
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds, float percentage)
    {
        GameManager.instance.SetMove(false);

        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        AudioManager.instance.PlaySound(GameManager.instance.snap, Vector3.zero);

        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            if (percentage > 0)
            {
                Page temp = pages[0];
                pages[0] = pages[1];
                pages[1] = pages[2];
                pages[2] = temp;
                pages[2].transform.position = rightLocation;
            }
            else if (percentage < 0)
            {
                Page temp = pages[2];
                pages[2] = pages[1];
                pages[1] = pages[0];
                pages[0] = temp;
                pages[0].transform.position = leftLocation;
            }

            UpdateAnimal(percentage);
        }

        CheckIfWin();

    }

    void CheckIfWin()
    {
        if (GameManager.instance.CheckWinCondition())
        {
            GameManager.instance.SetMove(false);
            GameManager.instance.WinGame(animals[centerIndex]);
        }
        else
        {
            GameManager.instance.SetMove(true);
        }
    }
}
