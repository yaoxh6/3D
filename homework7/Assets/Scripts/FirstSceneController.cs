using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public PatorlFactory _PatorlFactory;                             //巡逻者工厂
    public ScoreRecorder _ScoreRecorder;                             //记录员
    public PatrolActionManager _PatrolActionManager;                 //运动管理器
    public int wall_sign = -1;                                       //当前玩家所处哪个格子
    public GameObject player;                                        //玩家
    public Camera main_camera;                                       //主相机
    private List<GameObject> patrols;                                //场景中巡逻者列表
    private float player_speed = 5;                                  //玩家移动速度
    private float rotate_speed = 135f;                               //玩家旋转速度
    private bool game_over = false;                                  //游戏结束

    void Update()
    {
        for (int i = 0; i < patrols.Count; i++)
        {
            patrols[i].gameObject.GetComponent<PatrolData>().wall_sign = wall_sign;
        }
        //20分结束游戏
        if(_ScoreRecorder.score == 20)
        {
            Gameover();
        }
    }
    void Start()
    {
        //这一块注意一下，关于实例化，和实例化的查找
        //首先director是单例模式，所以用GetInstance方法，即使没有实例也会创建实例，有实例就确保只有一个实例
        //那么_PatorlFactory和_PatrolActionManager是怎么回事
        //首先看到代码可以这样写，用Singleton，这个是直接查找有没有这个类，没有就报错了
        //所以使用这个有个前提条件，就是必须已经存在了，所以在游戏开始之前必须把代码绑定到对应的物体上
        //如果不采用这种方法怎么办，可以手动添加，用AddComponent就相当于把手动添加的部分，用代码实现了，就是注释掉的部分
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        _PatorlFactory = Singleton<PatorlFactory>.Instance;
        _PatrolActionManager = Singleton<PatrolActionManager>.Instance;
        //_PatrolActionManager = gameObject.AddComponent<PatrolActionManager>() as PatrolActionManager;

        //ISceneController接口中函数的实现
        LoadResources();
        //设置摄像机跟随的对象，也就是玩家，如果不用工厂模式的话，直接在场景中生成玩家，然后把玩家拖到摄像机的Targer就可以了
        //这一块也要注意，main_camera的位置必须在LoadResources之后，因为main_camera跟随的对象是player，如果player没有实例化就无法运行
        //所以如果一开始在场景中就有player的话，下面的代码位置就可以随便放。
        main_camera.GetComponent<CameraFlow>().follow = player;
        _ScoreRecorder = Singleton<ScoreRecorder>.Instance;
    }

    public void LoadResources()
    {
        player = _PatorlFactory.LoadPlayer();

        //导入资源的时候为了巡逻兵有动作
        patrols = _PatorlFactory.LoadPatrol();
        for (int i = 0; i < patrols.Count; i++)
        {
            _PatrolActionManager.GoPatrol(patrols[i]);
        }
    }
    public void Attack()
    {
        if (!game_over)
        {
            //Fire1对应鼠标左键
            if(Input.GetButtonDown("Fire1"))
            {
                player.GetComponent<Animator>().SetTrigger("attack1");
            }
            //Fire2对应鼠标右键
            if (Input.GetButtonDown("Fire2"))
            {
                player.GetComponent<Animator>().SetTrigger("attack2");
            }
            //Jump对应空格键space
            if (Input.GetButtonDown("Jump"))
            {
                player.GetComponent<Animator>().SetTrigger("jump");
            }
        }
    }


    public void MovePlayer(float translationX, float translationZ)
    {
        if(!game_over)
        {
            //移动的时候播放run动画，否则停止run动画
            if (translationX != 0 || translationZ != 0)
            {
                player.GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("run", false);
            }
            //这个操作有点类似于魔兽世界的操作，魔兽世界也是左右键不是直接移动而是旋转
            //配合摄像机始终对着玩家的背面，操作也算是不反人类
            //当然现在的一般游戏都不采用这种方法，都是摄像机在围绕主角的球上可以自动移动，而方向键直接移动。
            player.transform.Translate(0, 0, translationZ * player_speed * Time.deltaTime);
            player.transform.Rotate(0, translationX * rotate_speed * Time.deltaTime, 0);

            //以防以外发生
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0)
            {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
            if (player.transform.position.y != 0)
            {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }     
        }
    }

    public int GetScore()
    {
        return _ScoreRecorder.score;
    }
    public bool GetGameover()
    {
        return game_over;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/mySence");
    }

    //发布与订阅模式
    void OnEnable()
    {
        //这里很奇怪，直接用AddScore就可以，但是改成_ScoreRecorder.AddScore就会报错。
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
    }
    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
    }
    
    void AddScore()
    {
        _ScoreRecorder.AddScore();
    }
    void Gameover()
    {
        game_over = true;
        _PatorlFactory.StopPatrol();
        _PatrolActionManager.DestroyAllAction();
    }
}
