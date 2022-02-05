using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform FollowingTarget;
    public float ParallaxStrength = 1;
    public bool DisableVerticalParallax = true;
    public Vector3 OffsetPosition = Vector3.zero;

    private Vector3 _targetPreviousPosition;

    // Start is called before the first frame update
    void Start()
    {
//        if (!FollowingTarget)
//        {
//            FollowingTarget = Camera.main.transform;    
//        }

        _targetPreviousPosition = FollowingTarget.position;

        transform.position = _targetPreviousPosition / transform.localScale.y + OffsetPosition;

    }

    // Update is called once per frame
    void Update()
    {
//        if (FollowingTarget)
//        {
            Vector3 deltaVector3 = FollowingTarget.position - _targetPreviousPosition;

            if (DisableVerticalParallax)
            {
                deltaVector3.x = deltaVector3.x * ParallaxStrength;
            }
            else
            {
                deltaVector3 *= ParallaxStrength;
            }

            _targetPreviousPosition = FollowingTarget.position;
            transform.position += deltaVector3;
//            transform.position += OffsetPosition;
//        }
    }
}
