using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAdder : MonoBehaviour
{
    public bool IsDroppable = false;

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.isTrigger && c.gameObject.TryGetComponent<BrainCharacterController>(out BrainCharacterController brain))
        {
//            Debug.Log(c.gameObject.name);

            BrainAnimationController animco = c.gameObject.GetComponent<BrainAnimationController>();

            if (animco)
            {
                animco.Die();
            }

//            BrainCharacterController brain = c.gameObject.GetComponent<BrainCharacterController>();

            if (brain)
            {
                brain.IsDead = true;

                if (IsDroppable)
                {
                    brain.DisableCharacterWithDelay(1);
                }
                else
                {
                    brain.DisableCharacterWithDelayFreeze(1);
                }
            }
        }

    }


}
