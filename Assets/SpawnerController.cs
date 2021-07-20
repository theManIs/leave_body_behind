using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject RespawnGameObject;

    // Update is called once per frame
    void Update()
    {
        if (RespawnGameObject)
        {
            BrainCharacterController bcc = FindObjectOfType<BrainCharacterController>();

            if (!bcc)
            {
                Instantiate(RespawnGameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
