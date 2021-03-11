using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
    private float Speed;
    private float cubehalf = 1f;
    private Vector3 m_targetposition;
    CubeController script;
    // Start is called before the first frame update
    void Start()
    {
        script = this.GetComponent<CubeController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRotate = this.script.isRotate;
        if (isRotate)
            return;
        RaycastHit hitDown;
        RaycastHit hitLeft;
        RaycastHit hitRight;
        RaycastHit hitForward;
        RaycastHit hitBack;
        //側面に物がないとき
        Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y+1.1f , this.transform.position.z), Vector3.left, out hitLeft, 2);
        Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y+1.1f, this.transform.position.z), Vector3.right, out hitRight, 2);
        Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y+1.1f , this.transform.position.z), Vector3.forward, out hitForward, 2);
        Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y+1.1f , this.transform.position.z), Vector3.back, out hitBack, 2);
        Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y+1.1f, this.transform.position.z), Vector3.left, Color.white,10f);
        Debug.Log(hitLeft.collider);
        Debug.Log(hitRight.collider);
        Debug.Log(hitForward.collider);
        Debug.Log(hitBack.collider);
        //前後左右に物がなかった場合
        if (hitLeft.collider == null&&hitRight.collider == null&&hitForward.collider ==null&&hitBack.collider ==null)
        {
            //床との距離があるとき、床との距離が0になるまで落ちる
            Physics.Raycast(new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z), Vector3.down, out hitDown);
            if(hitDown.distance > this.cubehalf)
            {
                //落下速度
                this.Speed += 0.1f;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - this.Speed * Time.deltaTime, this.transform.position.z);
            }
            //位置の調節
            else if (hitDown.distance <= this.cubehalf)
            {
                this.transform.position = new Vector3(hitDown.point.x,hitDown.point.y+1,hitDown.point.z);
            }
        }
        //側面に物があった時側面との距離を調節する
       /* else if(hitLeft.collider != null)
        {
            if (hitLeft.distance<this.cubehalf)
            {
                this.transform.position = new Vector3(hitLeft.point.x-this.cubehalf,hitLeft.point.y-this.cubehalf,hitLeft.point.z);
                Debug.Log("←point" + hitLeft.point);
                Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y , this.transform.position.z), Vector3.left, Color.red, 10f);
            }
        }*/
    }
}
