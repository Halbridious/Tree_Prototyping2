using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Manager : MonoBehaviour {

    [Tooltip("Path Archetypes go here")]
    [SerializeField]
    private GameObject[] path_types;

    [Tooltip("The player archetype")]
    [SerializeField]
    private GameObject player;

    [Tooltip("How many path items to make")]
    [SerializeField]
    private int pathLength;

    //contains all of the path objects after they're built
    public List<GameObject> path = new List<GameObject>();

	/*
     * when the game starts we make the path
     * 
     * We do this here so that the pathing nodes are all set up properly and we don't have to search a list of objects to add to an array
     * 
     * We can either manually make a path via code, or, for now, randomly instantiate one
     * */
	void Start () {

        //The initial path object is always a straight piece at the origin
        GameObject start = Instantiate(path_types[0], Vector3.zero, Quaternion.identity, transform);
        path.Add(start);

        print(start.GetComponent<Pathing_Data>().node);

        //we now randomly add the perscribed number of assets to the tree.
        //todo:branching logic?
        for( int i = 0; i < pathLength-1; i++ ) {
            
            //if there are multiple path archetypes
            int pathType = Random.Range((int)0, (int)path_types.Length);
            
            //get the transform of the 2nd child object of the previous path (i is NOT current path, thanks to the starter piece)
            //the transform of that object is the anchor point of the new object
            Vector3 pos = path[i].transform.GetChild(1).transform.position;

            //the rotation of the new piece, converted to a quaternion below.  The piece can be rotated x and y, not z
            Vector3 rot = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);

            GameObject obj = Instantiate(path_types[pathType], pos, Quaternion.Euler(rot), transform);
            path.Add(obj);
        }
	}
}
