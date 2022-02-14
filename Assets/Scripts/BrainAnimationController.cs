using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainAnimationController : MonoBehaviour
{
    public SpriteRenderer sp;
    public Animator anim;
    public AudioSource asource;

    public AudioClip JumpSound;
    public AudioClip DeadSound;

    void Update()
    {
        if (!sp)
        {
            sp = GetComponent<SpriteRenderer>();
        }

        if (!anim)
        {
            anim = GetComponent<Animator>();
        }

        if (!asource)
        {
            asource = GetComponent<AudioSource>();
        }

        if (!Input.GetAxis("Horizontal").Equals(0.0f))
        {
            sp.flipX = Input.GetAxis("Horizontal") < 0;

            anim.SetTrigger("Walk");
        }
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");

        if (asource)
        {
            asource.clip = JumpSound;
            asource.Play();
        }
    }

    public void Die()
    {
        anim.SetTrigger("Dead");

        if (asource)
        {
            asource.clip = DeadSound;
            asource.Play();
        }
    }
}
