using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public GameObject follow;            //跟随的物体
    void Start()
    {
    }

    void FixedUpdate()
    {
        Vector3 nextpos = follow.transform.forward * -1 * 4 + follow.transform.up * 3 + follow.transform.position;
        this.transform.position = nextpos;
        this.transform.LookAt(new Vector3(follow.transform.position.x, follow.transform.position.y+2, follow.transform.position.z));
    }
}
