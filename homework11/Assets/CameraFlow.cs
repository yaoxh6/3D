using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform follow;            //跟随的物体
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (follow)
        {
            Vector3 nextpos = follow.forward * -1 * 4 + follow.up * 3 + follow.position;
            this.transform.position = nextpos;
            this.transform.LookAt(new Vector3(follow.position.x, follow.position.y + 2, follow.position.z));
        }
    }
}