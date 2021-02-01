using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class GoalController : MonoBehaviour
{
    private GameObject StageClearText;//StageClearテキストを入れる
    GameObject FadeInFadeOut;         //FadeInFadeOutのオブジェクトを入れる
    FadeScript script;　　　　　　　　//FadeScriptを入れる
    private float step_time = 0.0f;　 //次のステージへ移行する時間の初期化
    public bool isGoal;   //ゴールのSetactiveを判断する変数

    // Start is called before the first frame update
    void Start()
    {
        this.StageClearText = GameObject.Find("StageClearText");
        FadeInFadeOut = GameObject.Find("FadeInFadeOut");
        script = FadeInFadeOut.GetComponent<FadeScript>();
        isGoal = false;
    }

    // Update is called once per frame
    void Update()
    {
        step_time += Time.deltaTime;
        if (isGoal == true)
        {
            Nextstage();
        }
    }
    //ステージの遷移
    void Nextstage()
    {
        bool FadeIn = script.isFadeIn;
        bool FadeOut = script.isFadeOut;
        //ステージ１からステージ２
        if (SceneManager.GetActiveScene().name == "Stage1 Goal")
        {
            this.StageClearText.GetComponent<Text>().text = "GOOD!";
            if (step_time >= 10.0f)
            {
                SceneManager.LoadScene("Stage2 Stairs");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage2 Stairs")
        {
            this.StageClearText.GetComponent<Text>().text = "GOOD!!";
            if (step_time >= 10.0f)
            {
                SceneManager.LoadScene("Stage3 Bridge");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage3 Bridge")
        {
            this.StageClearText.GetComponent<Text>().text = "GOOD!!!";
            if (step_time >= 10.0f)
            {
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Renderer>().enabled = false;
        isGoal = true;
    }
}
