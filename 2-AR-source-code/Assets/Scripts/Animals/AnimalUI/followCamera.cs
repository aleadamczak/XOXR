using UnityEngine;

public class followCamera : MonoBehaviour
{

    Camera m_MainCamera;
    void Start()
    {
        //This gets the Main Camera from the Scene
        if (Camera.main != null)
        {
            m_MainCamera = Camera.main;
            //This enables Main Camera
            m_MainCamera.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation= Camera.main.transform.rotation; 
    }
}
