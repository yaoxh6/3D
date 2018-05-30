using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 因为做粒子效果要结合逻辑处理，所以除了简单的粒子效果，还加上了点击事件
 以及为了用协程，写了自动载入场景的功能
 顺便加了图片淡入淡出的效果，以及音效
 */

/*
 * 参考师兄代码，将此代码挂载到粒子系统上即可运行，另外需要自己设置背景之类的
 * 其中有些过期的方法，已经更正。
 */
public class ParticleHalo : MonoBehaviour {

    public class CirclePosition
    {
        public float radius = 0f, angle = 0f, time = 0f;
        public CirclePosition(float radius, float angle, float time)
        {
            this.radius = radius;   // 半径
            this.angle = angle;     // 角度
            this.time = time;       // 时间
        }
    }

    public AudioSource darkSoulBGM;
    bool isExpand = false;
    public Image darkSoul;

    private ParticleSystem particleSysMain;  // 粒子系统
    private ParticleSystem.Particle[] particleArr;  // 粒子数组
    private CirclePosition[] circle; // 极坐标数组

    public int count = 10000;       // 粒子数量  
    public float size = 0.03f;      // 粒子大小
    public float minRadius = 5.0f;  // 最小半径
    public float maxRadius = 12.0f; // 最大半径
    public bool clockwise = true;   // 顺时针|逆时针  
    public float speed = 2f;        // 速度  
    public float pingPong = 0.02f;  // 游离范围

    private int tier = 10;  // 速度差分层数
    private float Yposition = 0f;
    // Use this for initialization
    void Start () {
        // 初始化粒子数组  
        particleArr = new ParticleSystem.Particle[count];
        circle = new CirclePosition[count];

        // 初始化粒子系统  
        particleSysMain = this.GetComponent<ParticleSystem>();
        var particleSys = particleSysMain.main;
        particleSys.startSpeed = 0;            // 粒子初始速度
        particleSys.startSize = size;          // 粒子初始大小 
        particleSys.loop = false;
        particleSys.maxParticles = count;      // 设置最大粒子量  
        particleSysMain.Emit(count);               // 发射粒子  
        particleSysMain.GetParticles(particleArr);

        RandomlySpread();   // 初始化各粒子位置  
    }
	
	void Update () {

        if(isExpand == true)
        {
            //这里处理了图片的淡入淡出
            Color test = darkSoul.color;
            test.a += 0.25f * Time.deltaTime;
            if (test.a > 0.5f)
            {
                test.a = 0.5f;
            }
            Yposition += Time.deltaTime;
            darkSoul.color = test;
        }

        for (int i = 0; i < count; i++)
        {
            if (clockwise)
            {
                circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
            }
            else
            {
                circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);
            }

            // 保证angle在0~360度  
            circle[i].angle = (360.0f + circle[i].angle) % 360.0f;
            float theta = circle[i].angle / 180 * Mathf.PI;

            //在这里改变半径大小就可以显示扩展效果
            if (isExpand == true)
            {
                if(circle[i].radius < 18.0f)
                {
                    circle[i].radius += Time.deltaTime * 4;
                }
            }

            // 粒子在半径方向上游离
            circle[i].time += Time.deltaTime;
            circle[i].radius += Mathf.PingPong(circle[i].time / minRadius / maxRadius, pingPong) - pingPong / 2.0f;

            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), Yposition, circle[i].radius * Mathf.Sin(theta));
        }

        particleSysMain.SetParticles(particleArr, particleArr.Length);
    }

    void RandomlySpread()
    {
        for (int i = 0; i < count; ++i)
        {   // 随机每个粒子距离中心的半径，同时希望粒子集中在平均半径附近
            float midRadius = (maxRadius + minRadius) / 2;
            float minRate = Random.Range(1.0f, midRadius / minRadius);
            float maxRate = Random.Range(midRadius / maxRadius, 1.0f);
            float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);

            // 随机每个粒子的角度  
            float angle = Random.Range(0.0f, 360.0f);
            float theta = angle / 180 * Mathf.PI;

            // 随机每个粒子的游离起始时间  
            float time = Random.Range(0.0f, 360.0f);

            circle[i] = new CirclePosition(radius, angle, time);

            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
        }

        particleSysMain.SetParticles(particleArr, particleArr.Length);
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 120, 30), "半径扩大"))
        {
            //播放BGM
            darkSoulBGM.Play();
            isExpand = true;
            //开始协程
            StartCoroutine(ReStart());
        }
        if(GUI.Button(new Rect(0, 30, 120, 30), "Reset"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    IEnumerator ReStart()
    {
        //七秒钟后载入SampleScene，但是颜色会变成浅黄色，感觉怪怪的
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene("SampleScene");
    }

}
