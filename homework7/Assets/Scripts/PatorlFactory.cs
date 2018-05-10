using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatorlFactory : MonoBehaviour {

    //工厂模式就负责生产，其他的不负责，所以可以看到这个类只和角色有关系和其他所有代码都没有关系
    private GameObject player = null;                                      //玩家
    private GameObject patrol = null;                                     //巡逻兵
    private List<GameObject> patrolList = new List<GameObject>();        //正在被使用的巡逻兵
    private Vector3[] vec = new Vector3[9];                             //保存每个巡逻兵的初始位置

    public GameObject LoadPlayer()
    {
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
        return player;
    }

    public List<GameObject> LoadPatrol()
    {
        int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int index = 0;

        for(int i=0;i < 3;i++)
        {
            for(int j=0;j < 3;j++)
            {
                vec[index] = new Vector3(pos_x[i], 0, pos_z[j]);
                index++;
            }
        }
        for(int i=0; i < 9; i++)
        {
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol1"));
            patrol.transform.position = vec[i];
            patrol.GetComponent<PatrolData>().sign = i + 1;
            patrol.GetComponent<PatrolData>().start_position = vec[i];
            patrolList.Add(patrol);
        }   
        return patrolList;
    }

    //游戏结束的时候会暂停所有动作
    public void StopPatrol()
    {
        for (int i = 0; i < patrolList.Count; i++)
        {
            patrolList[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
