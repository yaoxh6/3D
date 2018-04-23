using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactoryController : MonoBehaviour
{
    public GameObject disk;
    private SceneController scene;
    void Awake()
    {
        scene = SceneController.getInstance();
        scene.setDiskFactoryController(this);
        //DiskFactory.getInstance().disk = disk;
    }
    public void kinematic()
    {
        disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk2"));
        DiskFactory.getInstance().disk = disk;
    }

    public void physic()
    {
        disk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk1"));
        DiskFactory.getInstance().disk = disk;
    }
}