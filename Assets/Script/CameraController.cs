using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    private GameObject CP;
    private float RotateSpeed;
    private GameObject Button;
    private float Speed = 0.02f;
    //private Vector3 touchStartPos;
    //private Vector3 touchEndPos;


    // Start is called before the first frame update
    void Start()
    {
        CP = GameObject.Find("CameraPoint");
        Button = GameObject.Find("Button");
    }

    // Update is called once per frame
    void Update()
    {
        //カメラを指定したオブジェクトを軸に回転
        MoveCamera();
    }
    //カメラの回転についての関数
    void MoveCamera()
    {
        //Aが押されたとき左回転Dが押されたとき右回転
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateSpeed = -1f;
        }
        if (Input.GetKeyDown(KeyCode.A))

        {
            RotateSpeed = 1f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RotateSpeed = 0f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RotateSpeed = 0f;
        }
        this.transform.RotateAround(this.CP.transform.position, Vector3.up, RotateSpeed);
        this.Button.transform.rotation *= Quaternion.AngleAxis(RotateSpeed, Vector3.forward);
    }


   /* void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
    }
    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        string Direction;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (30 < directionY)
            {
                //上向きにフリック
                Direction = "up";
            }
            else if (-30 > directionY)
            {
                //下向きにフリック
                Direction = "down";
            }
        }
        else
        {
            //タッチを検出
            Direction = "touch";
        }
    }
        */
}
