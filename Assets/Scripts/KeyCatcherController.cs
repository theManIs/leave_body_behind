
using UnityEngine;

public class KeyCatcherController : MonoBehaviour
{
    public bool ReleasedLock;

    public bool Space => ReleasedLock && Input.GetKeyDown(KeyCode.Space);
    public float Horizontal => ReleasedLock ? Input.GetAxis("Horizontal") : 0f;
    public float Vertical => ReleasedLock ? Input.GetAxis("Vertical") : 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape");
            Application.Quit();
        }
    }
}
