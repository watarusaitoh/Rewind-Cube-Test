using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
    private float Speed;                   //落ちるスピードを示す変数
    private float cubeSizehalf;　　　      //Cubeの大きさの半分
    CubeController Cubecontrollerscript;   //CubeControllerのスクリプトを入れる　　　 
    private bool isFallStop;               //落下処理可能にする変数
    LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        //CubeControllerを取得
        Cubecontrollerscript = this.GetComponent<CubeController>();
        this.cubeSizehalf = this.transform.localScale.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRotate = this.Cubecontrollerscript.isRotate;
        if (isRotate)
            return;
        FallMove();
    }
    void FallMove()
    {
        if (gameObject.CompareTag("Cube1"))
        {
            layerMask = LayerMask.GetMask(new string[] { "Cube2" });
        }
        if (gameObject.CompareTag("Cube2"))
        {
            layerMask = LayerMask.GetMask(new string[] { "Cube1" });
        }
        RaycastHit hitLeft;
        RaycastHit hitRight;
        RaycastHit hitForward;
        RaycastHit hitBack;
        RaycastHit hitDown;
        RaycastHit hitUp;
        Physics.Raycast(this.transform.position, Vector3.left, out hitLeft, this.cubeSizehalf*2 );
        Physics.Raycast(this.transform.position, Vector3.right, out hitRight, this.cubeSizehalf*2);
        Physics.Raycast(this.transform.position, Vector3.forward, out hitForward, this.cubeSizehalf*2);
        Physics.Raycast(this.transform.position, Vector3.back, out hitBack, this.cubeSizehalf*2);
        Physics.Raycast(this.transform.position, Vector3.up, out hitUp, this.cubeSizehalf * 2,~layerMask);
        Physics.Raycast(this.transform.position, Vector3.down, out hitDown,this.cubeSizehalf+0.1f);
        //前後左右に物がなかった場合落下のフラグを立てる
        if (hitLeft.collider == null && hitRight.collider == null && hitForward.collider == null && hitBack.collider == null && hitUp.collider == null && hitDown.collider == null)
        { 
            //落下速度
            this.Speed += 0.1f;
            //落下処理
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - this.Speed * Time.deltaTime, this.transform.position.z);
            isFallStop = true;
        }
        //落下停止処理
        if (isFallStop)
        {
            //着地位置の調節
            if (hitDown.collider != null)
            {
                this.transform.position = new Vector3(Mathf.RoundToInt(hitDown.point.x), Mathf.RoundToInt(hitDown.point.y+this.cubeSizehalf), Mathf.RoundToInt(hitDown.point.z));
                //落下フラグを倒す
                isFallStop = false;
                GetComponent<AudioSource>().Play();
            }
            //側面に物があった時側面との距離を調節する
            if (hitLeft.collider != null||hitRight.collider != null||hitForward.collider != null||hitBack.collider != null)
            {
                if (hitLeft.distance > 0f)
                {   //落下フラグを倒す
                    isFallStop = false;
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.RoundToInt(hitLeft.point.y - this.cubeSizehalf), this.transform.position.z);
                }
                if (hitRight.distance > 0f)
                {
                    isFallStop = false;
                    this.transform.position = new Vector3(Mathf.RoundToInt(hitRight.point.x - this.cubeSizehalf), Mathf.RoundToInt(hitRight.point.y - this.cubeSizehalf), hitRight.point.z);
                }
                if (hitForward.distance > 0f)
                {
                    isFallStop = false;
                    this.transform.position = new Vector3(hitForward.point.x, Mathf.RoundToInt(hitForward.point.y - this.cubeSizehalf), Mathf.RoundToInt(hitForward.point.z-this.cubeSizehalf));
                }
                if (hitBack.distance > 0f)
                {
                    isFallStop = false;
                    this.transform.position = new Vector3(hitBack.point.x, Mathf.RoundToInt(hitBack.point.y - this.cubeSizehalf), Mathf.RoundToInt(hitBack.point.z + this.cubeSizehalf));
                }
                GetComponent<AudioSource>().Play();
            }
        }
       
    }
}
