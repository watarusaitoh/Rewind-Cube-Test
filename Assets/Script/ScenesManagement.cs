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

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stage1 Goal")
        {
            if (Input.GetKeyDown(KeyCode.R) || this.ResetButton)
            {
                SceneManager.LoadScene("Stage1 Goal");
                this.ResetButton = false;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Stage2 Stairs")
        {
            if (Input.GetKeyDown(KeyCode.R) || this.ResetButton)
            {
                SceneManager.LoadScene("Stage2 Stairs");
                this.ResetButton = false;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage3 Bridge")
        {
            if (Input.GetKeyDown(KeyCode.R) || this.ResetButton)
            {
                SceneManager.LoadScene("Stage3 Bridge");
                this.ResetButton = false;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Stage4 Rotate")
        {
            if (Input.GetKeyDown(KeyCode.R) || this.ResetButton)
            {
                SceneManager.LoadScene("Stage4 Rotate");
                this.ResetButton = false;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Stage5 Swich")
        {
            if (Input.GetKeyDown(KeyCode.R) || this.ResetButton)
            {
                SceneManager.LoadScene("Stage5 Swich");
                this.ResetButton = false;
            }
        }
    }

    public void GetResetButton()
    {
        this.ResetButton = true;
    }

}
