using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu()]
public class Animal : ScriptableObject
{
    public Sprite image;
    public Sprite top;
    public Sprite center;
    public Sprite bottom;
    public string name_;
    public Text animalText;
    public AudioClip description;
    public AudioClip audioName;

    private int id;
    private Color color;

    public void SetAnimalId(int _id)
    {
        this.id = _id;
    }
    public void SetAnimalColor(Color c)
    {
        this.color = c;
    }

    public Color GetAnimalColor()
    {
        return this.color;
    }

    public int GetAnimalId()
    {
        return this.id;
    }

    public string GetAnimalName()
    {
        return this.name_;
    }
}
