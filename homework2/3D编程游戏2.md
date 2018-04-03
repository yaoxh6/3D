[TOC]
#1、简答并用程序验证#
##游戏对象运动的本质是什么？##
游戏对象位置和状态的改变
##请用三种方法以上方法，实现物体的抛物线运动。##
###第一种###
以(0,0,0)为原点，分别在(0,0,0)和(2,4,0)放两个cube作为起点和终点，模拟出$y=x^2$的效果，方法就是利用速度控制x方向的值，然后再用x的值推出y的值。用的是Transform的方法。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour {

    public float xspeed = 1.0f;
    public Transform begin;
    public Transform end;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        begin.transform.position += Vector3.right * xspeed * Time.deltaTime;
        begin.transform.position = Vector3.right * begin.transform.position.x + Vector3.up * begin.transform.position.x * begin.transform.position.x;
    }
}

```

###第二种###
用rigidbody，(2,4,0)的cube作为起点，(0,0,0)的cube作为终点，给上面的cube一个向左初速度1，那么cube下落的路线会非常接近下面的cube。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public Rigidbody begin;
    // Use this for initialization
    void Start()
    {
        //begin = GetComponent<Rigidbody>();
        begin.velocity = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

```

###第三种###
用Transform.Translate方法，(2,4,0)为起点，给向左的一个固定速度V0，由于向下的速度要为at，所以设置一个变量t，每帧增加0.3，作为一个正比例函数，两个方向上合成路线就是抛物线。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3 : MonoBehaviour {

    public Transform begin;
    public float t = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        begin.transform.Translate(Vector3.left * Time.deltaTime);
        begin.transform.Translate(Vector3.down * Time.deltaTime * t, Space.World);
        t += 0.3f;
    }
}

```

##写一个程序，实现一个完整的太阳系， 其他星球围绕太阳的转速必须不一样，且不在一个法平面上。##

###第一步 建立10个球体###
加上月亮和九大行星，一共十个球体
![十个球体](https://img-blog.csdn.net/20180401093723150?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3lhb3hoNg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

###第二步 制作material###
在网上找到相应行星的3d贴图，然后做成一个material，制作成下面的样子，拖到球体上
![贴图](https://img-blog.csdn.net/20180401093444682?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3lhb3hoNg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

###第三步 写脚本###
以太阳为中心，用RotateAround函数，其中的角度随机生成，用Vector3控制，因为要保证接近Y轴，所以写x,z的值相对较小，y的值相对较大。将脚本拖到相应的球体中，然后将太阳，在拖到该球体的Transfrom中，设置一下speed即可。月球对地球的绑定也是直接拖拽，无需重新写代码，只要把月球脚本中的Transform换成地球就可以了。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public Transform sun;
    public float speed;
    private float rx;
    private float ry;
    private float rz;
	// Use this for initialization
	void Start () {
        rx = Random.Range(10, 30);
        ry = Random.Range(40, 60);
        rz = Random.Range(10, 30);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 axis = new Vector3(rx, ry, rz);
        this.transform.RotateAround(sun.position, axis, speed * Time.deltaTime);
	}
}

```

###第四步 自由视角###
主要实现从各个角度观看太阳系，用wasd或方向键控制上下左右，按住鼠标右键，可以用鼠标控制镜头。

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class view : MonoBehaviour {
    public float sensitivityMouse = 2f;
    public float sensitivetyKeyBoard = 0.1f;
    public float sensitivetyMouseWheel = 10f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //按着鼠标右键实现视角转动  
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivityMouse, Input.GetAxis("Mouse X") * sensitivityMouse, 0);
        }

        //键盘按钮←/a和→/d实现视角水平移动，键盘按钮↑/w和↓/s实现视角水平旋转  
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Input.GetAxis("Horizontal") * sensitivetyKeyBoard, 0, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(0, Input.GetAxis("Vertical") * sensitivetyKeyBoard, 0);
        }
    }
}

```
#编程实践#
>Priests and Devils

Priests and Devils is a puzzle game in which you will help the Priests and Devils to cross the river within the time limit. There are 3 priests and 3 devils at one side of the river. They all want to get to the other side of this river, but there is only one boat and this boat can only carry two persons each time. And there must be one person steering the boat from one side to the other side. In the flash game, you can click on them to move them and click the go button to move the boat to the other direction. If the priests are out numbered by the devils on either side of the river, they get killed and the game is over. You can try it in many > ways. Keep all priests alive! Good luck!

 - 列出游戏中提及的事物（Objects）
 - 用表格列出玩家动作表（规则表），注意，动作越少越好
 - 请将游戏中对象做成预制
 - 在 GenGameObjects 中创建 长方形、正方形、球 及其色彩代表游戏中的对象。
 - 使用 C# 集合类型 有效组织对象
 - 整个游戏仅 主摄像机 和 一个 Empty 对象， 其他对象必须代码动态生成！！！ 。 整个游戏不许出现 Find 游戏对象， SendMessage 这类突破程序结构的 通讯耦合 语句。 违背本条准则，不给分
 - 请使用课件架构图编程，不接受非 MVC 结构程序
 - 注意细节，例如：船未靠岸，牧师与魔鬼上下船运动中，均不能接受用户事件！
 - play the game ( http://www.flash-game.net/game/2535/priests-and-devils.html )

##MVC模式##
所谓mvc模式，模型，视图，控制分离。
我的文件结构
![文件结构](https://img-blog.csdn.net/20180403162514798?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3lhb3hoNg==/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)

 - Boat，Coast，Character，Water，作为Model
 - Click，move，作为控制
 - simplGUI，作为界面
 - other，main，分别作为接口和实现接口已经程序的入口
 
 ##类的结构##
 ###Character类###
 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    private GameObject character;
    public int TypeOfCharacter;//1代表魔鬼,0代表牧师
    private move Cmove;
    public int characterStatus;//0代表在岸上,1代表在船上
    Coast coast;
    Click click;

    public Character(int input)
    {
        if(input == 0)
        {
            character = Object.Instantiate(Resources.Load("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            TypeOfCharacter = 0;
        }
        else
        {
            character = Object.Instantiate(Resources.Load("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            TypeOfCharacter = 1;
        }

        Cmove = character.AddComponent(typeof(move)) as move;
        click = character.AddComponent(typeof(Click)) as Click;
        click.setGameObj(this);
    }
	
    public string getName()
    {
        return character.name;
    }

    public void setName(string input)
    {
        character.name = input;
    }

    public void setPosition(Vector3 input)
    {
        character.transform.position = input;
    }

    public void getOnBoat(Boat input)
    {
        character.transform.parent = input.getGameobj().transform;
        characterStatus = 1;
    }

    public void getOnCoast(Coast input)
    {
        character.transform.parent = null;
        characterStatus = 0;
        coast = input;
    }

    public void reset()
    {
        Cmove.reset();
        coast = (Director.getInstance().currentSceneController as main).RightCoast;
        getOnCoast(coast);
        setPosition(coast.getEmptyPosition());
        coast.getOnCoast(this);
    }

    public void moveToPosition(Vector3 input)
    {
        Cmove.setDestination(input);
    }

    public Coast getCoast()
    {
        return coast;
    }
}

```
####Character类的解释####
>成员变量

 - `private GameObject character;`必须的成员，在游戏中用cube代替
 - `public int TypeOfCharacter;`作为牧师和恶魔的标识，牧师是0，恶魔是1，因为没有把牧师和恶魔作为分开的类，在后面计算牧师和恶魔数量的时候需要用到。
 - `private move Cmove;` Character需要移动，因为牧师和恶魔的动作基本一致，所以也没有分开写成两个动作。
 - `public int characterStatus;` 表示Character的状态是在岸上还是在船上，0在岸上，1在船上。
 - `Coast coast;` 什么Character类会需要coast类，因为需要判断当前的人物是站在左边的coast上还是右边的coast上，已经上下船的时候，确定所在的coast。
 - `Click click;` 物体的点击事件。
 >成员函数，只说明重要的部分
 
 - 第一个是挂载move和Click，
 `Cmove = character.AddComponent(typeof(move)) as move;
  click = character.AddComponent(typeof(Click)) as Click;`
  
 - ` public void getOnBoat(Boat input)`，在上船的时候，将Character变成船的子节点，便于船统计Character的数量。同样`public void getOnCoast(Coast input)` 在上岸的时候解除该关系，并确定上的是哪个河岸。
 - `public void moveToPosition(Vector3 input)` 这个利用的是move里面的方法。

###Coast类###

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coast {

    private GameObject coast;
    public int TypeOfCoast;//-1代表左边,1代表右边
    Vector3 LeftPos = new Vector3(-9,1,0);
    Vector3 RightPos = new Vector3(9, 1, 0);
    Vector3[] positions;
    Character[] charactersOnCoast;
    public Coast(int input)
    {
        positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
                new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};
        charactersOnCoast = new Character[6];
        if (input == -1)
        {
            coast = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)),LeftPos, Quaternion.identity, null) as GameObject;
            TypeOfCoast = -1;
        }
        else
        {
            coast = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)), RightPos, Quaternion.identity, null) as GameObject;
            TypeOfCoast = 1;
        }
    }

    public int getEmptyIndex()
    {
        for (int i = 0; i < charactersOnCoast.Length; i++)
        {
            if (charactersOnCoast[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos = positions[getEmptyIndex()];
        pos.x *= TypeOfCoast;
        return pos;
    }

    public void getOnCoast(Character input)
    {
        int index = getEmptyIndex();
        charactersOnCoast[index] = input;
    }

    public Character getOffCoast(string input)
    {   
        for (int i = 0; i < charactersOnCoast.Length; i++)
        {
            if (charactersOnCoast[i] != null && charactersOnCoast[i].getName() == input)
            {
                Character charactorLeaveCoast = charactersOnCoast[i];
                charactersOnCoast[i] = null;
                return charactorLeaveCoast;
            }
        }
        return null;
    }

    public void reset()
    {
        charactersOnCoast = new Character[6];
    }

    public int[] getCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < charactersOnCoast.Length; i++)
        {
            if (charactersOnCoast[i] == null)
                continue;
            if (charactersOnCoast[i].TypeOfCharacter == 0)
            {   
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }
}

```

####Coast类的解释####
> 成员变量 只说不重复的

 - `public int TypeOfCoast;` 标识是左河岸还是右河岸，-1代表左河岸，1代表是右河岸，为什么用-1，不用0，因为左右河岸对称，乘以1，或者-1，正好取到两边的位置。
 - `Vector3 LeftPos = new Vector3(-9,1,0);` 和 `Vector3 RightPos = new Vector3(9, 1, 0);` 便于初始化。
 - `Vector3[] positions;` 初始化成6个位置，分别给6个Character；
 - `Character[] charactersOnCoast;` 记录在该岸上的Character
 > 成员函数
 
 - `public Character getOffCoast(string input)` 在下岸的时候，将岸上的那个Character置为null。
 
 ###Boat类###
 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat {

    private GameObject boat;
    Vector3 LeftPos = new Vector3(-5, 1, 0);
    Vector3 RightPos = new Vector3(5, 1, 0);
    Vector3[] characterOnLeftPos;
    Vector3[] characterOnRightPos;
    move Bmove;
    public int BoatPosStatus;//-1在左边,1在右边
    Character[] characterOnBoat;
    Click click;

    public Boat()
    {
        characterOnBoat = new Character[2];
        characterOnRightPos = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        characterOnLeftPos = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };
        boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)),RightPos , Quaternion.identity, null) as GameObject;
        boat.name = "boat";
        Bmove = boat.AddComponent(typeof(move)) as move;
        BoatPosStatus = 1;
        click = boat.AddComponent(typeof(Click)) as Click;
    }

    public void Move()
    {
        Debug.Log("Move");
        if (BoatPosStatus == -1)
        {
            Bmove.setDestination(RightPos);
            BoatPosStatus = 1;
        }
        else
        {
            Bmove.setDestination(LeftPos);
            BoatPosStatus = -1;
        }
    }

    public int getEmptyIndex()
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool isEmpty()
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos;
        int emptyIndex = getEmptyIndex();
        if (BoatPosStatus == -1)
        {
            pos = characterOnLeftPos[emptyIndex];
        }
        else
        {
            pos = characterOnRightPos[emptyIndex];
        }
        return pos;
    }

    public void GetOnBoat(Character input)
    {
        int index = getEmptyIndex();
        characterOnBoat[index] = input;
    }

    public Character GetOffBoat(string input)
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] != null && characterOnBoat[i].getName() == input)
            {
                Character character = characterOnBoat[i];
                characterOnBoat[i] = null;
                return character;
            }
        }
        return null;
    }

    public void reset()
    {
        if ( BoatPosStatus == -1)
        {
            Move();
        }
        characterOnBoat = new Character[2];
    }

    public GameObject getGameobj()
    {
        return boat;
    }

    public int[] getCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] == null)
                continue;
            if (characterOnBoat[i].TypeOfCharacter == 0)
            {
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }
}

```
####Boat类的解释####
和之前Character和Coast类似。

###Water类###

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water {

    private GameObject water;
    Vector3 Position = new Vector3(0, 0.5f, 0);

    public Water()
    {
        water = Object.Instantiate(Resources.Load("Perfabs/Water", typeof(GameObject)), Position, Quaternion.identity, null) as GameObject;
    }
}

```

####Water类的解释####
Water类只是作为一个静物，没有任何动作和联系，但是为什么单独成类，因为方便以后功能的添加，比如想让water有波纹之类的。

###Click类###

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    ClickAction action;
    Character character;

    public void setGameObj(Character input)
    {
        character = input;
    }
    
    void Start()
    {
        action = Director.getInstance().currentSceneController as ClickAction;
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        if (gameObject.name == "boat")
        {
            action.ClickBoat();
        }
        else
        {
            action.ClickCharacter(character);
        }
    }
}

```

####Click类的解释####
>成员变量

 - `ClickAction action;` ClickAction是由main实现的关于点击的接口
 - `Character character;` 有Character类，没有Boat类，因为Boat只有一个，点击的时候并不需要判断是哪一个Boat，但似乎Character需要判断。
>成员函数
 - `void OnMouseDown()` 通过鼠标点击的对象，传给action，做出相应的动作。
 
 ###move类###
 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    private float moveSpeed = 20;

    int moveStatus;//0静止,1移动到中间,2移动到终点
    Vector3 Destination;
    Vector3 Middle;

    private void Update()
    {
        if(moveStatus == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Middle, moveSpeed * Time.deltaTime);
            if (transform.position == Middle)
            {
                moveStatus = 2;
            }
        }
        else if(moveStatus == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, moveSpeed * Time.deltaTime);
            if(transform.position == Destination)
            {
                moveStatus = 0;
            }
        }


    }

    public void setDestination(Vector3 input)
    {
        Middle = input;
        Destination = input;
        if(transform.position.y == input.y)
        {
            moveStatus = 2;
        }
        else if(transform.position.y > input.y)
        {
            Middle.y = transform.position.y;
            moveStatus = 1;
        }
        else
        {
            Middle.x = transform.position.x;
            moveStatus = 1;
        }
    }

    public void reset()
    {
        moveStatus = 0;
    }
}

```
####move类的解释####
> 成员变量

 - `int moveStatus;` 用来判读移动的状态，0静止,1移动到中间,2移动到终点。
 - `Vector3 Destination;`和`Vector3 Middle;` 有Middle的原因，防止，Character直线穿墙上船，视觉效果不好。
 > 成员函数
 
 - `public void setDestination(Vector3 input)` 这个函数的实现，通过Boat和Character的高度，判断是移动船还是移动人，而且通过Destination确定Middle的位置。
 
 ###other###
 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : System.Object
{
    private static Director _instance;
    public SceneController currentSceneController { get; set; }

    public static Director getInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }
}

public interface SceneController
{
    void loadResources();
}

public interface ClickAction
{
    void ClickBoat();
    void ClickCharacter(Character input);
    void ClickReset();
}


```
####other的解释####
不知道该怎么划分，就放到other里面了，主要用了单例模式，和声明了场景控制以及点击事件的接口。

###simpleGUI###

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleGUI : MonoBehaviour {

    private ClickAction action;
    public int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;

    void Start()
    {
        action = Director.getInstance().currentSceneController as ClickAction;

        style = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;
    }
    void OnGUI()
    {
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.ClickReset();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.ClickReset();
            }
        }
    }
}

```

####simpleGUI的解释####
原本是叫GUI，但是具体写程序的时候凡是遇到`GUI.` 这样的都没法运行，所以改成了simpleGUI。

###mian###

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class main : MonoBehaviour,SceneController,ClickAction {

    private Coast LeftCoast;
    public Coast RightCoast;
    private Boat boat;
    private Character[] characters;
    private Water water;
    public simpleGUI simplegui;
    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        characters = new Character[6];
        loadResources();
        simplegui = gameObject.AddComponent<simpleGUI>() as simpleGUI   ;
    }
    public void loadResources()
    {
        LeftCoast = new Coast(-1);
        RightCoast = new Coast(1);
        boat = new Boat();
        water = new Water();
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("ok");
            Character newCharacter = new Character(0);
            newCharacter.setName("priest" + i);
            newCharacter.setPosition(RightCoast.getEmptyPosition());
            newCharacter.getOnCoast(RightCoast);
            RightCoast.getOnCoast(newCharacter);

            characters[i] = newCharacter;
        }
        for (int i = 0; i < 3; i++)
        {
            Character newCharacter = new Character(1);
            newCharacter.setName("devil" + i);
            newCharacter.setPosition(RightCoast.getEmptyPosition());
            newCharacter.getOnCoast(RightCoast);
            RightCoast.getOnCoast(newCharacter);

            characters[i+3] = newCharacter;
        }
    }

    public void ClickBoat()
    {
        if (boat.isEmpty())
        {
            return;
        }
        else
        {
            boat.Move();
        }
        simplegui.status = gameStatus();
    }

    public void ClickCharacter(Character input)
    {
        if (input.characterStatus == 1)//0在岸上,1在船上
        {
            Coast coast;
            Debug.Log(boat.BoatPosStatus);
            if (boat.BoatPosStatus == -1)
            {
                coast = LeftCoast;
            }
            else
            {
                coast = RightCoast;
            }
            boat.GetOffBoat(input.getName());
            Debug.Log(coast.getEmptyPosition());
            input.moveToPosition(coast.getEmptyPosition());
            input.getOnCoast(coast);
            coast.getOnCoast(input);

        }
        else
        {
            Coast coast = input.getCoast();

            if (boat.getEmptyIndex() == -1)
            {
                return;
            }
            if (coast.TypeOfCoast != boat.BoatPosStatus)
            {
                return;
            }
            coast.getOffCoast(input.getName());
            input.moveToPosition(boat.getEmptyPosition());
            input.getOnBoat(boat);
            boat.GetOnBoat(input);
        }
        Debug.Log(gameStatus());
        simplegui.status = gameStatus();
    }
    public void ClickReset()
    {
        boat.reset();
        LeftCoast.reset();
        RightCoast.reset();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].reset();
        }
    }

    public int gameStatus()
    {
        int from_priest = 0;
        int from_devil = 0;
        int to_priest = 0;
        int to_devil = 0;

        int[] fromCount = RightCoast.getCharacterNum();
        from_priest += fromCount[0];
        from_devil += fromCount[1];
        int[] toCount = LeftCoast.getCharacterNum();
        to_priest += toCount[0];
        to_devil += toCount[1];

        if (to_priest + to_devil == 6)      
            return 2;

        int[] boatCount = boat.getCharacterNum();
        if (boat.BoatPosStatus == -1)
        {   
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        else
        {   
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        if (from_priest < from_devil && from_priest > 0)
        {
            return 1;
        }
        if (to_priest < to_devil && to_priest > 0)
	    {
            return 1;
        }
        return 0;
    }
}

```
####main的解释####
对接口类的实现，补充了判断游戏结束的函数。

#资源#

[太阳系，牧师与恶魔](https://github.com/yaoxh6/3D/tree/master/homework2)