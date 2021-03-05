using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class ReleaseParentScript : MonoBehaviour
{
    GameObject Cube1;
    GameObject Cube2;
    GameObject StageCore;
    public GameObject GimmickGroup;
    StageController stagecontroller;
    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        Cube2 = GameObject.Find("Cube2");
        StageCore = GameObject.Find("StageCore");
        stagecontroller = StageCore.GetComponent<StageController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        bool isRotate = stagecontroller.isRotate;
        if (isRotate)
            return;
        if (other.gameObject.CompareTag("Cube1"))
        {
            this.Cube1.transform.parent = null;
            //接触している間はistriggerのチェックがつく
            this.GetComponent<BoxCollider>().isTrigger = true;
        }
        if (other.gameObject.CompareTag("Cube2"))
        {
            this.Cube2.transform.parent = null;//回転している間だけは外れなければいい
            this.GetComponent<BoxCollider>().isTrigger = true;
        }
        if(SceneManager.GetActiveScene().name == "Stage5"|| SceneManager.GetActiveScene().name == "Stage6")
        {
            if (other.gameObject.CompareTag("Cube1"))
            {
                this.Cube1.transform.parent = this.GimmickGroup.gameObject.transform;
            }
            if (other.gameObject.CompareTag("Cube2"))
            {
                this.Cube2.transform.parent = this.GimmickGroup.gameObject.transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bool isRotate = stagecontroller.isRotate;
        if (isRotate)
            return;
        if (other.gameObject.CompareTag("Cube1"))
        {
            this.Cube1.transform.parent = StageCore.gameObject.transform;
            //離れたときistriggerのチェックが外れる
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (other.gameObject.CompareTag("Cube2"))
        {
            this.Cube2.transform.parent = StageCore.gameObject.transform;
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
