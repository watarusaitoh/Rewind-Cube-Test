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
    CubeController script;               //CubeControllerから変数をもらう
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
        //変数をもらう
        script =Cube1.GetComponent<CubeController>();
    }
   
    // Update is called once per frame
    void Update()
    {
       
        if (isRotate)
            return;
        Cube1True = false;
        Cube2True = false;
        //CubeControllerから渡された変数
        bool RotateEnd = script.RotateEnd;
        if (RotateEnd)
        {
            distance_right = this.RS_R.transform.position.z - this.RE_R.transform.position.z;
            RaycastHit hit;
            Physics.Raycast(this.RS_R.transform.position, new Vector3(0f, 0f, -distance_right), out hit, distance_right);
            if (hit.collider != null && Input.GetKeyDown(KeyCode.RightArrow))
            {
                CubeOnOff();
                StartCoroutine(Intarval());
                rotateAxis = new Vector3(0f, 0f, 1f);
            }
            distance_Left = this.RS_L.transform.position.z - this.RE_L.transform.position.z;
            Physics.Raycast(this.RS_L.transform.position, new Vector3(0f, 0f, -distance_Left), out hit, distance_Left);
            if (hit.collider != null&&Input.GetKeyDown(KeyCode.LeftArrow ))
            {
                CubeOnOff();
                StartCoroutine(Intarval());
                rotateAxis = new Vector3(0f, 0f, -1f);
            }
            distance_forward = this.RS_R.transform.position.x - this.RS_L.transform.position.x;
            Physics.Raycast(this.RS_R.transform.position, new Vector3(-distance_forward, 0f, 0f), out hit, distance_forward);
            if (hit.collider != null && Input.GetKeyDown(KeyCode.UpArrow))
            {
                CubeOnOff();
                StartCoroutine(Intarval());
                rotateAxis = new Vector3(-1f, 0f, 0f);
            }
            distance_back = this.RE_R.transform.position.x - this.RE_L.transform.position.x;
            Physics.Raycast(this.RE_R.transform.position, new Vector3(-distance_back, 0f, 0f), out hit, distance_back);
            if (hit.collider != null && Input.GetKeyDown(KeyCode.DownArrow))
            {
                CubeOnOff();
                StartCoroutine(Intarval());
                rotateAxis = new Vector3(1f, 0f, 0f);
            }

            if (rotateAxis == Vector3.zero)
                return;
        }
            StartCoroutine(MoveCube());
       
       
    }
    IEnumerator MoveCube()
    {
        //回転中のフラグを立てる
        isRotate = true;
        sumRotate = 90f;
        this.script.RotateEnd = false;
        //回転処理
        float sumAngle = 0f;  //angleの合計を保存
        //transform.DORotate(new Vector3(0f,90f),1.0f);
        while (sumAngle <= this.sumRotate)
        {
            cubeAngle = 1f;  //ここを変えると回転速度が変わる
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
        rotatePoint = Vector3.zero;
        rotateAxis = Vector3.zero;
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
    //ステージが回転の際、Cubeを動かなくする
    IEnumerator Intarval()
    {
        yield return new WaitForSeconds(0.5f);
    }
    //CubeControllerのスクリプトをオンにする
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
}
