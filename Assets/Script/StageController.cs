using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageController : MonoBehaviour
{
    Vector3 rotatePoint = Vector3.zero;  //回転の中心
    Vector3 rotateAxis = Vector3.zero;   //回転軸
    float cubeAngle = 0f;                //回転角度
    bool isRotate = false;               //回転中に立つフラグ、回転中は入力を受け付けない
    private float sumRotate;             //回転する合計
    GameObject RS_R;                     //右奥のオブジェクト
    GameObject RE_R;                     //右手前のオブジェクト
    GameObject RS_L;                     //左奥のオブジェクト
    GameObject RE_L;                     //左手前オブジェクト
    GameObject Cube1;                    //Cube1を入れる
    GameObject Cube2;                    //Cube2を入れる
    float distance_right;　　　　　　　　//右のレイの長さ
    float distance_Left;                 //左のレイの長さ
    float distance_forward;              //奥のレイの長さ
    float distance_back;                 //手前のレイの長さ
    bool Cube1True;                      //Cube1のスクリプトが外れたかを判断する変数
    bool Cube2True;　　　　　　　　　　　//Cube2のスクリプトが外れたかを判断する変数
    CubeController scriptCube1;          //CubeControllerから変数をもらう
    CubeController scriptCube2;          //CubeControllerから変数をもらう
    private int layermask;               //Raycastの検知を指定する変数
    private bool RButton;          //RightButtonを入れる
    private bool LButton;          //LeftButtonを入れる
    private bool FButton;          //ForwardButtonを入れる
    private bool BButton;          //BuckButtonを入れる

    // Start is called before the first frame update
    void Start()
    {
        //レイのオブジェクトを取得
        RS_R = GameObject.Find("RayStart(Right)");
        RE_R = GameObject.Find("RayEnd(Right)");
        RS_L = GameObject.Find("RayStart(Left)");
        RE_L = GameObject.Find("RayEnd(Left)");
        //Cube1を取得する
        Cube1 = GameObject.Find("Cube1");
        //Cube2を取得する
        Cube2 = GameObject.Find("Cube2");
        //CubeControllerから変数をもらう
        scriptCube1 = Cube1.GetComponent<CubeController>();
        scriptCube2 = Cube2.GetComponent<CubeController>();
    }
   
    // Update is called once per frame
    void Update()
    {
       
        if (isRotate)
            return;
        Cube1True = false;
        Cube2True = false;
        //RayCastがCubeControllerが有効なCubeにのみ反応する
        if(this.Cube1.GetComponent<CubeController>().enabled == true)
        {
            layermask = LayerMask.GetMask(new string[] { "Cube1" });
        }
        else if(this.Cube2.GetComponent<CubeController>().enabled == true)
        {
            layermask = LayerMask.GetMask(new string[] { "Cube2" });
        }
        rotatePoint = transform.position;
        //CubeControllerから渡された変数
        bool RotateEndCube1 = scriptCube1.RotateEnd;
        bool RotateEndCube2 = scriptCube2.RotateEnd;
        if (RotateEndCube1||RotateEndCube2)
        {
            //RaycastがColliderを検知する
            distance_right = this.RS_R.transform.position.z - this.RE_R.transform.position.z;
            RaycastHit hit;
            Physics.Raycast(this.RS_R.transform.position, new Vector3(0f, 0f, -distance_right), out hit, distance_right,layermask);
            if (hit.collider != null&&Input.GetKeyDown(KeyCode.RightArrow)||hit.collider != null&&RButton)
            {
                //CubeControllerを無効にするにする
                CubeOnOff();
                rotateAxis = new Vector3(0f, 0f, 1f);
            }
            distance_Left = this.RS_L.transform.position.z - this.RE_L.transform.position.z;
            Physics.Raycast(this.RS_L.transform.position, new Vector3(0f, 0f, -distance_Left), out hit, distance_Left,layermask);
            if (hit.collider != null&&Input.GetKeyDown(KeyCode.LeftArrow) || hit.collider != null && LButton)
            {
                CubeOnOff();
                rotateAxis = new Vector3(0f, 0f, -1f);
            }
            distance_forward = this.RS_R.transform.position.x - this.RS_L.transform.position.x;
            Physics.Raycast(this.RS_R.transform.position, new Vector3(-distance_forward, 0f, 0f), out hit, distance_forward,layermask);
            if (hit.collider != null&&Input.GetKeyDown(KeyCode.UpArrow) || hit.collider != null && FButton)
            {
                CubeOnOff();
                rotateAxis = new Vector3(-1f, 0f, 0f);
            }
            distance_back = this.RE_R.transform.position.x - this.RE_L.transform.position.x;
            Physics.Raycast(this.RE_R.transform.position, new Vector3(-distance_back, 0f, 0f), out hit, distance_back,layermask);
            if (hit.collider != null&&Input.GetKeyDown(KeyCode.DownArrow) || hit.collider != null && BButton)
            {
                CubeOnOff();
                rotateAxis = new Vector3(1f, 0f, 0f);
            }
            //Cubeが側面に位置し且つチェンジしたときにStageを回転させる
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Cube1とCube2の間の角度を取得する
                float angle = Vector3.Angle(scriptCube1.nomal, scriptCube2.nomal); 
                if (angle ==90f)
                {
                    CubeOnOff();
                    rotateAxis = new Vector3(0f, 0f, 1f);
                }
                else if(angle == -90)
                {
                    CubeOnOff();
                    rotateAxis = new Vector3(0f, 0f, -1f);
                }
                else if (angle ==180)
                {
                    CubeOnOff();
                    rotateAxis = new Vector3(0f, 0f, 1f);
                }
               
            }
            if (rotateAxis == Vector3.zero)
                return;
        }
        StartCoroutine(MoveStage());
       
       
    }
    IEnumerator MoveStage()
    {
        //回転中のフラグを立てる
        isRotate = true;
        sumRotate = 90f;
        this.scriptCube1.RotateEnd = false;
        this.scriptCube2.RotateEnd = false;
        //回転処理
        float sumAngle = 0f;  //angleの合計を保存
        //transform.DORotate(new Vector3(0f,90f),1.0f);
        while (sumAngle <= this.sumRotate)
        {
            cubeAngle = 0.6f;  //ここを変えると回転速度が変わる
            sumAngle += cubeAngle;


            //sumRotateの角度以上回転しないように値を制限
            if (sumAngle > this.sumRotate)
            {

                cubeAngle -= sumAngle - this.sumRotate;
                
            }
            this.transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);
            yield return null;
        }
        //回転中のフラグを倒す
        
        isRotate = false;
        //rotatePoint = Vector3.zero;  //rotatePointはオブジェクト中心で動くため不要
        rotateAxis = Vector3.zero;
        //CubeControllerを有効にする
        if (Cube1True)
        {
            Cube1.GetComponent<CubeController>().enabled = true;
        }
        else if (Cube2True)
        {
            Cube2.GetComponent<CubeController>().enabled = true;
        }
        yield break;
    }
   
    //ステージの回転中はCubeControllerのスクリプトを無効にする
    void CubeOnOff()
    {
        if (Cube1.GetComponent<CubeController>().enabled == true)
        {
            Cube1.GetComponent<CubeController>().enabled = false;
            Cube1True = true;
        }
        else if (Cube2.GetComponent<CubeController>().enabled == true)
        {
            
            Cube2.GetComponent<CubeController>().enabled = false;
            Cube2True = true;
        }
    }
    //Uiのボタンを押した時に矢印の方向に進む
    public void GetRButtonDown()
    {
        this.RButton = true;
    }
    public void GetRButtonUp()
    {
        this.RButton = false;
    }
    public void GetLButtonDown()
    {
        this.LButton = true;
    }
    public void GetLButtonUp()
    {
        this.LButton = false;
    }
    public void GetFButtonDown()
    {
        this.FButton = true;
    }
    public void GetFButtonUp()
    {
        this.FButton = false;
    }
    public void GetBButtonDown()
    {
        this.BButton = true;
    }
    public void GetBButtonUp()
    {
        this.BButton = false;
    }
}
