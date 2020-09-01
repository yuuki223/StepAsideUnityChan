using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenetator : MonoBehaviour
{
    //carPrefabを入れる
　　 public GameObject carPrefab;
    //coinPrefabを入れる
　　　public GameObject coinPrefab;
    //conePrefabを入れる
　　　public GameObject conePrefab;
    //スタート地点
　　　private int startPos = 40;
    //ゴール地点
　　　private int goalPos = 340;
    //アイテムを出すx方向の範囲
　　　private float posRange = 3.4f;

    private GameObject unitychan;

    private float unityPosz;

　　　    
    // Start is called before the first frame update
    void Start()
    { 
        this.unitychan = GameObject.Find("unitychan");
        this.unityPosz = unitychan.transform.position.z;
        
               //どのアイテムを出すのかランダムに設定
　　　　　　　　　int num = Random.Range (1,11);   
　　　　　　　　　if(num <= 2)
              {
                 //コーンをx軸方向に一直線に生成
　　　　　　　　　　　for(float j = -1; j <= 1; j += 0.4f)
                 { 
                     GameObject cone = Instantiate (conePrefab);
                     cone.transform.position = new Vector3 (4 * j, cone.transform.position.y,startPos);
                 }
              }
              else
              {
                  //レーンごとにアイテムを生成
　　　　　　　　　　　for (int j = -1; j<= 1; j++)
                 {
                      //アイテムの種類を決める
　　　　　　　　　　　　　　int item = Random.Range (1,11);
                      //アイテムを置くZ座標のオフセットをランダムに設定
　　　　　　　　　　　　　　int offsetZ = Random.Range(1,11);
                      //60％コイン配置：30％車配置：10％何もなし
　　　　　　　　　　　　　　if (1<= item && item<=6)
                      {
                          //コインを生成
　　　　　　　　　　　　　　　　GameObject coin = Instantiate (coinPrefab);
                         coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y,startPos + offsetZ);
                      }
                      else if (7 <= item && item <= 9)
                      {
                          //車を生成
　　　　　　　　　　　　　　　　GameObject car = Instantiate (carPrefab);
                         car.transform.position = new Vector3 (posRange * j, car.transform.position.y,startPos + offsetZ);
                      }
                   }
               }
            
    }

    // Update is called once per frame
    void Update()
    {
        float unitymovPosz = unitychan.transform.position.z;

        if (startPos < goalPos)
        {

            if (unitymovPosz - unityPosz >= 10)
            {
                startPos += 15;
                unityPosz += 15;

                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, startPos);
                    }
                }
                else
                {
                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(1, 11);
                        //60％コイン配置：30％車配置：10％何もなし
                        if (1 <= item && item <= 6)
                        {
                            //コインを生成
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, startPos + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, startPos + offsetZ);
                        }
                    }
                }
            }
        }
    }
}
