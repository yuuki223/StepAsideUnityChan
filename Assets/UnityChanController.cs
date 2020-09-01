using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
　　　private Animator myAnimator;
　　 //Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;
    //前方向の速度（追加）
    private float velocityZ = 16f;
    //横方向の速度（追加）
    private float velocityX = 10f;
    //上方向の速度（追加）
　　 private float velocityY = 10f;
    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;
    //動きを減速させる係数（追加）
    private float coefficient = 0.99f;
    //ゲーム終了の判定（追加）
　　　private bool isEnd = false;
    //ゲーム終了時に表示するテキスト（追加）
    private GameObject stateText;
    //スコアを表示するテキスト（追加）
　　　private GameObject scoreText;
    //得点（追加）
　　 private int score = 0; 
    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
　　 private bool isRButtonDown = false;
    //ジャンプボタン押下の判定（追加）
　　　private bool isJButtonDown = false;  

    // Start is called before the first frame update
    void Start()
    {

          //Animatorコンポーネントを取得
          this.myAnimator = GetComponent<Animator>();
          
          //走るアニメーションを開始
　　　　　　this.myAnimator.SetFloat ("Speed",1);

          //Rigidbodyコンポーネントを取得（追加）
　　　　　　this.myRigidbody = GetComponent<Rigidbody>();
          
          //シーン中のstateTextオブジェクトを取得（追加）
　　　　　　this.stateText = GameObject.Find("GameResultText");
     
          //シーン中のscoreTextオブジェクトを取得（追加）
　　　　　　this.scoreText = GameObject.Find("ScoreText");
        
    }

    // Update is called once per frame
    void Update()
    {
          //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
　　　　　　if (this.isEnd)
          {
                           this.velocityZ *= this.coefficient;
                           this.velocityX *= this.coefficient;
                           this.velocityY *= this.coefficient;
                           this.myAnimator.speed *= this.coefficient;
          }

          //横方向の入力による速度（追加）
　　　　　　float inputVelocityX = 0;
          //上方向の入力による速度（追加）
　　　　　　float inputVelocityY = 0;

          //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
　　　　　　if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
          {
                           //左方向への速度を代入（追加）
　　　　　　　　　　　　　　　　　inputVelocityX = -this.velocityX;
          }
          else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
          {
                           //右方向への速度を代入（追加）
　　　　　　　　　　　　　　　　　inputVelocityX = this.velocityX;
          }
          
          //ジャンプしてない時にスペースが押されたらジャンプする（追加）
　　　　　　if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
          {
                           //ジャンプアニメを再生（追加）
　　　　　　　　　　　　　　　　　this.myAnimator.SetBool("jump",true);
                           //上方向への速度を代入（追加）
　　　　　　　　　　　　　　　　　inputVelocityY = this.velocityY;
          }
          else
          {
                           //現在のY軸の速度を代入（追加）
　　　　　　　　　　　　　　　　　inputVelocityY = this.myRigidbody.velocity.y;
          }
          
          //Jumpステートの場合はJumpにfalseをセットする（追加）
          if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jmp"))
          {
                   this.myAnimator.SetBool ("Jump",false);
          }
          //Unityちゃんに速度を与える（追加）
          this.myRigidbody.velocity = new Vector3(inputVelocityX,inputVelocityY,velocityZ);
        
    }
    //トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other)
    {
           //障害物に衝突した場合（追加）
　　　　　　　if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
           {
                   this.isEnd = true;
                   //stateTextにGAME OVERを表示（追加）
　　　　　　　　　　　　this.stateText.GetComponent<Text>().text = "GAME OVER";
           }
           
           //ゴール地点に到達した場合（追加）
　　　　　　　if (other.gameObject.tag == "GoalTag")
           {
                    this.isEnd = true;
                    //stateTextにGAME CLEARを表示（追加）
　　　　　　　　　　　　　this.stateText.GetComponent<Text>().text = "CLEAR!!";
           }
　　　　　　　
           //コインに衝突した場合（追加）
　　　　　　　if(other.gameObject.tag == "CoinTag")
           {
                    //スコアを加算（追加）
　　　　　　　　　　　　　this.score += 10;
                    //ScoreTextに獲得した点数を表示（追加）
　　　　　　　　　　　　　this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";
                    //パーティクルを再生（追加）
　　　　　　　　　　　　　GetComponent<ParticleSystem> ().Play();
                    //接触したコインのオブジェクトを破壊（追加）
　　　　　　　　　　　　　Destroy (other.gameObject);
           }
    }
    //ジャンプボタンを押した場合の処理（追加）
　　public void GetMyJumpButtonDown()
   {
                    this.isJButtonDown = true;
   }
   //ジャンプボタンを離した場合の処理（追加）
　　public void GetMyJumpButtonUp()
   {
                     this.isJButtonDown = false;
   }
   //左ボタンを押し続けた場合の処理（追加）
　　public void GetMyLeftButtonDown()
   {
                      this.isLButtonDown = true;
   }
   //左ボタンを離した場合の処理（追加）
　　public void GetMyLeftButtonUp()
   {
                       this.isLButtonDown = false;
   }
   //右ボタンを押し続けた場合の処理（追加）
　　public void GetMyRightButtonDown()
   {
                       this.isRButtonDown = true;
   }
   //右ボタンを離した場合の処理（追加）
　　public void GetMyRightButtonUp()
   {
                        this.isRButtonDown = false;
   }
   
}

