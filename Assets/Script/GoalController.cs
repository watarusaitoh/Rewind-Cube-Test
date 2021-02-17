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
    public bool isGoal;   //ゴールのSetactiveを判断する変数
    public string NextStageName;

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
      
    }
    //ステージの遷移
    IEnumerator Nextstage()
    {
        this.StageClearText.GetComponent<Text>().text = "GOOD";
        script.isFadeOut = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(NextStageName);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Nextstage());
        this.GetComponent<Renderer>().enabled = false;
        isGoal = true;
    }
}
