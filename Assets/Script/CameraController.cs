using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    
    private GameObject CP;
    private float RotateSpeed ;
    private Vector3 beforePoint;
    private Vector3 nowPoint;
    private float horizontalAngle;
    private GameObject Button;
    private GameObject RButton;
    
    // Start is called before the first frame update
    void Start()
    {
        CP = GameObject.Find("CameraPoint");
        Button = GameObject.Find("Button");
        RButton = GameObject.Find("RightButton");
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateSpeed = -1f;
        }
        if (Input.GetKeyDown(KeyCode.D))

        {  
            RotateSpeed = 1f;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RotateSpeed = 0f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RotateSpeed = 0f;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x >= Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    RotateSpeed = 1f;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    RotateSpeed = 0f;
                }
            }
            else
            {
                if (touch.phase == TouchPhase.Began)
                {
                    RotateSpeed = -1f;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    RotateSpeed = 0f;
                }
            }
        }
        this.transform.RotateAround(this.CP.transform.position, Vector3.up, RotateSpeed);
        this.Button.transform.rotation *= Quaternion.AngleAxis(RotateSpeed, Vector3.forward);
    }

}
