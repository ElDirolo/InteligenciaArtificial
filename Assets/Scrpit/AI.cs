using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    //hacer un estado de waiting en este estado es cuando llege a un punto se tiene que estar alli paradao durante unos segundos y luego pasar l siguiente y repetir
    //Que coga los puntos por orden
    //estado de atacando si se acerca mucho al jugador que salga un debug donde salga que le ha atacado y que cuando sale que se vuelva al de perseguir

    //Cambiar que en vez de que lo haga de una manera ranmdom lo haga en orden segun el grupo y cuando llege al ultimo que vuelva al primero y cuando llegue al punto hacer una funcion que sea que tenga que estar en el llugar durante  5 segundo y si durante ese tiempo pasa el jugador que lo persigua
    enum State
    {
        Patrolling,
        Chasing,
        Waiting,
        Ataking,
    }


    State currentState;
    NavMeshAgent agent;
    int destinationIndex = 0;

    public Transform[] destinationPoints;
    public float visionRange;
    public Transform player;
    // Start is called before the first frame update

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
            default:
                Chase();
            break;
        }
    }

    // Update is called once per frame
    void Patrol()
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
