using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    public Image background;
    private string id;
    public Image image;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void SetImageSprite(Sprite s)
    {
        image.sprite = s;
    }
    public void SetImageId(string _id)
    {
        this.id = _id;
    }
    public  void SetBgColor(Color c)
    {
        this.background.color = c;
    }

    public void SetAnimal(Sprite s, string _id, Color c)
    {
        this.SetImageSprite(s);
        this.SetImageId(_id);
        this.SetBgColor(c);
    }

    public string GetImageId()
    {
        return this.id;
    }

}
