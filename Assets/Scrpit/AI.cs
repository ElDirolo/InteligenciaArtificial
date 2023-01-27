using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    //hacer un estado de waiting despues de que nos persiga
   
    //estado de atacando si se acerca mucho al jugador que salga un debug donde salga que le ha atacado y que cuando sale que se vuelva al de perseguir

   
    enum State
    {
        Patrolling,
        Chasing,
        Attacking,
    }


    State currentState;
    NavMeshAgent agent;
    public int destinationIndex = 0;

    public Transform[] destinationPoints;
    public float visionRange;
    public float hitRange;
    public Transform player;

    public float waitTime;
    public float startWaitTime;


    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start() 
    {
        currentState = State.Patrolling;

        waitTime = startWaitTime;

        destinationIndex = (destinationIndex + 1) % destinationPoints.Length;


    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
            break;
            case State.Chasing:
                Chase();
            break;
            default:
                Chase();
            break;
        }
    }


    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if(Vector3.Distance(transform.position, destinationPoints[destinationIndex].position) < 1)
        {
            if(waitTime <= 0)
            {

                destinationIndex = (destinationIndex + 1) % destinationPoints.Length;
                waitTime =startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }
// > cuando no este en el rango                < cuando este en el area
    void Chase()
    {
        agent.destination = player.position;
        
        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }

        if(Vector3.Distance(transform.position, player.position) < hitRange)
        {
            Debug.Log("PIto");
            
        }
    }

    /*void Ataking()
    {
        agent.destination = player.position;
    }*/

    void OnDrawGizmos() 
    {
        foreach (Transform point in destinationPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
