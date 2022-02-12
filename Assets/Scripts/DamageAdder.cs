using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAdder : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.isTrigger)
        {
//            Debug.Log(c.gameObject.name);

            BrainAnimationController animco = c.gameObject.GetComponent<BrainAnimationController>();

            if (animco)
            {
                animco.Die();
            }

            BrainCharacterController brain = c.gameObject.GetComponent<BrainCharacterController>();

            if (brain)
            {
                brain.DisableCharacter();
            }
        }

    }
}
