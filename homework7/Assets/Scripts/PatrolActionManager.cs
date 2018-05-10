using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolActionManager : SSActionManager, ISSActionCallback
{
    //这个函数是游戏初始化的时候用到，最重要的实现还是在SSActionEvent函数中实现，也就是ISSActionCallback接口的函数
    public void GoPatrol(GameObject patrol)
    {
        GoPatrolAction go_patrol = GoPatrolAction.GetSSAction(patrol.transform.position);
        this.RunAction(patrol, go_patrol, this);
    }
    //停止所有动作
    public void DestroyAllAction()
    {
        DestroyAll();
    }
    //动作管理最终要的部分,实现各个动作的切换，以及条件判断。
    public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null)
    {
        if (intParam == 0)
        {
            PatrolFollowAction follow = PatrolFollowAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().player);
            this.RunAction(objectParam, follow, this);
        }
        else
        {
            GoPatrolAction move = GoPatrolAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().start_position);
            this.RunAction(objectParam, move, this);
            //委托事件，Singleton直接生成实例，调用PlayerEscape就相当于发出通知，那么在OnEnable函数就会触发加分的函数
            Singleton<GameEventManager>.Instance.PlayerEscape();
        }
    }
}
