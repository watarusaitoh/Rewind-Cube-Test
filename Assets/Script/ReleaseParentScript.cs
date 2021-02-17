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
    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        Cube2 = GameObject.Find("Cube2");
        StageCore = GameObject.Find("StageCore");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cube1"))
        {
            this.Cube1.transform.parent = null;
        }
        if (other.gameObject.CompareTag("Cube2"))
        {
            this.Cube2.transform.parent = null;
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
        if (other.gameObject.CompareTag("Cube1"))
        {
            this.Cube1.transform.parent = StageCore.gameObject.transform;
        }
        if (other.gameObject.CompareTag("Cube2"))
        {
            this.Cube2.transform.parent = StageCore.gameObject.transform;
        }
    }
}
