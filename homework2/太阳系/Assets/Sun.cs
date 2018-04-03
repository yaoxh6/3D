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
