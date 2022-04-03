using System;
using UnityEditor.UIElements;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject RespawnGameObject;
    public ParallaxBackground[] BackgroundToSetTarget;
    public ParallaxElement[] BackgroundToParallax;

    public void Start()
    {
//        Debug.Log("The fist assign " + transform.name);
        InitTargetBrainWithBackground(transform);

        GameObject go = new GameObject(nameof(KeyCatcherController), typeof(KeyCatcherController));
        go.transform.parent = transform;
    }

    public void InitTargetBrainWithBackground(Transform brain)
    {
        foreach (ParallaxBackground parallaxBackground in BackgroundToSetTarget)
        {
            parallaxBackground.FollowingTarget = brain;
        }
    }    
    
    public void InitTargetBrainWithBackgroundFromTransform(Transform brain)
    {
        foreach (ParallaxElement t in BackgroundToParallax)
        {
//            Transform parallaxElement = Instantiate(t.Background, transform.position, Quaternion.identity);
            ParallaxBackground parallaxBackground = t.Background.gameObject.AddComponent<ParallaxBackground>();
            parallaxBackground.ParallaxStrength = t.GetStrength();
            parallaxBackground.OffsetPosition = t.Offset;
//            parallaxBackground.OffsetPosition = new Vector3(transform.position.x - t.Background.transform.position.x, 0, 0);
//            Debug.Log("transform.position.x" + transform.position.x);
//            Debug.Log("t.Background.transform.position.x" + t.Background.transform.position.x);
//            t.Background.position = transform.position;
//            Debug.Log("t.Background.transform.position.x = transform.position" + t.Background.transform.position.x);
            //            Vector3 localScale = t.Background.localScale;
            //            t.Background.localScale = Vector3.one;
            //            t.Background.localScale = localScale;
            parallaxBackground.FollowingTarget = brain;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (RespawnGameObject)
        {
            BrainCharacterController bcc = FindObjectOfType<BrainCharacterController>();

            if (!bcc)
            {
                GameObject theBrain = Instantiate(RespawnGameObject, transform.position, Quaternion.identity);

                InitTargetBrainWithBackgroundFromTransform(theBrain.transform);
            }
        }
    }
}

[Serializable]
public struct ParallaxElement
{
    public Transform Background;
    [Tooltip("Сила паралакса в процентах")][Range(0, 100)] 
    public int Strength;
    [Tooltip("Сдвиг картинки по осям; позволяет картинке следовать не от точки спауна")]
    public Vector3 Offset;

    public float GetStrength()
    {
        return (float)Strength / (float)100;
    }
}