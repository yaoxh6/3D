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
