using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家进入巡逻兵的追踪范围
public class PlayerInDetection : MonoBehaviour
{
    //玩家进入巡逻兵的追踪范围
    void OnTriggerEnter(Collider collider)
    {
        //加上判断Player的原因是，人物会与地板碰撞
        if (collider.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = true;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = collider.gameObject;
            //触发巡逻兵向前扑的动作
            this.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("shock");
        }
    }
    //玩家离开巡逻兵的追踪范围
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = false;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = null;
        }
    }
}
