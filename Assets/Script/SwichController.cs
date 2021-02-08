using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class SwichController : MonoBehaviour
{
    GameObject Gate;

    // Start is called before the first frame update
    void Start()
    {
        Gate = GameObject.Find("Gate");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //スイッチを踏んだ時ゲートが上がる
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Stage5 Swich")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gate.transform.Translate(-8, 0, 0, Space.Self);
            }
        }
    }
    //スイッチをから離れたときゲートが下がる。
    private void OnTriggerExit(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Stage5 Swich")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gate.transform.Translate(8, 0, 0, Space.Self);
            }
        }
    }
}
