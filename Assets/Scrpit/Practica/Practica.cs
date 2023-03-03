using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practica : MonoBehaviour
{
    // Start is called before the first frame update

    /*
        Crear 3 stados el de patrolling perseguir y atacar

        using UnityEngine.AI;

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


    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Start() 
    {
        currentState = State.Patrolling;

        destinationIndex = Random.Range(0, destinationPoints.Length); 
    }


    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
            break;

            break;
            case State.Chasing:
                Chase();
            break;
            case State.Attacking:
                Attack();
            break;
            default:
                Patrol();
            break;
        }
    }

    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if(Vector3.Distance(transform.position, destinationPoints[destinationIndex].position) < 1f)
        {
            destinationIndex = Random.Range(0, destinationPoints.Length);
        }

        if(DistanceToTarget(visionRange))
        {
            currentState = State.Chasing;
        }
    }

    void Chase()
    {

        agent.destination = player.position;
        if(DistanceToTarget(visionRange) == false)
        {
            currentState = State.Patrolling;
        }
        
        if(DistanceToTarget(hitRange))
        {
            currentState = State.Attacking;
        }

    }

    void Attack()
    {
        Debug.Log("Patada en la boca");
        
        if(!DistanceToTarget(hitRange))
        {
            currentState = State.Chasing;
        }
        
    }


    bool AttackRange()
        {
            if(Vector3.Distance(transform.position, player.position) < hitRange)
            {
                return true;
            }

            return false;
        }

    bool FindTarget()
    {
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            return true;
        }

        return false;
    
    }



    bool DistanceToTarget(float distance)
    {
        if(Vector3.Distance(transform.position, player.position) < distance)
        {
            return true;
        }

        return false;
    
    }

    */
}
