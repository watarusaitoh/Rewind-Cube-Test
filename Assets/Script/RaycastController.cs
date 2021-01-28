using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    GameObject RS_R;
    GameObject RE_R;
    float distance;
    public bool StageRightRotate = false;
    GameObject Cube1;
    GameObject SC;
    // Start is called before the first frame update
    void Start()
    {
        RS_R = GameObject.Find("RayStart(Right)");
        RE_R = GameObject.Find("RayEnd(Right)");
        Cube1 = GameObject.Find("Cube1");
        SC = GameObject.Find("StageCore");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RayRight();
            
        }
        

    }
    void RayRight()
    {
        distance = this.RS_R.transform.position.z - this.RE_R.transform.position.z;
        RaycastHit hit;
        Physics.Raycast(this.RS_R.transform.position, new Vector3(0f, 0f, -distance), out hit, distance);
        if (hit.collider != null)
        {
            this.StageRightRotate = true;
            this.Cube1.transform.parent = this.SC.transform;
            Debug.DrawRay(this.RS_R.transform.position, new Vector3(0f, 0f, -distance), Color.red, 100f);
            this.StageRightRotate = false;
        }
    }
}
