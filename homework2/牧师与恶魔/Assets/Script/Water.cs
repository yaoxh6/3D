using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water {

    private GameObject water;
    Vector3 Position = new Vector3(0, 0.5f, 0);

    public Water()
    {
        water = Object.Instantiate(Resources.Load("Perfabs/Water", typeof(GameObject)), Position, Quaternion.identity, null) as GameObject;
    }
}
