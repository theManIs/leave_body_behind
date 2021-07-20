using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainAnimationController : MonoBehaviour
{
    public SpriteRenderer sp;
    public Animator anim;

    // Update is called once per frame
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

        if (!Input.GetAxis("Horizontal").Equals(0.0f))
        {
            sp.flipX = Input.GetAxis("Horizontal") < 0;

            anim.SetTrigger("Walk");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }
    }

    public void Die()
    {
        anim.SetTrigger("Dead");
        
//        Invoke(nameof(StopPlayback), 1);
    }

//    public void StopPlayback() => anim.enabled = false;
}
