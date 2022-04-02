using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject RespawnGameObject;
    public ParallaxBackground[] BackgroundToSetTarget;

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

    // Update is called once per frame
    void Update()
    {
        if (RespawnGameObject)
        {
            BrainCharacterController bcc = FindObjectOfType<BrainCharacterController>();

            if (!bcc)
            {
                GameObject theBrain = Instantiate(RespawnGameObject, transform.position, Quaternion.identity);

                InitTargetBrainWithBackground(theBrain.transform);
            }
        }
    }
}
