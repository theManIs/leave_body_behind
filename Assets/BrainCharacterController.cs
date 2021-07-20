using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCharacterController : MonoBehaviour
{
    public float MoveSpeed = 10;
    public float JumpHeight = 100;
    public bool JumpLock = false;


    private Rigidbody2D rb2;

    // Update is called once per frame
    void Update()
    {
        if (rb2 is null)
        {
            rb2 = GetComponent<Rigidbody2D>();
        }

        if (!Input.GetAxis("Horizontal").Equals(0.0f))
        {
            rb2.AddForce(Vector2.right * MoveSpeed * Input.GetAxis("Horizontal"));
        } 
        
        if (!JumpLock && Input.GetKeyDown(KeyCode.Space))
        {
            rb2.AddForce(Vector2.up * JumpHeight);

            Invoke("ReleaseJumpLock", 1);
        }
    }

    public void ReleaseJumpLock() => JumpLock = false;
}
