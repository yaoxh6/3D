using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFollowAction : SSAction
{
    private float speed = 2f;            //跟随玩家的速度
    private GameObject player;           //玩家
    private PatrolData data;             //侦查兵数据

    private PatrolFollowAction() { }
    public static PatrolFollowAction GetSSAction(GameObject player)
    {
        PatrolFollowAction action = CreateInstance<PatrolFollowAction>();
        action.player = player;
        return action;
    }

    public override void Update()
    {
        //因为碰撞会产生不可预料的结果，所以还是要保证模型在正确的位置
        if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        //追踪动作
        Follow();
        //如果没有跟随的玩家，或者玩家不在自己所在的区域，就会调用ISSActionCallback接口中的函数，切换到巡逻状态
        //所以说动作的管理是靠ISSActionCallback实现的，同样在巡逻的时候的动作也会有切换到追踪状态的方法。
        if (!data.follow_player || data.wall_sign != data.sign)
        {
            //当前动作摧毁掉
            this.destroy = true;
            //切换到巡逻状态
            this.callback.SSActionEvent(this,1,this.gameobject);
        }
    }
    public override void Start()
    {
        data = this.gameobject.GetComponent<PatrolData>();
    }
    void Follow()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        this.transform.LookAt(player.transform.position);
    }
}
