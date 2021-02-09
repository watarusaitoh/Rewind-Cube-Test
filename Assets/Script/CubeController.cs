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
    private bool RButton;          //RightButtonを入れる
    private bool LButton;          //LeftButtonを入れる
    private bool FButton;          //ForwardButtonを入れる
    private bool BButton;          //BuckButtonを入れる
    private bool StepUpDownMove;  //一段上がるか下がるかの判断をする変数
    private float sumRotate;　　　//転がる角度の合計
    public bool RotateEnd;        //Cubeの回転終わりを判断する。　StageControllerに渡す変数
    public Vector3 nomal ;　　　　//法線を代入する変数

    void Start()
    {
        cubeSizeHalf = transform.localScale.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
            return;
        //上面にある場合のCubeの法線
        nomal = new Vector3(0, 1, 0);
        RaycastHit Result;
        //右矢印またはRButtonを押した時
        if (Input.GetKeyDown(KeyCode.RightArrow)||this.RButton)
        {
            //一段上る場合
            Physics.Raycast(this.transform.position, Vector3.right, out Result, this.cubeSizeHalf*2f);
            if (Result.collider != null)
            {
                //2段以上ある場合
                if (Physics.Raycast(this.transform.position + new Vector3(2f, 0f, 0f), new Vector3(0f, 2f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    //登れない
                    return;
                }
                //両側には挟まれた場合
                if(Physics.Raycast(this.transform.position + new Vector3(0f, 0f, 0f), new Vector3(-2f, 0f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    return;
                }
                //上を基準にし180度回転する
                Rotate180(cubeSizeHalf, cubeSizeHalf, cubeSizeHalf);
                return;
            }
            //一段降りる場合
            Physics.Raycast(this.transform.position + new Vector3(2f, 0f, 0f), new Vector3(0f, -2f, 0f), out Result, this.cubeSizeHalf * 2f);
            if (Result.collider == null)
            {
                if (Physics.Raycast(this.transform.position + new Vector3(2f, -2f, 0f), new Vector3(-2f, 0f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    //下を基準にし180度回転する
                    Rotate180(cubeSizeHalf, -cubeSizeHalf, cubeSizeHalf);
                    return;
                }
                //下に何もない場合動けない
                return;
            }
            //下を基準にし90度回転する
            Rotate90(cubeSizeHalf,-cubeSizeHalf,cubeSizeHalf);
        }
        //左矢印またはLButtonを押した時
        if (Input.GetKeyDown(KeyCode.LeftArrow)||this.LButton)
        {
            //一段上る場合
            
            Physics.Raycast(this.transform.position, Vector3.left, out Result, this.cubeSizeHalf*2f);
            if (Result.collider != null)
            {
                //2段以上ある場合
                if (Physics.Raycast(this.transform.position + new Vector3(-2f, 0f, 0f), new Vector3(0f, 2f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    //登れない
                    return;
                }
                //両側には挟まれた場合
                if (Physics.Raycast(this.transform.position + new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    return;
                }
                //上を基準にし180度回転する
                Rotate180(-cubeSizeHalf, cubeSizeHalf, cubeSizeHalf);
                return;
            }
            //一段降りる場合
            Physics.Raycast(this.transform.position + new Vector3(-2f, 0f, 0f), new Vector3(0f, -2f,0f), out Result, this.cubeSizeHalf * 2f);
            if (Result.collider == null)
            {
                if (Physics.Raycast(this.transform.position + new Vector3(-2f, -2f, 0f), new Vector3(2f, 0f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    //下を基準にし180度回転する
                    Rotate180(-cubeSizeHalf, -cubeSizeHalf, cubeSizeHalf);
                    return;
                }
                //下に何もない場合動けない
                return;
            }
            //下を基準にし90度回転する
            Rotate90(-cubeSizeHalf,-cubeSizeHalf,cubeSizeHalf);

        }
        //上矢印またはFButtonを押した時
        if (Input.GetKeyDown(KeyCode.UpArrow)||this.FButton)
        {
            //一段上る場合
          
            Physics.Raycast(this.transform.position, Vector3.forward, out Result, this.cubeSizeHalf*2f);
            if(Result.collider != null)
            {
                //2段以上ある場合
                if (Physics.Raycast(this.transform.position + new Vector3(0f, 0f, 2f), new Vector3(0f, 2f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    //登れない
                    return;
                }
                //両側には挟まれた場合
                if (Physics.Raycast(this.transform.position + new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -2f), out Result, this.cubeSizeHalf * 2f))
                {
                    return;
                }
                //上を基準にし180度回転する
                Rotate180(cubeSizeHalf, cubeSizeHalf, cubeSizeHalf);
                return;
            }
            //一段降りる場合
            Physics.Raycast(this.transform.position+new Vector3(0f,0f,2f), new Vector3(0f,-2f,0f),out Result, this.cubeSizeHalf*2f);
            if(Result.collider == null)
            {
                if (Physics.Raycast(this.transform.position + new Vector3(0f, -2f, 2f), new Vector3(0f, 0f, -2f), out Result, this.cubeSizeHalf * 2f))
                {
                    //下を基準にし180度回転する
                    Rotate180(cubeSizeHalf, -cubeSizeHalf, cubeSizeHalf);
                    return;
                }
                //下に何もない場合動けない
                return;
            }
            //下を基準にし90度回転する
            Rotate90(cubeSizeHalf,-cubeSizeHalf,cubeSizeHalf);


        }
        //下矢印またはBButtonを押した時
        if (Input.GetKeyDown(KeyCode.DownArrow)||this.BButton)
        {
            //一段上る場合 
            Physics.Raycast(this.transform.position, Vector3.back, out Result, this.cubeSizeHalf*2f);
            if (Result.collider != null)
            {
                //2段以上ある場合
                if (Physics.Raycast(this.transform.position + new Vector3(0f, 0f, -2f), new Vector3(0f, 2f, 0f), out Result, this.cubeSizeHalf * 2f))
                {
                    //登れない
                    return;
                }
                //両側には挟まれた場合
                if (Physics.Raycast(this.transform.position + new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 2f), out Result, this.cubeSizeHalf * 2f))
                {
                    return;
                }
                //上を基準にし180度回転する
                Rotate180(cubeSizeHalf, cubeSizeHalf, -cubeSizeHalf);
                return;
            }
            //一段降りる場合
            Physics.Raycast(this.transform.position + new Vector3(0f, 0f, -2f), new Vector3(0f, -2f, 0f), out Result, this.cubeSizeHalf * 2f);
            if (Result.collider == null)
            {
                if (Physics.Raycast(this.transform.position + new Vector3(0f, -2f, -2f), new Vector3(0f, 0f, 2f), out Result, this.cubeSizeHalf * 2f))
                {
                    //下を基準にし180度回転する
                    Rotate180(cubeSizeHalf, -cubeSizeHalf, -cubeSizeHalf);
                    return;
                }
                //下に何もない場合動けない
                return;
            }
            //下を基準にし90度回転する
            Rotate90(cubeSizeHalf,-cubeSizeHalf,-cubeSizeHalf);


        }
        if (rotatePoint == Vector3.zero)
            return;
        StartCoroutine(MoveCube());


    }
    IEnumerator MoveCube()
    {
        RotateEnd = false;
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
        RotateEnd = true;
        yield break;
    }
    //90度回転する場合の処理
    void Rotate90(float PointNumX,float PointNumY,float PointNumZ)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)||this.RButton == true)
        {
            m_targetPosition = this.transform.position + new Vector3 (cubeSizeHalf * 2f, 0f, 0f);
            rotatePoint = transform.position + new Vector3(PointNumX, PointNumY, 0f);
            rotateAxis = new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)||this.LButton == true)
        {
            m_targetPosition = this.transform.position +new Vector3(-cubeSizeHalf* 2f, 0f, 0f);
            rotatePoint = transform.position + new Vector3(PointNumX, PointNumY, 0f);
            rotateAxis = new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)||this.FButton == true)
        {
            m_targetPosition = this.transform.position + new Vector3(0f, 0f, cubeSizeHalf * 2f);
            rotatePoint = transform.position + new Vector3(0f, PointNumY, PointNumZ);
            rotateAxis = new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)||this.BButton == true)
        {
            m_targetPosition = this.transform.position + new Vector3(0f, 0f, -cubeSizeHalf * 2f);
            rotatePoint = transform.position + new Vector3(0f, PointNumY, PointNumZ);
            rotateAxis = new Vector3(-1, 0, 0);
        }

    }
    //180度回転する場合の処理
    void Rotate180(float PointNumX,float PointNumY,float PointNumZ)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)||this.RButton == true)
        {
            StepUpDownMove = true;
            m_targetPosition = this.transform.position + new Vector3(PointNumX * 2f, PointNumY * 2f, 0f);
            rotatePoint = transform.position + new Vector3(PointNumX, PointNumY, 0f);
            rotateAxis = new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)||this.LButton == true)
        {
            StepUpDownMove = true;
            m_targetPosition = this.transform.position + new Vector3(PointNumX*2f, PointNumY * 2f, 0f);
            rotatePoint = transform.position + new Vector3(PointNumX, PointNumY, 0f);
            rotateAxis = new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)||this.FButton == true)
        {
            StepUpDownMove = true;
            m_targetPosition = this.transform.position + new Vector3(0f, PointNumY * 2f, PointNumZ*2f);
            rotatePoint = transform.position + new Vector3(0f, PointNumY, PointNumZ);
            rotateAxis = new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)||this.BButton == true)
        {
            StepUpDownMove = true;
            m_targetPosition = this.transform.position + new Vector3(0f, PointNumY * 2f,PointNumZ*2f);
            rotatePoint = transform.position + new Vector3(0f, PointNumY, PointNumZ);
            rotateAxis = new Vector3(-1, 0, 0);
        }
    }
    //UIのボタンを押した時Cubeが進む
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
    //法線を判断するオブジェクトに当たった場合
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("JudgeRight"))
        {
            nomal = new Vector3(1, 0, 0);
        }
        if (other.gameObject.CompareTag("JudgeLeft"))
        {
            nomal = new Vector3(-1, 0, 0);
        }
        if (other.gameObject.CompareTag("JudgeForward"))
        {
            nomal = new Vector3(0, 0, 1);
        }
        if (other.gameObject.CompareTag("JudgeBack"))
        {
            nomal = new Vector3(0, 0, -1);
        }
        if (other.gameObject.CompareTag("JudgeBack"))
        {
            nomal = new Vector3(0, -1, 0);
        }
    }

}
