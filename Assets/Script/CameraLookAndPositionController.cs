using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraLookAndPositionController : MonoBehaviour
{
    public Transform lookTarget;
    public float radius = 30;
    float angleX = 0;
    float angleY = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(30, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && angleX < 89)
        {
            angleX += 1f;
        }
        if (Input.GetKey(KeyCode.S) && angleX > -89)
        {
            angleX -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            angleY += -1f;
            if (angleY < 0)
                angleY = 359;
        }
        if (Input.GetKey(KeyCode.D))
        {
            angleY += 1f;
            if (angleY > 360)
                angleY = 0;
        }
        //        Debug.Log(angleX + "/" + angleY);
        //極座標→直交座標
        float x = radius * Mathf.Cos(angleX * Mathf.Deg2Rad) * Mathf.Cos(angleY * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angleX * Mathf.Deg2Rad);
        float z = radius * Mathf.Cos(angleX * Mathf.Deg2Rad) * Mathf.Sin(angleY * Mathf.Deg2Rad);
        //カメラの位置
        transform.position = new Vector3(x, y, z);
        //カメラの向き
        transform.LookAt(lookTarget);
    }
}
