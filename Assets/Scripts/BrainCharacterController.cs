using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
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
    private GameObject _poc;
    private CircleCollider2D _col;

//    public void OnCollisionEnter(Collision c)
//    {
////        if (IsAirborn)
////        {
//            Debug.Log(c);
////        }
//    }

    public void Start()
    {
        _poc = new GameObject("pointOfContact");
        _col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizonShift = Input.GetAxis("Horizontal");
        int horizonSign = Math.Sign(horizonShift);

        if (rb2 is null)
        {
            rb2 = GetComponent<Rigidbody2D>();
        }
        if (!animco)
        {
            animco = GetComponent<BrainAnimationController>();
        }

//        Debug.DrawRay(transform.position, Vector3.right * Input.GetAxis("Horizontal"), Color.yellow);
        if (!Input.GetAxis("Horizontal").Equals(0.0f))
        {
            Vector2 brainPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 offsetX = new Vector2(_col.radius + _col.offset.x * horizonSign, 0);
            RaycastHit2D rh = Physics2D.Raycast(brainPos + offsetX * horizonSign, Vector2.right * horizonSign);

            if (rh)
            {
//                Debug.Log("name " + rh.transform.name + " offsetX " + offsetX.x + " distance " + Vector2.Distance(brainPos, rh.point));
                _poc.transform.position = rh.point;
            }


//            if (Physics.Raycast(new Ray(transform.position, Vector3.right * Input.GetAxis("Horizontal")), out RaycastHit hit, 100))
//            {
//                Debug.Log(hit);
//            }

            if (rh && Vector2.Distance(brainPos, rh.point) > (offsetX.x + 0.1f))
            {
                rb2.AddForce(Vector2.right * MoveSpeed * (IsAirborn ? AirbornDrag : 1) * Input.GetAxis("Horizontal"));
            }
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
        BoxCollider2D bc2c = GetComponent<BoxCollider2D>();
        Vector2 v2Collider = bc2c.size;
        v2Collider.y -= .3f;
        bc2c.size = v2Collider;
        Vector2 v2Offset = bc2c.offset;
        v2Offset.y = -.2f;
        bc2c.offset = v2Offset;

        gameObject.layer = 7;

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