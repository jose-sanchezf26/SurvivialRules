using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int Health = 100;
    public float chaseSpeed = 3f;
    public float exploreSpeed = 3f;
    public GameObject target;
    public float ditanceToFollow;
    public float detectDistance;
    private AIChase aiChase;
    private Explore explore;
    private Attack attack;



    void Start()
    {
        aiChase = GetComponent<AIChase>();
        explore = GetComponent<Explore>();
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        // Seguimiento del jugador y movimiento
        if (Vector2.Distance(transform.position, target.transform.position) <= ditanceToFollow && !target.GetComponent<Player>().Hidden)
        {
            explore.SetActive(false);
            aiChase.enabled = true;
            aiChase.SetSpeed(chaseSpeed);
            aiChase.targetPosition = target.transform.position;
        }
        else
        {
            explore.SetActive(true);
            explore.SetSpeed(exploreSpeed);
            aiChase.enabled = false;
        }

        // Ataque
        attack.DoAttack("Player", AttackType.Enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ditanceToFollow);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
