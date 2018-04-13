using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FirstController : MonoBehaviour
{
    public Color[] TotalColor = {Color.black,Color.blue,Color.cyan,Color.green,Color.grey,Color.red,Color.yellow };
    public float EmissionDelay = 1f;
    public float timeToNextEmission;
    private bool isCounting;
    private bool isShooting;
    public bool getIsCounting() { return isCounting; }
    public bool getIsShooting() { return isShooting; }

    private List<GameObject> disks = new List<GameObject>();   
    private List<int> diskIds = new List<int>();               
    private int diskScale;                  
    private Color diskColor;               
    private Vector3 emissionPosition;          
    private Vector3 emissionDirection;         
    private float emissionSpeed;
    private int emissionNumber;                 
    private bool isEmissionEnable;               

    private SceneController scene;
    private RoundController roundController;

    void Awake()
    {
        scene = SceneController.getInstance();
        scene.setFirstController(this);
    }

    public void setting(int scale, Color color, Vector3 emitPos, Vector3 emitDir, float speed, int num)
    {
        diskScale = scale;
        diskColor = color;
        emissionPosition = emitPos;
        emissionDirection = emitDir;
        emissionSpeed = speed;
        emissionNumber = num;
    }


    public void MakeEmissionDiskable()
    {
        if (!isCounting && !isShooting)
        {
            timeToNextEmission = EmissionDelay;
            isEmissionEnable = true;
        }
    }

    void emissionDisks()
    {
        if (scene.getRound() == 4)
        {
            scene.gameOver();
        }
        scene.setTrial(scene.getTrial()+1);
        for (int i = 0; i < emissionNumber; ++i)
        {
            diskIds.Add(DiskFactory.getInstance().getDiskId());
            disks.Add(DiskFactory.getInstance().getDiskObject(diskIds[i]));
            diskScale = Random.Range(1,3);
            disks[i].transform.localScale *= diskScale;
            int chooseColor = Random.Range(0, 7);
            disks[i].GetComponent<Renderer>().material.color = TotalColor[chooseColor];
            disks[i].transform.position = new Vector3(Random.Range(-2.5f,2.5f), emissionPosition.y + i, emissionPosition.z);
            disks[i].SetActive(true);
            emissionDirection.x = emissionDirection.x * Random.Range(-1, 1);
            disks[i].GetComponent<Rigidbody>().AddForce(emissionDirection * Random.Range(emissionSpeed * 5, emissionSpeed * 10) / 10, ForceMode.Impulse);
        }
        if(scene.getTrial() == 10)
        {
            scene.nextRound();
        }
    }

    void freeADisk(int i)
    {
        DiskFactory.getInstance().free(diskIds[i]);
        disks.RemoveAt(i);
        diskIds.RemoveAt(i);
    }

    void FixedUpdate()
    {
        if (timeToNextEmission > 0)
        {
            isCounting = true;
            timeToNextEmission -= Time.deltaTime;
        }
        else
        {
            isCounting = false;
            if (isEmissionEnable)
            {
                emissionDisks();
                isEmissionEnable = false;
                isShooting = true;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < disks.Count; i++)
        {
            if (!disks[i].activeInHierarchy)
            {  
                scene.getScoreController().hitDisk(); 
                freeADisk(i);
            }
            else if (disks[i].transform.position.y < 0)
            {   
                scene.getScoreController().hitGround(scene.getRound()); 
                freeADisk(i);
            }
        }
        if (disks.Count == 0)
        {
            isShooting = false;
        }
    }
}