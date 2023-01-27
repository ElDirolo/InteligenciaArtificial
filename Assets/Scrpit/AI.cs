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
        WaitingChase,
        Attacking,
    }


    State currentState;
    NavMeshAgent agent;
    public int destinationIndex = 0;

    public Transform[] destinationPoints;
    public float visionRange;
    public float hitRange;
    public Transform player;

    public float startWaitTime;
    private float waitTime;
    
    public float startWaitChaseTime;
    private float waitChaseTime;
    

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start() 
    {
        currentState = State.Patrolling;

        destinationIndex = (destinationIndex + 1) % destinationPoints.Length;
        
        waitTime = startWaitTime;

        waitChaseTime = startWaitChaseTime;
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
            case State.WaitingChase:
                WaitChaise();
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
            waitTime = startWaitTime;
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

    void WaitChaise()
    {
        agent.destination = transform.position;
        if(waitChaseTime <= 0)
        {
            waitChaseTime = startWaitChaseTime;
            currentState = State.Patrolling;
        }
        else
        {
            waitChaseTime -= Time.deltaTime;
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
            currentState = State.WaitingChase;
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
