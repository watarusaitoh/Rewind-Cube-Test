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


    // Start is called before the first frame update
    void Start()
    {
        Button = GameObject.Find("Button");
        MainCamera = GameObject.Find("Main Camera");
        CameraRange = GameObject.Find("CameraRange");
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        MoveSwipe();
    }
    //カメラの回転についての関数
    void MoveCamera()
    {
        //Aが押されたとき左回転Dが押されたとき右回転
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateSpeed_X = -1f;
        }
        if (Input.GetKeyDown(KeyCode.A))

        {
            RotateSpeed_X = 1f;
        }
        //離されたとき止まる
        if (Input.GetKeyUp(KeyCode.D))
        {
            RotateSpeed_X = 0f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RotateSpeed_X = 0f;
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

        /*if (Input.GetKeyUp(KeyCode.W))
        {
            RotateSpeed_Y = 0f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            RotateSpeed_Y = 0f;
        }*/
        this.transform.eulerAngles = new Vector3(RotateSpeed_Y, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        // this.transform.Rotate(this.RotateSpeed_Y, 0, 0, Space.Self);
        this.transform.RotateAround(this.transform.position, Vector3.up, RotateSpeed_X);
        this.Button.transform.rotation *= Quaternion.AngleAxis(RotateSpeed_X, Vector3.forward);
    }

    void MoveSwipe()
    {
        //マウスの左クリックまたは画面が押されたとき
        if (Input.GetMouseButtonDown(0))
        {
            //押した座標を取得
            this.StartPos = Input.mousePosition;
        }
        //離されたとき
        else if (Input.GetMouseButton(0))
        {
            //離した座標を取得
            this.EndPos = Input.mousePosition;
            this.SwipeLength_X = EndPos.x - StartPos.x;
            this.SwipeLength_Y = EndPos.y - StartPos.y;

            //スワイプの長さを速度に変換
            this.X_Speed = SwipeLength_X / 250.0f;
            this.Y_Speed += SwipeLength_Y / 250.0f;

        }
        //角度制御ができれば追加できる
        // this.transform.eulerAngles = new Vector3(Y_Speed, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        this.transform.RotateAround(this.transform.position, Vector3.up, this.X_Speed);
        this.Button.transform.rotation *= Quaternion.AngleAxis(X_Speed, Vector3.forward);
        this.X_Speed *= 0.98f;
    }
}
