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
