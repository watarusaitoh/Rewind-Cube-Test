using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class GoalController : MonoBehaviour
{
    private GameObject StageClearText;//StageClearテキストを入れる
    public bool Stage2 = false;
    public bool Stage3 = false;
    // Start is called before the first frame update
    void Start()
    {
        this.StageClearText = GameObject.Find("StageClearText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator NextStage()
    {
        //チュートリアル３ステージのテキスト
        if (SceneManager.GetActiveScene().name == "Stage1 Goal")
        {
            Debug.Log("呼び出された");
            this.StageClearText.GetComponent<Text>().text = "GOOD!";
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Stage2 Stairs");
        }
        else if (SceneManager.GetActiveScene().name == "Stage2 Stairs")
        {
            this.StageClearText.GetComponent<Text>().text = "GOOD!!";
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Stage3 Bridge");
        }
        else if (SceneManager.GetActiveScene().name == "Stage3 Bridge")
        {
            this.StageClearText.GetComponent<Text>().text = "GREAT!!!";
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(NextStage());
        Destroy(this.gameObject);
       
    }
}
