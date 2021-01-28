using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube1"))
        {
            Destroy(this.gameObject);
           this.StageClearText.GetComponent<Text>().text = "CLEAR";
           Stage2 = true;
        }
    }
}
