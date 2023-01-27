using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiClase : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Waiting,
        Ataking,
        Travelling,
    }


    State currentState;
    NavMeshAgent agent;
    int destinationIndex = 0;

    public Transform[] destinationPoints;
    public float visionRange;
    public Transform player;

    [SerializeField] float patrolRange = 10f;


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
            case State.Chasing:
                Chase();
            break;
            case State.Travelling:
                Travel();
            break;
            default:
                Chase();
            break;
        }
    }

    // Update is called once per frame
    /*void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if(Vector3.Distance(transform.position, destinationPoints[destinationIndex].position) < 1)
        {
            destinationIndex = Random.Range(0, destinationPoints.Length);
        }
        
        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }*/

    void Patrol()
    {

        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentState = State.Chasing;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 point)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 4, NavMesh.AllAreas))
        {
            point = hit.position;
            return true;
        }
        point = Vector3.zero;
        return false;
    
    }
    void Travel()
    {

    }
    void Chase()
    {
        agent.destination = player.position;
        
        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }

    }

    void OnDrawGizmos() 
    {
        foreach (Transform point in destinationPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
