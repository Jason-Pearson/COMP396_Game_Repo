using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{

    public float forward;
    public float strafing;
    
    public int state = 1;
    public bool waypointreached = true;

    public NavMeshAgent mover;
    public List<GameObject> waypoints;

    public Rigidbody rb;
    public GameObject player;
    public MeshRenderer MR;
    public Material[] changeColor;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        mover = GetComponent<NavMeshAgent>();
        MR = GetComponent<MeshRenderer>();
        FindPatrolPoints();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        float Yforce = forward * Time.deltaTime;
        rb.AddForce(transform.forward * Yforce);

        float Xforce = strafing * Time.deltaTime;
        rb.transform.Rotate(0, Xforce, 0);
        
        //Vector2.Distance(gameObject, );

        /*
         * public Transform target;
    public float speed;
    void Update() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
         * */
        CheckState();

        switch (state)
        {
            case 0:
                //wander between patrol points
                if (waypointreached)
                {
                    MR.material = changeColor[0];
                    forward = 500;
                    strafing = 0;
                    int dest = Random.Range(0, waypoints.Count);
                    mover.destination = waypoints[dest].transform.position;
                    waypointreached = false;
                    Debug.Log("chase mode");
                    Debug.Log(dest);
                }
                if (Vector3.Distance(this.transform.position, mover.destination) <= 15) //once it hits <= 10, then wapoint=true and strafing immediately = 0 :(
                {
                    MR.material = changeColor[1];
                    forward = 500;
                    strafing = 1000;
                    waypointreached = true;
                    Debug.Log("PING");
                }
                break;
            case 1:
                //hunt player
                MR.material = changeColor[2];
                strafing = 3000;
                forward = 750;
                mover.destination = player.transform.position;
                break;

        }
    }

    void CheckState()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= 7)
        {
            state = 1;
            Debug.Log("chase mode");
            /*if (Vector3.Distance(this.transform.position, player.transform.position) <= 1)
            {
                player.SetActive(false);
            }*/
        }
        else
        {
            state = 0;
            Debug.Log("patrol mode");
        }
    }

    void FindPatrolPoints()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("waypoint"))
        {
            waypoints.Add(g);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            other.gameObject.SetActive(false);
            state = 0;
        }
        
    }

    void OnCollisionExit(Collision other)
    {
        
       /* if (other.collider.tag == "Player")
        {
            strafing = 500;
        }*/
    }
}
