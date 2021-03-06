﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MOBACommon.Codes;
public class KeyController : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
        //当鼠标右键点击
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouse = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            //判断有没有投射到
            if (!Physics.Raycast(ray, out hit))
                return;
            //如果点击到了地面就移动
            if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
            {
                Move(hit.point);
            }
        }
        //当按下空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.GetComponent<CameraController>().FocusHero();
        }
    }
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="point"></param>
    private void Move(Vector3 point)
    {
        //生成鼠标特效
        GameObject go = PoolManager.Instance.GetObject("ClickMove");
        go.transform.position = point + Vector3.up;
        go.transform.localScale = Vector3.one * 2;
        //给服务器发送移动请求
        PhotonManager.Instance.Request(OperationCode.BattleCode, OpBattle.Walk, point.x, point.y, point.z);
    }
}
