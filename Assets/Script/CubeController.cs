using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    Vector3 rotatePoint = Vector3.zero;  //回転の中心
    Vector3 rotateAxis = Vector3.zero;   //回転軸
    float cubeAngle = 0f;                //回転角度

    float cubeSizeHalf;                  //キューブの大きさの半分
    bool isRotate = false;               //回転中に立つフラグ、回転中は入力を受け付けない
    Vector3 m_targetPosition = Vector3.zero;//キューブの微量な誤差を修正するための変数
    private GameObject L_Gate;           //LeftGateを入れる
    private float RiseDegree = 4f;
    public bool RButton;          //RightButtonを入れる
    public bool LButton;          //LeftButtonを入れる
    public bool FButton;          //ForwardButtonを入れる
    public bool BButton;          //BuckButtonを入れる
    private bool StepUpDownMove;
    private float sumRotate;

    void Start()
    {
        cubeSizeHalf = transform.localScale.x / 2f;
        L_Gate = GameObject.Find("LeftGate");//L_Gateを取得する
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow)||this.RButton)//RightArrowまたはRButtonを押した時
        {
            RaycastHit Result;
            Physics.Raycast(this.transform.position, Vector3.right, out Result, this.cubeSizeHalf*2f);
            if (Result.collider != null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(2f, 2f, 0f);
                rotatePoint = transform.position + new Vector3(cubeSizeHalf, cubeSizeHalf, 0f);
                rotateAxis = new Vector3(0, 0, -1);
                return;
            }
            Physics.Raycast(this.transform.position + new Vector3(2f, 0f, 0f), new Vector3(0f, -2f, 0f), out Result, this.cubeSizeHalf * 2f);
            if (Result.collider == null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(2f, -2f, 0f);
                rotatePoint = transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
                rotateAxis = new Vector3(0, 0, -1);
                return;
            }
            m_targetPosition = this.transform.position + Vector3.right * 2f;   //回転時の微量の誤差を修正
            rotatePoint = transform.position + new Vector3(cubeSizeHalf, -cubeSizeHalf, 0f);
            rotateAxis = new Vector3(0, 0, -1);


        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)||this.LButton)//LeftArrowまたはLButtonを押した時
        {
            RaycastHit Result;
            Physics.Raycast(this.transform.position, Vector3.left, out Result, this.cubeSizeHalf*2f);
            if (Result.collider != null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(-2f, 2f,0f);
                rotatePoint = transform.position + new Vector3(-cubeSizeHalf, cubeSizeHalf, 0f);
                rotateAxis = new Vector3(0, 0, 1);
                return;
            }
            Physics.Raycast(this.transform.position + new Vector3(-2f, 0f, 0f), new Vector3(0f, -2f,02f), out Result, this.cubeSizeHalf * 2f);
            if (Result.collider == null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(-2f, -2f, 0f);
                rotatePoint = transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
                rotateAxis = new Vector3(0, 0, 1);
                return;
            }
            m_targetPosition = this.transform.position + Vector3.left * 2f;
            rotatePoint = transform.position + new Vector3(-cubeSizeHalf, -cubeSizeHalf, 0f);
            rotateAxis = new Vector3(0, 0, 1);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)||this.FButton)//UpArrowまたはFButtonを押した時
        {
            //Cubeが一段上る場合
            RaycastHit Result;
            Physics.Raycast(this.transform.position, Vector3.forward, out Result, this.cubeSizeHalf*2f);
            if(Result.collider != null)
            {
                if (Physics.Raycast(this.transform.position + new Vector3(0f, 2f, 2f), new Vector3(0f, 1f, 0f), out Result ,this.cubeSizeHalf))
                {
                    StepUpDownMove = false;
                    Debug.Log("yobidasareta");
                    Debug.DrawRay(this.transform.position + new Vector3(0f, 2f, 2f), new Vector3(0f, 1f, 0f), Color.red,100f);
                    m_targetPosition = this.transform.position + new Vector3(0f, 2f, 0f);
                }
                else
                {
                    StepUpDownMove = true;
                    m_targetPosition = this.transform.position + new Vector3(0f, 2f, 2f);
                }
               
                rotatePoint = transform.position + new Vector3(0f, cubeSizeHalf, cubeSizeHalf);
                rotateAxis = new Vector3(1, 0, 0);
                return;
            }
            //Cubeが一段降りる場合
            Physics.Raycast(this.transform.position+new Vector3(0f,0f,2f), new Vector3(0f,-2f,0f),out Result, this.cubeSizeHalf*2f);
            if(Result.collider == null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(0f, -2f, 2f);
                rotatePoint = transform.position + new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
                rotateAxis = new Vector3(1, 0, 0);
                return;
            }
            
            m_targetPosition = this.transform.position + Vector3.forward * 2f;
            rotatePoint = transform.position + new Vector3(0f, -cubeSizeHalf, cubeSizeHalf);
            rotateAxis = new Vector3(1, 0, 0);


        }
        if (Input.GetKeyDown(KeyCode.DownArrow)||this.BButton)//DownArrowまたはBButtonを押した時
        {
            RaycastHit Result;
            Physics.Raycast(this.transform.position, Vector3.back, out Result, this.cubeSizeHalf*2f);
            if (Result.collider != null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(0f, 2f, -2f);
                rotatePoint = transform.position + new Vector3(0f, cubeSizeHalf, -cubeSizeHalf);
                rotateAxis = new Vector3(-1, 0, 0);

                return;
            }
            Physics.Raycast(this.transform.position + new Vector3(0f, 0f, -2f), new Vector3(0f, -2f, 0f), out Result, this.cubeSizeHalf * 2f);
            if (Result.collider == null)
            {
                StepUpDownMove = true;
                m_targetPosition = this.transform.position + new Vector3(0f, -2f, -2f);
                rotatePoint = transform.position + new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
                rotateAxis = new Vector3(-1, 0, 0);
                return;
            }
            m_targetPosition = this.transform.position + Vector3.back * 2f;
            rotatePoint = transform.position + new Vector3(0f, -cubeSizeHalf, -cubeSizeHalf);
            rotateAxis = new Vector3(-1, 0, 0);


        }
        if (rotatePoint == Vector3.zero)
            return;
        StartCoroutine(MoveCube());


    }
    IEnumerator MoveCube()
    {
        //回転中のフラグを立てる
        isRotate = true;
        if (StepUpDownMove == true)
        {
            sumRotate = 180f;
        }
        else 
        {
            sumRotate = 90f;
        }
        
        //回転処理
        float sumAngle = 0f;  //angleの合計を保存
        while (sumAngle <= this.sumRotate)
        {
            cubeAngle = 5f;  //ここを変えると回転速度が変わる
            sumAngle += cubeAngle;


            //sumRotateの角度以上回転しないように値を制限
            if (sumAngle >this.sumRotate)
            {
                cubeAngle -= sumAngle - this.sumRotate;
            }
            transform.RotateAround(rotatePoint, rotateAxis, cubeAngle);
            yield return null;
        }

        //回転中のフラグを倒す
        isRotate = false;
        rotatePoint = Vector3.zero;
        rotateAxis = Vector3.zero;
        this.transform.position = m_targetPosition;
        this.StepUpDownMove = false;
        yield break;
    }
//スイッチを踏んだ時ゲートが上がる
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftGateSwich"))
        {
            L_Gate.transform.position = new Vector3(this.L_Gate.transform.position.x, this.L_Gate.transform.position.y + this.RiseDegree, this.L_Gate.transform.position.z);
        }
    }
//スイッチをから離れたときゲートが下がる。
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LeftGateSwich"))
        {
            L_Gate.transform.position = new Vector3(this.L_Gate.transform.position.x, this.L_Gate.transform.position.y - this.RiseDegree, this.L_Gate.transform.position.z);
        }
    }

    public void GetRButtonDown()
    {
        this.RButton = true;
    }
    public void GetRButtonUp()
    {
        this.RButton = false;
    }
    public void GetLButtonDown()
    {
        this.LButton = true;
    }
    public void GetLButtonUp()
    {
        this.LButton = false;
    }
    public void GetFButtonDown()
    {
        this.FButton = true;
    }
    public void GetFButtonUp()
    {
        this.FButton = false;
    }
    public void GetBButtonDown()
    {
        this.BButton = true;
    }
    public void GetBButtonUp()
    {
        this.BButton = false;
    }
}
