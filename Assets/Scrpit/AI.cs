using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
// > cuando no este en el rango                < cuando este en el area
   //Intentar que se quede quieto despues de perseguir
    enum State
    {
        Patrolling,
        Waiting,
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
            case State.Waiting:
                Wait();
            break;
            case State.Chasing:
                Chase();
            break;
            case State.Attacking:
                Attack();
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
            currentState = State.Waiting;
            /*if(waitTime <= 0)
            {
                destinationIndex = (destinationIndex + 1) % destinationPoints.Length;
                waitTime =startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }*/
        }
        
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }

    void Wait()
    {
        agent.destination = destinationPoints[destinationIndex].position;
        if(waitTime <= 0)
        {
            destinationIndex = (destinationIndex + 1) % destinationPoints.Length;
            waitTime =startWaitTime;
            currentState = State.Patrolling;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }
    void Chase()
    {
        agent.destination = player.position;
        
        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }

        if(Vector3.Distance(transform.position, player.position) < hitRange)
        {
            currentState = State.Attacking;
        }
    }

    void Attack()
    {
        agent.destination = player.position;
        
        Debug.Log("Patada en la boca");
        
        
        if(Vector3.Distance(transform.position, player.position) > hitRange)
        {
            currentState = State.Chasing;
        }
        
    }

    void OnDrawGizmos() 
    {
        foreach (Transform point in destinationPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1);
        }
        
        //Rango de persecucion
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        
        //Rango de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}
