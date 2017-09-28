using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing_Data : MonoBehaviour {

    [SerializeField]
    private GameObject pathing_point;

    public Vector3 node;

	void Start () {
        node = pathing_point.transform.position;
	}
}
