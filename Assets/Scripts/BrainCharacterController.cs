using System;
using UnityEngine;


public class BrainCharacterController : MonoBehaviour
{
    public float MoveSpeed = 10;
    public float JumpHeight = 100;
    public bool JumpLock = false;
    public bool IsAirborn = false;
    public bool IsDead = false;
    public float DistanceError = 0.5f;
    public int ColiderShrinkCountdown = 2;
    public float CameraDistance = 6;
    [Range(0, 10)] 
    public int AirbornDrag = 5;
    public RigidbodyProperties GravityScaleRigidbody = new RigidbodyProperties {Mass = 1, FloatGravity = 3, LinearDrag = 5};
    public KeyCatcherController Kcc;


    private Rigidbody2D rb2;
    private BrainAnimationController animco;
    private GameObject _poc;
    private CircleCollider2D _col;
    private BoxCollider2D _bc2c;
    private Camera _cam;

//    public void OnCollisionEnter(Collision c)
//    {
////        if (IsAirborn)
////        {
//            Debug.Log(c);
////        }
//    }

    private float GetMoveSpeed => Application.isEditor ? MoveSpeed : MoveSpeed * 2;

    public void Start()
    {
//        _poc = new GameObject("pointOfContact");
        _col = GetComponent<CircleCollider2D>();
        _bc2c = GetComponent<BoxCollider2D>();
        _cam = GetComponentInChildren<Camera>();
        _cam.orthographicSize = CameraDistance;
        rb2 = GetComponent<Rigidbody2D>();
        GravityScaleRigidbody.Rb2 = rb2;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead) return;
        GravityScaleRigidbody.SetGravityScale().SetMass().SetLinearDrag();

        float horizonShift = Kcc.Horizontal;
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
        if (!Kcc.Horizontal.Equals(0.0f))
        {
            Vector2 brainPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 offsetX = new Vector2(_col.radius + _col.offset.x * horizonSign, 0);
            RaycastHit2D rh = Physics2D.Raycast(brainPos + offsetX * horizonSign, Vector2.right * horizonSign, 10);

//            if (rh)
//            {
//                Debug.Log("name " + rh.transform.name + " offsetX " + offsetX.x + " distance " + Vector2.Distance(brainPos, rh.point));
//                _poc.transform.position = rh.point;
//            }


//            if (Physics.Raycast(new Ray(transform.position, Vector3.right * Input.GetAxis("Horizontal")), out RaycastHit hit, 100))
//            {
//                Debug.Log(hit);
//            }


            if (!rh || Vector2.Distance(brainPos, rh.point) > (offsetX.x + 0.1f))
            {
                rb2.AddForce(Vector2.right * GetMoveSpeed * (IsAirborn ? AirbornDrag / 5 : 1) * Kcc.Horizontal);
            }

            animco.Walk();
        }
        
        if (!IsAirborn && Kcc.Space)
        {
            rb2.AddForce(Vector2.up * JumpHeight);
            animco.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            animco.Smoke();
            rb2.constraints = RigidbodyConstraints2D.FreezePositionX;
            IsDead = true;
            Invoke(nameof(DisableCharacter), 3.5f);
        }

        CheckAirborn();
    }

    public void CheckAirborn()
    {
//        Vector2 raycastPointRight = new Vector2(transform.position.x + (_bc2c.size.x / 2f), transform.position.y - _col.radius + _col.offset.y);
//        Vector2 raycastPointLeft = new Vector2(transform.position.x - (_bc2c.size.x / 2f), transform.position.y - _col.radius + _col.offset.y);
//        Vector2 raycastPointCenter = new Vector2(transform.position.x - (_bc2c.size.x / 2f), transform.position.y - _col.radius + _col.offset.y);
        Vector2 raycastPointRight = new Vector2(transform.position.x + _col.radius + _col.offset.x + 0.0001f, transform.position.y + _col.offset.y - 0.0001f);
        Vector2 raycastPointLeft = new Vector2(transform.position.x - _col.radius + _col.offset.x - 0.0001f, transform.position.y + _col.offset.y - 0.0001f);
        Vector2 raycastPointCenter = new Vector2(transform.position.x + _col.offset.x, transform.position.y - _col.radius + _col.offset.y - 0.0001f);
        RaycastHit2D rh0 = Physics2D.Raycast(raycastPointRight, Vector2.down, DistanceError + _col.radius);
        RaycastHit2D rh1 = Physics2D.Raycast(raycastPointLeft, Vector2.down, DistanceError + _col.radius);
        RaycastHit2D rh11 = Physics2D.Raycast(raycastPointCenter, Vector2.down, DistanceError);
        RaycastHit2D rh2 = rh0 ? rh0 : rh1 ? rh1 : rh11;
//        Debug.Log(rh2.collider.name);
//        RaycastHit2D rh2 = Physics2D.Raycast(transform.position, Vector2.down, DistanceError, LayerMask.GetMask("Platform"));

        if (rh2.collider != null)
        {
            if (IsAirborn)
            {
                float distance = Mathf.Abs(rh2.point.y - transform.position.y);
//                Debug.Log(rh2.collider.name + " " + gameObject.GetInstanceID() + " " + Time.frameCount);

                if (rh2 && rh2.transform.TryGetComponent(out Rigidbody2D rbPush) && rh2.transform.TryGetComponent(out DeadBodyController brain) && ColiderShrinkCountdown > 0)
                {
//                    Debug.Log(transform.position.y + " " + _col.radius + " " + _col.offset.y + " " + (transform.position.y - _col.radius + _col.offset.y));
//                    _poc.transform.position = rh.point;
//                    Debug.Log("gm name \"" + rbPush.name + "\"" + " " + brain);
//                    rbPush.AddForce(Vector2.down * 10);
//                    EditorApplication.isPaused = true;
//                    rbPush.gravityScale = 1;

                    BoxCollider2D bc2d = rh2.transform.GetComponent<BoxCollider2D>();
                    Vector2 chw = bc2d.size;
                    chw.y -= 0.1f;
                    chw.x -= 0.2f;
                    bc2d.size = chw;

                    ColiderShrinkCountdown -= 1;
                }
            }

            IsAirborn = false;
        }
        else
        {
//            Debug.Log( Time.frameCount);
            IsAirborn = true;
        }
    }

    /// <summary>
    /// Disables character after {delay} seconds
    /// </summary>
    /// <param name="delay">Delay in seconds before character is disabled</param>
    public void DisableCharacterWithDelay(int delay)
    {
        Invoke(nameof(DisableCharacter), delay);
    }

    public void DropBodyDown()
    {
        rb2.AddForce(Vector2.down);
    }

    /// <summary>
    /// Disables character after {delay} seconds and freezes all position axis
    /// </summary>
    /// <param name="delay">Delay in seconds before character is disabled</param>
    public void DisableCharacterWithDelayFreeze(int delay)
    {
        rb2.constraints = RigidbodyConstraints2D.FreezeAll;

        DisableCharacterWithDelay(delay);
    }

    public void DisableCharacter()
    {
        gameObject.name = "Dead " + name;

        _col.enabled = false;
        _bc2c.enabled = true;
        Vector2 v2Collider = _bc2c.size;
        v2Collider.y -= .3f;
        _bc2c.size = v2Collider;
        Vector2 v2Offset = _bc2c.offset;
        v2Offset.y = -.2f;
        _bc2c.offset = v2Offset;

        gameObject.layer = 7;
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
//        rb2.gravityScale = 0;
//        Destroy(rb2);
        Destroy(animco);
        Destroy(this);
        gameObject.AddComponent<DeadBodyController>();

        _cam.GetComponent<AudioListener>().enabled = false;

        if (_cam)
        {
            Destroy(_cam.gameObject, 0.5f);
        }

        AudioListener aul = GetComponent<AudioListener>();

        if (aul)
        {
            aul.enabled = false;
        }
    }
}

[Serializable]
public struct RigidbodyProperties
{
    public Rigidbody2D Rb2;

    [Range(0, 10)]
    public float FloatGravity;
    private float _previousFloatValue;

    [Range(0, 10)]
    public float Mass;
    private float _previousFloatMass;

    [Range(0, 10)]
    public float LinearDrag;
    private float _previousLinearDrag;

    public RigidbodyProperties SetGravityScale()
    {
        if (!_previousFloatValue.Equals(FloatGravity))
        {
            _previousFloatValue = FloatGravity;
            Rb2.gravityScale = FloatGravity;
        }

        return this;
    }

    public RigidbodyProperties SetMass()
    {
        if (!_previousFloatMass.Equals(Mass))
        {
            _previousFloatMass = Mass;
            Rb2.mass = Mass;
        }

        return this;
    }

    public RigidbodyProperties SetLinearDrag()
    {
        if (!_previousLinearDrag.Equals(LinearDrag))
        {
            _previousLinearDrag = LinearDrag;
            Rb2.drag = LinearDrag;
        }

        return this;
    }
}