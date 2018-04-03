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
