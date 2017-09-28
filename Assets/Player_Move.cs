using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    #region variables

    [SerializeField]
    private Map_Manager map;

    [SerializeField]
    private float speedH = 10f;

    [SerializeField]
    private float jumpImpulse = 10f;

    private Vector3 velocity = Vector3.zero;

    bool isJumping = false;

    [SerializeField]
    private int forwards = 0;
    [SerializeField]
    private int backwards = -1;

    #endregion

    // Use this for initialization
    void Start () {
        if( map == null && GameObject.Find("Map")) {
            map = GameObject.Find("Map").GetComponent<Map_Manager>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        //variables to store the heading node locations
        Vector3 headingF = Vector3.zero;
        if( forwards >= 0 ) headingF = map.path[forwards].GetComponent<Pathing_Data>().node;
        Vector3 headingB = Vector3.zero;//we can only move backwards if there is a backwards to move to!
        if( backwards >= 0 ) headingB = map.path[backwards].GetComponent<Pathing_Data>().node;

        //we need to identify the map sector we're moving towards and away from
        float axisH = Input.GetAxisRaw("Horizontal");
        float axisV = Input.GetAxisRaw("Vertical");

        //if a player wishes to move forwards, we move towards the next node
        if ( axisH > 0 ) {
            //aim the velocity at the node
            velocity += Vector3.MoveTowards(velocity, headingF, speedH);
        }

        //if they want to go backwards, we aim ourselves at the previous node, as long as there IS one
        if( axisH < 0 ) {
            if( backwards != -1 ) {
                velocity += Vector3.MoveTowards(velocity, headingB, speedH);
            }
        }

        print(velocity);

        //enable gravity to counteract jumping
        //velocity += Physics.gravity * Time.deltaTime;

        //Actually MOVE the pawn now
        transform.position += velocity * Time.deltaTime;

        //Have we passed a node?  if so, we need to go to the next/previous node
        if( transform.position.z > headingF.z ) {
            forwards++;
            backwards++;
        }
        if( transform.position.z < headingB.z ) {
            forwards--;
            backwards--;
        }

    }
}
