using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClickable : MonoBehaviour
{
    public AudioClip clicksound;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Animate()
    {
        AudioManager.instance.PlaySound(clicksound, Vector3.zero);
        anim.SetTrigger("move");
    }


}
