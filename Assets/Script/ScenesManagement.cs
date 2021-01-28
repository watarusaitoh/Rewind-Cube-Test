using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンを切り替えるために追加

public class ScenesManagement : MonoBehaviour
{
    GameObject GoalPoint;
    GoalController StageChange;
    private bool ResetButton = false;

    // Start is called before the first frame update
    void Start()
    {
        GoalPoint = GameObject.Find("GoalPoint");
        StageChange = GoalPoint.GetComponent<GoalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)||this.ResetButton)
        {
            SceneManager.LoadScene("Stage1 Goal");
            this.ResetButton = false;
        }

        bool StageChanger = StageChange.Stage2;
        if (StageChanger)
        {
            SceneManager.LoadScene("Stage2 Stairs");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("Stage3 Bridge");
        }
    }
    public void GetResetButton()
    {
        this.ResetButton = true;
    }
}
