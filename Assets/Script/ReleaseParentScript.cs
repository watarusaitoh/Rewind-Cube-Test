using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseParentScript : MonoBehaviour
{
    GameObject Cube1;
    GameObject Cube2;
    GameObject StageCore;
    // Start is called before the first frame update
    void Start()
    {
        Cube1 = GameObject.Find("Cube1");
        Cube2 = GameObject.Find("Cube2");
        StageCore = GameObject.Find("StageCore");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube1"))
        {
            this.Cube1.transform.parent = null;
        }
        if (other.gameObject.CompareTag("Cube2"))
        {
            this.Cube2.transform.parent = null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cube1"))
        {
            this.Cube1.transform.parent = StageCore.gameObject.transform;
        }
        if (other.gameObject.CompareTag("Cube2"))
        {
            this.Cube2.transform.parent = StageCore.gameObject.transform;
        }
    }
}
