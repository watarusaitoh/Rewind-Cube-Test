using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChanger : MonoBehaviour
{
    GameObject Cube1;
    GameObject Cube2;
    private GameObject MyCamera;
    private bool ChangeButton;
   
    // Start is called before the first frame update
    void Start()
    {
        //Cube1を取得する
        Cube1 = GameObject.Find("Cube1");
        //Cube2を取得する
        Cube2 = GameObject.Find("Cube2");
        //Main Cameraを取得する
        MyCamera = GameObject.Find("Main Camera");
      
    }

    // Update is called once per frame
    void Update()
    {
        //Cube1のCubeControllerスクリプトが無効の時
        if (Cube1.GetComponent<CubeController>().enabled == false)
        {
            //Cube2をカメラが追尾する
            this.MyCamera.transform.position = new Vector3(this.Cube2.transform.position.x, this.MyCamera.transform.position.y, this.Cube2.transform.position.z - 12f);
            if (Input.GetKeyDown(KeyCode.Space)||this.ChangeButton)//Spaceキーを押した時
            {
                Cube1.GetComponent<CubeController>().enabled = true;
                Cube2.GetComponent<CubeController>().enabled = false;
                this.ChangeButton = false;
            }
        }
        //Cube1のCubeControllerスクリプトが有効の時
        else if(Cube1.GetComponent<CubeController>().enabled ==true)
        {
            //Cube1をカメラが追尾する
            this.MyCamera.transform.position = new Vector3(this.Cube1.transform.position.x, this.Cube1.transform.position.y, this.Cube1.transform.position.z - 12f);
            if (Input.GetKeyDown(KeyCode.Space)||this.ChangeButton)//Spaceキーを押した時
            {
                Cube1.GetComponent<CubeController>().enabled = false;
                Cube2.GetComponent<CubeController>().enabled = true;
                this.ChangeButton = false;
            }
        }
    }
    public void GetMyChangeButtonDown()
    {
        this.ChangeButton = true;
    }
    
}
