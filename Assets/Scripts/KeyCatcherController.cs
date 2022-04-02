
using UnityEngine;

public class KeyCatcherController : MonoBehaviour
{
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
