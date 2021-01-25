using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    private GameObject StageClearText;//StageClearテキストを入れる
    
    // Start is called before the first frame update
    void Start()
    {
        this.StageClearText = GameObject.Find("StageClearText");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
            this.StageClearText.GetComponent<Text>().text = "Stage1Clear!";
    }
}
