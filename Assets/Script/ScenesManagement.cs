using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class ScenesManagement : MonoBehaviour
{
    GameObject GoalPoint;
    GoalController StageChange;
    private bool ResetButton = false;
    public string StageName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == StageName)
        {
            if (Input.GetKeyDown(KeyCode.R) || this.ResetButton)
            {
                SceneManager.LoadScene(StageName);
                this.ResetButton = false;
            }
        }
    }

    public void GetResetButton()
    {
        this.ResetButton = true;
    }

}
