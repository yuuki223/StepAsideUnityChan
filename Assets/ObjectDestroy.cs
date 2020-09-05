using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{

    private GameObject camera;

    private float cameraPosz;
    // Start is called before the first frame update
    void Start()
    {
        this.camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosz = camera.transform.position.z;

        if (this.transform.position.z<cameraPosz)
        {
            Destroy(this.gameObject);
        }  
    }
    
   
}
