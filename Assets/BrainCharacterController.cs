using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BrainCharacterController : MonoBehaviour
{
    public float MoveSpeed = 10;
    public float JumpHeight = 100;
    public float AirbornDrag = 0.5f;
    public bool JumpLock = false;
    public bool IsAirborn = false;


    private Rigidbody2D rb2;
    private BrainAnimationController animco;

    // Update is called once per frame
    void Update()
    {
        if (rb2 is null)
        {
            rb2 = GetComponent<Rigidbody2D>();
        }
        if (!animco)
        {
            animco = GetComponent<BrainAnimationController>();
        }

        if (!Input.GetAxis("Horizontal").Equals(0.0f))
        {
            rb2.AddForce(Vector2.right * MoveSpeed * (IsAirborn ? AirbornDrag : 1) * Input.GetAxis("Horizontal"));
        } 
        
        if (!IsAirborn && Input.GetKeyDown(KeyCode.Space))
        {
            rb2.AddForce(Vector2.up * JumpHeight);
        }

        CheckAirborn();
    }

    public void CheckAirborn()
    {
        RaycastHit2D rh2 = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Platform"));

        if (rh2.collider != null)
        {
//            float distance = Mathf.Abs(rh2.point.y - transform.position.y);
//            Debug.Log(rh2.collider.name + " " + distance);
            IsAirborn = false;
        }
        else
        {
            IsAirborn = true;
        }
    }

    public void DisableCharacter()
    {
        Destroy(rb2);
        Destroy(animco);
        Destroy(this);

        Camera cam = GetComponentInChildren<Camera>();

        if (cam)
        {
            Destroy(cam.gameObject);
        }
    }
}
