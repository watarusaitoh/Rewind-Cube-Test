using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomMove : MonoBehaviour
{
    private Camera cam;               //MainCameraのCameraコンポーネントを取得
    private GameObject Cube1;
    private GameObject Cube2;
    private float beforeZoom = 60f;   //ズームする前の位置を保存
    private bool isDoubleTapStart;    //最初のタップが押されたときtrueになる変数
    private bool isDoubleTapMove;     //ダブルタップされたときtrueになる変数
    private float doubleTapTime;      //ダブルタップを受け付ける時間の変数
    private Vector2 StartPos;         //タップし始めた場所
    private Vector2 EndPos;           //タップし動いた後の場所
    private float SwipeLenth_Y;       //StartPosとEndPosの距離
    private float Y_Speed;            //SwipeLenth_Yを正常な距離に直した変数
    //デフォルトサイズ
    Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Cube1 = GameObject.Find("Cube1");
        Cube2 = GameObject.Find("Cube2");
    }

    // Update is called once per frame
    void Update()
    {
        Smartphonezoom();
        PCzoom();
        doubletap();
    }
    //カメラのズームインズームアウトの関数
    private void doubletap()
    {
        //ダブルタップした時に上下でズームできるようにする
        if(isDoubleTapStart)
        {
            doubleTapTime += Time.deltaTime;
            if (doubleTapTime < 0.2f)
            {
                //ダブルタップされた時
                if (Input.GetMouseButtonDown(0))
                {
                    isDoubleTapMove = true;
                    //Smartphonezoomで使用する変数
                    this.StartPos = Input.mousePosition;
                    isDoubleTapStart = false;
                    doubleTapTime = 0.0f;
                }
            }
            //シングルタップの処理
            else
            {
                isDoubleTapStart = false;
                doubleTapTime = 0.0f;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDoubleTapStart = true;
            }
        }
    }
    void Smartphonezoom()
    {
        if (isDoubleTapMove)
        {
            if (Input.GetMouseButton(0))
            {
                this.EndPos = Input.mousePosition;
                this.SwipeLenth_Y = this.EndPos.y - this.StartPos.y;

                this.Y_Speed = SwipeLenth_Y / 250.0f;
                cam.fieldOfView -= Y_Speed;
                beforeZoom = cam.fieldOfView;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDoubleTapMove = false;
            }
        }
    }
    //EキーQキーが押されたときにズームインアウトされる
    void PCzoom()
    {
        //ズームされる制限を付ける
        if (cam.fieldOfView < 10f)
        {
            cam.fieldOfView = 10f;
        }
        if (cam.fieldOfView > 100f)
        {
            cam.fieldOfView = 100f;
        }
        //Eキーが押されたときズームインする
        if (Input.GetKey(KeyCode.E))
        {
            cam.fieldOfView -= 0.3f;
            beforeZoom = cam.fieldOfView;
        }
        //Qキーが押されたときズームアウトする
        if (Input.GetKey(KeyCode.Q))
        {
            cam.fieldOfView += 0.3f;
            beforeZoom = cam.fieldOfView;
        }
        //ステージの回転時ステージ中心になる
        if(Cube1.GetComponent<CubeController>().enabled ==false&&Cube2.GetComponent<CubeController>().enabled ==false)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, 0.05f);
        }
        if (Cube1.GetComponent<CubeController>().enabled == true||Cube2.GetComponent<CubeController>().enabled == true)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, beforeZoom, 0.05f);
        }
    }

}
