using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSwicher : MonoBehaviour
{
    public Vector3 moveAmount;          //オブジェクトの移動量
    private bool isEnable;　　　　　　　
    private Vector3 firstPosition;
    private Vector3 movedPosition;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        this.firstPosition = this.target.position;
        this.movedPosition = this.firstPosition + this.moveAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnable)
        {
            Vector3 direction = (this.movedPosition - this.target.position).normalized;
            this.target.position += direction*0.1f;
            if (Vector3.Distance(this.target.position, this.movedPosition) < 0.1f)
            {
                this.enabled = false;
            }
        }
        else
        {
            Vector3 direction = (this.firstPosition - this.target.position).normalized;
            this.target.position += direction*0.1f;
            if (Vector3.Distance(this.target.position, this.firstPosition) < 0.1f)
            {
                this.enabled = false;
            }
        }
    }
    public void SetSwich(bool isEnable)
    {
        this.isEnable = isEnable;
        this.enabled = true;
    }
}
