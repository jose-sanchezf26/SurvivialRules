using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int Health = 100;
    public float speed = 5f;
    public GameObject target;
    public float ditanceToFollow;
    public float detectDistance;
    public AIChase aiChase;
    public Explore explore;


    void Start()
    {
        aiChase = GetComponent<AIChase>();
        explore = GetComponent<Explore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= ditanceToFollow)
        {
            explore.SetActive(false);
            aiChase.enabled = true;
            aiChase.targetPosition = target.transform.position;
        }
        else
        {
            explore.SetActive(true);
            aiChase.enabled = false;
        }
    }
}
