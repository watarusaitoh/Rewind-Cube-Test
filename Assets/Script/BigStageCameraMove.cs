using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStageCameraMove : MonoBehaviour
{
    private float RotateSpeed_X;
    private float RotateSpeed_Y;
    private GameObject Button;
    Vector2 StartPos;
    Vector2 EndPos;
    private float SwipeLength_X;
    private float SwipeLength_Y;
    private float X_Speed;
    private float Y_Speed;
    private GameObject MainCamera;
    private float CameraPositionRange = 80f;
    private GameObject CameraRange;
    private GameObject Cube1;
    private GameObject Cube2;
    private Vector3 RockOnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Button = GameObject.Find("Button");
        MainCamera = GameObject.Find("Main Camera");
        CameraRange = GameObject.Find("CameraRange");
        this.Cube1 = GameObject.Find("Cube1");
        this.Cube2 = GameObject.Find("Cube2");
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        MoveSwipe();
        RockOn();
    }
    //カメラの回転についての関数
    void MoveCamera()
    {
        //Aが押されたとき左回転Dが押されたとき右回転
        if (Input.GetKey(KeyCode.D))
        {
            RotateSpeed_X -= 1f;
        }
        if (Input.GetKey(KeyCode.A))

        {
            RotateSpeed_X += 1f;
        }
        //Wが押されたとき上回転Sが押されたとき下回転
        if (Input.GetKey(KeyCode.W) && this.CameraRange.transform.position.y < CameraPositionRange)
        {
            RotateSpeed_Y += 1f;
        }
        if (Input.GetKey(KeyCode.S) && this.CameraRange.transform.position.y > -CameraPositionRange)
        {
            RotateSpeed_Y -= 1f;
        }

        this.transform.eulerAngles = new Vector3(RotateSpeed_Y, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,RotateSpeed_X,this.transform.eulerAngles.z);
        this.Button.transform.eulerAngles = new Vector3(0, 0, RotateSpeed_X);
    }

    void MoveSwipe()
    {
        //マルチタッチではないとき処理される
        if (Input.touchCount < 2)
        {
            //マウスの左クリックまたは画面がタッチされたとき
            if (Input.GetMouseButtonDown(0))
            {
                //座標を取得
                this.StartPos = Input.mousePosition;
            }
            //タッチしている場所が動いた時
            else if (Input.GetMouseButton(0))
            {
                //離した座標を取得
                this.EndPos = Input.mousePosition;
                this.SwipeLength_X = EndPos.x - StartPos.x;
                this.SwipeLength_Y = EndPos.y - StartPos.y;

                //スワイプの長さを速度に変換
                this.X_Speed += SwipeLength_X / 250.0f;
                if (this.CameraRange.transform.position.y < this.CameraPositionRange&&this.CameraRange.transform.position.y>this.CameraPositionRange)
                {
                    this.Y_Speed += SwipeLength_Y / 250.0f;
                }
            }
        }
        //縦の動き
        this.transform.RotateAround(this.transform.position,Vector3.right,Y_Speed);
        //横の動き
        this.transform.RotateAround(this.transform.position,Vector3.up,X_Speed);
        //ボタンの回転
        this.Button.transform.rotation *= Quaternion.AngleAxis(X_Speed, Vector3.forward);
    }
    void RockOn()
    {
        if (this.Cube1.GetComponent<CubeController>().enabled == true)
        {
            //ステージの回転が終わった時、Cube2から交代した時徐々にCube1に近づける
            RockOnPoint = Vector3.Lerp(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector3(this.Cube1.transform.position.x, this.Cube1.transform.position.y, this.Cube1.transform.position.z), Time.deltaTime * 2);
            this.transform.position = RockOnPoint;
            
        }
        else if (this.Cube2.GetComponent<CubeController>().enabled == true)
        {
            //ステージの回転が終わった時、Cube1から交代した時徐々にCube2に近づける
            RockOnPoint = Vector3.Lerp(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector3(this.Cube2.transform.position.x, this.Cube2.transform.position.y, this.Cube2.transform.position.z), Time.deltaTime * 2);
            this.transform.position = RockOnPoint;
        }
        else
        {
            //Stageが回転するときに徐々にゼロに近づく
            RockOnPoint = Vector3.Lerp(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector3(0, 0, 0), Time.deltaTime * 2);
            this.transform.position = RockOnPoint;
        }
    }
}
