using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        Button = GameObject.Find("Button");
    }

    // Update is called once per frame
    void Update()
    {
        //カメラを指定したオブジェクトを軸に回転
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
        if (Input.GetKeyUp(KeyCode.D))
        {
            RotateSpeed_X = 0f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RotateSpeed_X = 0f;
        }
        if (Input.GetKeyDown(KeyCode.W)&&this.transform.localEulerAngles.x<=90)
        {
            RotateSpeed_Y = 1f;

        }
        if (Input.GetKeyDown(KeyCode.S)&& this.transform.localEulerAngles.x >= -90)
        {
            RotateSpeed_Y = -1f;
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            RotateSpeed_Y = 0f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            RotateSpeed_Y = 0f;
        }
        this.transform.Rotate(this.RotateSpeed_Y, 0, 0, Space.Self);
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
            this.Y_Speed = SwipeLength_Y / 250.0f;
        }
        this.transform.Rotate(-this.Y_Speed,0, 0,Space.Self);
        this.transform.RotateAround(this.transform.position, Vector3.up, this.X_Speed);
        this.Button.transform.rotation *= Quaternion.AngleAxis(X_Speed, Vector3.forward);
        this.X_Speed *= 0.98f;
        this.Y_Speed *= 0.98f;
    }
}
