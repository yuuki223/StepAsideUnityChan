using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject camera;

    private float cameraPosz;

    // Start is called before the first frame update
    void Start()
    {
          //回転を開始する角度を設定
　　　　　　this.transform.Rotate (0,Random.Range (0,360),0);

         this.camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
         this.cameraPosz = camera.transform.position.z;
        

          //回転
　　　　　　this.transform.Rotate (0,3,0);

         if (this.transform.position.z < cameraPosz )
        {
            Destroy(this.gameObject);
        }
    }
   
}
