using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    // El prefab del agente que se genera
    public GameObject agentPrefab;
    public GameObject actualAgent;
    // El tiempo de generación después de su muerte
    public float timeToSpawn;
    // La dificultad en la que empieza a spawnear enemigos, hecho para la dificultad
    public int difficultyToActive = 1;
    private bool active = false;
    private bool waitForSpawn = false;

    void Start()
    {
        if (difficultyToActive <= DifficultyManager.Instance.currentDifficulty)
        {
            GeneratePrefab();
        }
    }

    void Update()
    {
        if (!active) return;
        // Comprueba que el agente siga vivo y en el caso contrario espera x segundos y lo vuelve a crear
        if (actualAgent == null && !waitForSpawn && difficultyToActive <= DifficultyManager.Instance.currentDifficulty)
        {
            waitForSpawn = true;
            StartCoroutine(WaitForSpawn(timeToSpawn));
        }
    }

    private IEnumerator WaitForSpawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GeneratePrefab();
        waitForSpawn = false;
    }

    private void GeneratePrefab()
    {
        if (agentPrefab != null)
        {
            actualAgent = Instantiate(agentPrefab, transform.position, Quaternion.identity);

            // En el caso de que sea un enemigo, es necesario establecer un objetivo al que atacar
            Enemy enemy = actualAgent.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.target = FindAnyObjectByType<Player>().gameObject;
                NavMeshAgent agent = actualAgent.GetComponent<NavMeshAgent>();
                // agent.Warp(transform.position);
            }

        }
    }
}
