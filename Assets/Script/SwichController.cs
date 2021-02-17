using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class SwichController : MonoBehaviour
{
    public GameObject Gimmick;
    public float Digree;
    //public float RiseY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //スイッチを踏んだ時ゲートが上がる
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "HowToPlay5 Swich")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gimmick.transform.Translate(-Digree, 0, 0, Space.Self);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage5")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gimmick.transform.Translate(0, Digree, 0, Space.Self);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage6")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gimmick.transform.Translate(0, 0, Digree, Space.Self);
            }
        }
    }
    //スイッチから離れたときゲートが下がる。
    private void OnTriggerExit(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "HowToPlay5 Swich")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gimmick.transform.Translate(Digree, 0, 0, Space.Self);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage5")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gimmick.transform.Translate(0, -Digree, 0, Space.Self);
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage6")
        {
            if (other.gameObject.CompareTag("Cube1") || other.gameObject.CompareTag("Cube2"))
            {
                Gimmick.transform.Translate(0, 0, -Digree, Space.Self);
            }
        }
    }
}
