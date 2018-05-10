using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家与巡逻兵碰撞
public class PlayerCollideDetection : MonoBehaviour {
    //当玩家与巡逻兵碰撞
    void OnCollisionEnter(Collision other)
    {
        //在这里皮了一下，因为一开始是想做攻击判定的，但是剑和人的模型是一起的
        //没法单独给剑加一个碰撞盒，所以只好放弃
        //于是想攻击的时候直接让巡逻兵的碰撞盒半径为0，然后播放死亡动画也可以达到目的
        //然后发现会鬼畜，就注释掉了，现在只要在攻击的时候碰到巡逻兵，巡逻兵会直接消失
        //一般情况下的，玩家会自己挂掉，并且播放死亡动画，巡逻兵也会砍一刀然后死掉。
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Animator>().GetBool("attack1"))
        {
            //this.gameObject.GetComponent<CapsuleCollider>().radius = 0;
            this.gameObject.SetActive(false);
            return;
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetBool("death",true);
            this.GetComponent<Animator>().SetTrigger("shoot");
            Singleton<GameEventManager>.Instance.PlayerGameover();
        }
    }
}
