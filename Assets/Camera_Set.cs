using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Set : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    [SerializeField]  int maxView = 75;
    [SerializeField]  int minView = 50;
    [SerializeField]  float slideSpeed = 20;

    private void OnEnable()
    {
        myCamera = this.GetComponent<Camera>();
    }
    private void OnDisable()
    {
        myCamera = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (myCamera == null) return;
        //获取虚拟按键(鼠标中轴滚轮)
        float mouseCenter = Input.GetAxis("Mouse ScrollWheel");

        //鼠标滑动中键滚轮,实现摄像机的镜头放大和缩放
        //mouseCenter < 0 = 负数 往后滑动,缩放镜头
        if (mouseCenter < 0)
        {
            //滑动限制
            if (myCamera.fieldOfView <= maxView)
            {
                myCamera.fieldOfView += 10 * slideSpeed * Time.deltaTime;
                if (myCamera.fieldOfView >= maxView)
                {
                    return;
                   // myCamera.fieldOfView = minView;
                }
            }
            //mouseCenter >0 = 正数 往前滑动,放大镜头
        }
        else if (mouseCenter > 0)
        {
            //滑动限制
            if (myCamera.fieldOfView >= minView)
            {
                myCamera.fieldOfView -= 10 * slideSpeed *
             Time.deltaTime;
                if (myCamera.fieldOfView <= minView)
                {
                    //myCamera.fieldOfView = maxView;
                    return;
                }
            }
        }
    }
}
