﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeChanger : MonoBehaviour
{
    GameObject Cube1;
    GameObject Cube2;
    private bool ChangeButton;
    private GameObject StageCore;
    StageController script;
    public bool change1;          //StageControllerに参照する変数
    public bool change2;          //StageControllerに参照する変数

    // Start is called before the first frame update
    void Start()
    {
        //Cube1を取得する
        Cube1 = GameObject.Find("Cube1");
        //Cube2を取得する
        Cube2 = GameObject.Find("Cube2");
        StageCore = GameObject.Find("StageCore");
        script = StageCore.GetComponent<StageController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spaceを離した時ステージが回転する軸が取得されなくなる
        if (Input.GetKeyUp(KeyCode.Space) || this.ChangeButton)
        {
            change1 = false;
            change2 = false;
        }
        bool isRotate = script.isRotate;
        //Stageが回転しているときは交代ができない
        if (isRotate)
            return;
        //Cube1のCubeControllerスクリプトが無効の時
        if (Cube1.GetComponent<CubeController>().enabled == false)
        {
            //Cube2を有効にする
            if (Input.GetKeyDown(KeyCode.Space)||this.ChangeButton)
            {
                change2 = true;
                Cube1.GetComponent<CubeController>().enabled = true;
                Cube2.GetComponent<CubeController>().enabled = false;
                this.ChangeButton = false;
            }
        }
        //Cube1のCubeControllerスクリプトが有効の時
        else if(Cube1.GetComponent<CubeController>().enabled ==true)
        {
            //Cube1を有効にする
            if (Input.GetKeyDown(KeyCode.Space)||this.ChangeButton)
            {
                change1 = true;
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
