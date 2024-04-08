using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Extensions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    float timeO = 0f;
    float interval = 2f;
    // Componente para Pathfinding
    private NavMeshAgent agent;
    // Target utilizado para seguimiento
    [SerializeField] Transform target;

    // Propiedades del agente
    private float health;
    private float hunger;
    private float thirst;
    private float tiredness;

    // Cantidad a la hora de beber en un pozo
    public float drinkAmount = 20;

    // Valor inicial de las propiedades del jugador
    public int maxLevelProperties = 100;
    // Guarda el componente encargado de detectar objetos
    public Detector detector;
    // Guarda el componente encargado de explorar
    private Explore explore;
    // Componente encargador de hacer huir al jugador dada la posición de un enemigo
    private Flee flee;
    // Animación del jugador
    private Animator animator;
    // Ataque
    private Attack attack;
    // Inventario
    private Inventory inventory;

    // Posición anterior para detectar si hay movimiento
    private Vector2 lastPosition;
    private Vector2 currentPosition;

    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100); }
    }

    public float Hunger
    {
        get { return hunger; }
        set { hunger = Mathf.Clamp(value, 0, 100); }
    }

    public float Thirst
    {
        get { return thirst; }
        set { thirst = Mathf.Clamp(value, 0, 100); }
    }

    public float Tiredness
    {
        get { return tiredness; }
        set { tiredness = Mathf.Clamp(value, 0, 100); }
    }

    public float Speed
    {
        get { return agent.speed; }
        set { agent.speed = value; }
    }

    // Inicialización
    void Start()
    {
        //Evita que el jugador rote
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Obtiene los componentes
        detector = GetComponent<Detector>();
        explore = GetComponent<Explore>();
        flee = GetComponent<Flee>();
        flee.enabled = false;
        animator = GetComponent<Animator>();
        attack = GetComponent<Attack>();
        inventory = FindAnyObjectByType<Inventory>();

        Health = maxLevelProperties;
        Hunger = maxLevelProperties;
        Thirst = maxLevelProperties;
        Tiredness = maxLevelProperties;

        lastPosition = transform.position;

        // Busca el pozo más cercano cada x segundos, por temas de optimización
        InvokeRepeating("NearestWell", 0f, 10f);

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
        transform.Translate(movement * Speed * Time.deltaTime);
        // if (Input.GetKey(KeyCode.W)) { Explore(); }
        // if (Input.GetKey(KeyCode.S)) { SetTarget(new Vector2(0, 0)); }
    }

    public void Eat(string item)
    {
        ItemData itemData = inventory.GetItemData(item);
        if (itemData != null)
        {
            Stay();
            hunger += itemData.hungerheal;
            inventory.Remove(itemData);
        }

        ShowInformation();
    }

    // Es el pozo que tiene asignado en ese momento
    public Well well;
    public void NearestWell()
    {
        Well[] wells = FindObjectsOfType<Well>();

        float bestDistance = Mathf.Infinity;
        Well nearWell = new Well();

        foreach (Well well in wells)
        {
            float actualDistance = Vector2.Distance(transform.position, well.transform.position);

            if (actualDistance < bestDistance)
            {
                bestDistance = actualDistance;
                nearWell = well;
            }
        }

        well = nearWell;
    }

    public void Drink()
    {
        if (well.Drink(transform.position))
        {
            Thirst += drinkAmount;
        }
        // ShowInformation();
    }

    public void Rest(int amount)
    {
        Tiredness -= amount;

        ShowInformation();
    }

    Vector2 lastPositionT;
    //Distancia para el cansancio;
    public float distanceTiredness = 2f;
    float intervalT = 2f;
    float timeOT = 0;

    // Calcula el cansancio del jugador en función de la distancia recorrida en x segundos
    private void CalculeTiredness()
    {
        timeOT += Time.deltaTime;
        if (timeOT >= intervalT)
        {
            if (distanceTiredness < Vector2.Distance(lastPositionT, transform.position))
            {
                Tiredness -= 1;
            }
            else
            {
                Tiredness += 2;
            }
            timeOT = 0f;
            lastPositionT = transform.position;
        }
    }

    // Simula el paso del tiempo para los atributos del jugador
    public void TimePassage()
    {
        Hunger -= 2;
        Thirst -= 2;
        if (Hunger < 20 && Thirst < 20)
        {
            TakeDamage(1);
        }
        if (Tiredness < 50)
        {
            Speed = 4f;
            if (Tiredness < 25)
            {
                Speed = 2.5f;
            }
        }
        else
        {
            Speed = 5f;
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
    }

    public void SetTarget(Vector2 position)
    {
        explore.SetActive(false);
        agent.SetDestination(position);
    }

    public void Explore()
    {
        agent.ResetPath();
        flee.enabled = false;
        explore.SetActive(true);
    }

    public void Flee(Transform enemy)
    {
        agent.ResetPath();
        flee.enabled = true;
        flee.enemy = enemy;
        explore.SetActive(false);
    }

    public void CancelMovement()
    {
        agent.ResetPath();
        flee.enabled = false;
        explore.SetActive(false);
    }

    IEnumerator Stay()
    {
        CancelMovement();
        yield return new WaitForSeconds(2);
    }

    private void Movement()
    {
        //Se asigna la velocidad del personaje a los componentes de movimiento
        explore.moveSpeed = Speed;
        flee.speed = Speed;

        //Se detecta si hay movimiento y aplica efecto de cansancio
        currentPosition = transform.position;
        if (lastPosition != currentPosition)
        {
            animator.SetFloat("Movement", 1);
        }
        else
        {
            animator.SetFloat("Movement", 0);
        }

        // Calcula la dirección del movimiento
        float direction = Mathf.Sign(currentPosition.x - lastPosition.x);
        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        lastPosition = currentPosition;
    }

    void Update()
    {
        ManualControl();

        timeO += Time.deltaTime;
        if (timeO >= interval)
        {
            TimePassage();
            timeO = 0f;
        }
        CalculeTiredness();

        // detector.DetectObjectsInVision();
        attack.DoAttack("Health");
        Movement();
    }
}
