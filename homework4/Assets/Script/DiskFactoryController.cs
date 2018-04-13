using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactoryController : MonoBehaviour
{
    public GameObject disk;

    void Awake()
    {
        DiskFactory.getInstance().disk = disk;
    }
}