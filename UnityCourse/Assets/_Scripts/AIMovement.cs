using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
    
    public NavMeshAgent agent;
    private GameObject player;

    void Update() {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) {
            agent.Stop();
            return;
        }
        
        agent.SetDestination(player.GetComponent<Transform>().position);
    }
}