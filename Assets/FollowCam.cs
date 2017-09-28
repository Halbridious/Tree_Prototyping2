using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    [SerializeField]
    private GameObject target;

	// Use this for initialization
	void Start () {
        if(target == null ) {
            target = GameObject.Find("Player");
        }		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position;
	}
}
