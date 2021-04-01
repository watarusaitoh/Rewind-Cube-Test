using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーンを切り替えるために必要

public class StageSelectController : MonoBehaviour
{
    private bool StageSelectButton = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelectButton)
        {
            SceneManager.LoadScene("StageSelect");
            this.StageSelectButton = false;
        }
    }
    public void StageSelect()
    {
        this.StageSelectButton = true;
    }
    public void SetStageScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
