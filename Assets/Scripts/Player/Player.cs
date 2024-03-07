using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed = 5f; 
    // Componente para Pathfinding
    NavMeshAgent agent;
    // Target utilizado para seguimiento
    [SerializeField] Transform target;

    // Propiedades del agente
    private int health;
    private int hunger;
    private int thirst;
    public int drinkAmount = 10;
    private int tiredness;
    public int maxLevelProperties = 100;

    public float visionRadius = 5f;
    public int raysCount = 100; // Número de rayos para simular el círculo
    public LayerMask detectionLayer;


    public int Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100); } // Ensure health is within the range [0, 100]
    }

    public int Hunger
    {
        get { return hunger; }
        set { hunger = Mathf.Clamp(value, 0, 100); } // Ensure hunger is within the range [0, 100]
    }

    public int Thirst
    {
        get { return thirst; }
        set { thirst = Mathf.Clamp(value, 0, 100); } // Ensure thirst is within the range [0, 100]
    }

    public int Tiredness
    {
        get { return tiredness; }
        set { tiredness = Mathf.Clamp(value, 0, 100); } // Ensure tiredness is within the range [0, 100]
    }

    // Inicialización
    void Start()
    {
        //Evita que el jugador rote
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Health = maxLevelProperties;
        Hunger = maxLevelProperties;
        Thirst = maxLevelProperties;
        Tiredness = maxLevelProperties;

        ShowInformation();
    }


    void ShowInformation()
    {
        Debug.Log("Health: " + Health);
        Debug.Log("Hunger: " + Hunger);
        Debug.Log("Thirst: " + Thirst);
        Debug.Log("Tiredness: " + Tiredness);
    }

    private void ManualControl()
    {
        // Obtener la entrada del teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular la dirección del movimiento
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        // Mover el jugador en la dirección calculada
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void PathFinding()
    {

        agent.SetDestination(target.position);
    }

    public void Eat(int amount)
    {
        Hunger -= amount;

        ShowInformation();
    }

    public void Drink(Well well)
    {
        if(well.Drink()){
            Thirst += drinkAmount;
        }
        ShowInformation();
    }

    public void Rest(int amount)
    {
        Tiredness -= amount;

        ShowInformation();
    }

    public void TimePassage()
    {
        Hunger -= 1;
        Thirst -= 1;
        Tiredness    -= 1;
    }

    public void SetTarget(Vector2 position)
    {
        agent.SetDestination(position);
    }

    void DetectObjectsInVision()
    {
        float angleIncrement = 360f / raysCount; // Incremento angular entre rayos

        for (int i = 0; i < raysCount; i++)
        {
            float angle = i * angleIncrement;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionRadius, detectionLayer);

            if (hit.collider != null)
            {
                GameObject detectedObject = hit.collider.gameObject;
                Debug.Log("Se ha detectado un objeto");
                // InteractableObject interactableObject = detectedObject.GetComponent<InteractableObject>();
                // if (interactableObject != null)
                // {
                //     interactableObject.Interact();
                // }
            }
        }
    }

    // Método para visualizar el campo de visión en la escena de Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float angleIncrement = 360f / raysCount;

        for (int i = 0; i < raysCount; i++)
        {
            float angle = i * angleIncrement;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * visionRadius);
        }
    }           

    void Update()
    {
        // ManualControl();
        if(Time.time % 1 == 0)
        {
            TimePassage();
        }
        DetectObjectsInVision();
    }
}
