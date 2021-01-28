using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChanger : MonoBehaviour
{
    GameObject Cube1;
    GameObject Cube2;
    private bool ChangeButton;
   
    // Start is called before the first frame update
    void Start()
    {
        //Cube1を取得する
        Cube1 = GameObject.Find("Cube1");
        //Cube2を取得する
        Cube2 = GameObject.Find("Cube2");
    }

    // Update is called once per frame
    void Update()
    {
        //Cube1のCubeControllerスクリプトが無効の時
        if (Cube1.GetComponent<CubeController>().enabled == false)
        {
            if (Input.GetKeyDown(KeyCode.Space)||this.ChangeButton)
            {
                Cube1.GetComponent<CubeController>().enabled = true;
                Cube2.GetComponent<CubeController>().enabled = false;
                this.ChangeButton = false;
            }
        }
        //Cube1のCubeControllerスクリプトが有効の時
        else if(Cube1.GetComponent<CubeController>().enabled ==true)
        {
            if (Input.GetKeyDown(KeyCode.Space)||this.ChangeButton)
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
