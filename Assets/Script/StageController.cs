using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    GameObject RC;
    Vector3 rotatePoint = Vector3.zero;  //回転の中心
    Vector3 rotateAxis = Vector3.zero;   //回転軸
    float cubeAngle = 0f;                //回転角度
    float cubeSizeHalf;                  //キューブの大きさの半分
    bool isRotate = false;               //回転中に立つフラグ、回転中は入力を受け付けない
    Vector3 m_targetPosition = Vector3.zero;//キューブの微量な誤差を修正するための変数
    private float sumRotate;
    RaycastController script;
    GameObject Cube1;
    bool RotateStop;
    
    // Start is called before the first frame update
    void Start()
    {
        RC = GameObject.Find("RaycastObject");
        script = this.RC.GetComponent<RaycastController>();
        Cube1 = GameObject.Find("Cube1");
       
    }
   
    // Update is called once per frame
    void Update()
    {
       
        if (isRotate)
            return;
        bool RightRotate = this.script.StageRightRotate ;
        if (RightRotate)
        {
            rotateAxis = new Vector3(0f, 0f, 1f);
        }
        
        if(RightRotate == false)
            return;
        RightRotate = false;
        Debug.Log("iii");
        StartCoroutine(MoveCube());
        
       
    }
    IEnumerator MoveCube()
    {
        //回転中のフラグを立てる
        isRotate = true;
        sumRotate = 90f;
        rotatePoint = Vector3.zero;

        //回転処理
        float sumAngle = 0f;  //angleの合計を保存
        while (sumAngle <= this.sumRotate)
        {
            cubeAngle = 1f;  //ここを変えると回転速度が変わる
            sumAngle += cubeAngle;


            //sumRotateの角度以上回転しないように値を制限
            if (sumAngle > this.sumRotate)
            {

                //cubeAngle -= sumAngle - this.sumRotate;
                cubeAngle = 0F;
            }
            this.transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);
            yield return null;
        }
        //回転中のフラグを倒す
        Debug.Log("aaa");
        isRotate = false;
        rotatePoint = Vector3.zero;
        rotateAxis = Vector3.zero;
        this.Cube1.transform.parent = null;
        yield break;
    }
}
