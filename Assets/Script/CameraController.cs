using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetObject;//Cube1を入れる
    public float CameraSpeed = 3.0f;//回転する速度

    // Start is called before the first frame update
    void Start()
    {
       // Cube1 = GameObject.Find("Cube1");//Cube1を取得する
    }

    // Update is called once per frame
    void Update()
    {
        //回転させる角度
        float angle = Input.GetAxis("Horizontal") * this.CameraSpeed;
        //カメラを回転させる
        this.transform.RotateAround(this.targetObject.transform.position,Vector3.up,angle );   
    }
}
