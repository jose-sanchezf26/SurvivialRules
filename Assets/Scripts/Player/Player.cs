using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Extensions;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    // Evento para ventana de muerte
    [HideInInspector]
    public UnityEvent PlayerDeath;

    float timeO = 0f;
    float interval = 2f;
    // Componente para Pathfinding
    private NavMeshAgent agent;

    // Target utilizado para seguimiento
    [HideInInspector]
    public string target;

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
    // Tipo de ataque
    public AttackType attackType;
    // Inventario
    private Inventory inventory;
    // Componente para cambiar de color cuando le golpean
    private ChangeHitColor changeHitColor;

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
        animator = GetComponent<Animator>();
        attack = GetComponent<Attack>();
        ChangeAttackType(AttackType.None);
        ChangeDamage(5);
        inventory = FindAnyObjectByType<Inventory>();
        changeHitColor = GetComponent<ChangeHitColor>();

        Health = maxLevelProperties;
        Hunger = maxLevelProperties;
        Thirst = maxLevelProperties;
        Tiredness = maxLevelProperties;

        lastPosition = transform.position;

        // Busca el pozo más cercano cada x segundos, por temas de optimización
        InvokeRepeating("NearestObjects", 0f, 10f);

        ShowInformation();
    }

    public bool ItemInInventory(string item)
    {
        return inventory.HasItem(item);
    }

    // Métodos para incluir información en la base de hechos


    // Método para actualizar la lista de objetos detectados
    public List<DetectableName> NamesDetected()
    {
        return detector.DetectableNames();
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
    [HideInInspector]
    public Well well;

    // Es la hoguera más cercana 
    [HideInInspector]
    public Campfire campfire;

    // Es la cabaña más cercana
    public Cabage cabage;

    public void NearestObjects()
    {
        Well[] wells = FindObjectsOfType<Well>();
        Campfire[] campfires = FindObjectsOfType<Campfire>();
        Cabage[] cabages = FindObjectsOfType<Cabage>();

        float bestDistanceW = Mathf.Infinity;
        float bestDistanceC = Mathf.Infinity;
        float bestDistanceCa = Mathf.Infinity;
        Well nearWell;
        Campfire nearCampfire;
        Cabage nearCabage;

        foreach (Well well in wells)
        {
            float actualDistanceW = Vector2.Distance(transform.position, well.transform.position);

            if (actualDistanceW < bestDistanceW)
            {
                bestDistanceW = actualDistanceW;
                nearWell = well;
                this.well = well;
            }
        }
        foreach (Campfire campfire in campfires)
        {
            float actualDistanceC = Vector2.Distance(transform.position, campfire.transform.position);

            if (actualDistanceC < bestDistanceC)
            {
                bestDistanceC = actualDistanceC;
                nearCampfire = campfire;
                this.campfire = campfire;
            }
        }
        foreach (Cabage cabage in cabages)
        {
            float actualDistanceCa = Vector2.Distance(transform.position, cabage.transform.position);

            if (actualDistanceCa < bestDistanceCa)
            {
                bestDistanceCa = actualDistanceCa;
                nearCabage = cabage;
                this.cabage = cabage;
            }
        }
    }

    // Método para cocinar en una hoguera
    public void Cook()
    {
        if (campfire.itemToCook != null && Vector2.Distance(transform.position, campfire.transform.position) < campfire.cookDistance && inventory.HasItemData(campfire.itemToCook))
        {
            CancelMovement();
            inventory.Remove(campfire.itemToCook);
            campfire.Delay();
            campfire.Cook();
        }
    }

    // Método para beber en un pozo
    public void Drink()
    {
        if (well != null)
        {
            if (well.Drink(transform.position))
            {
                Thirst += drinkAmount;
            }
            // ShowInformation();
        }
    }

    // Campo para el modo oculto, para evitar que te detecten los enemigos
    private bool hidden = false;
    public bool Hidden
    {
        get { return hidden; }
        set { hidden = value; }
    }


    // Método para descansar
    public void Rest()
    {
        if (Vector2.Distance(transform.position, cabage.transform.position) <= cabage.restDistance)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            hidden = true;
            CancelMovement();

            StartCoroutine(IncreaseTirednessOverTime(2f));
        }
    }

    // Método que hace que incremente el nivel de cansancio cada x segundos
    IEnumerator IncreaseTirednessOverTime(float s)
    {
        while (Tiredness < maxLevelProperties)
        {
            Tiredness += 5;
            yield return new WaitForSeconds(s);
        }

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        hidden = false;
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
                Tiredness -= 4;
            }
            else
            {
                Tiredness += 1;
            }
            timeOT = 0f;
            lastPositionT = transform.position;
        }
    }

    private bool isDie = false;
    // Simula el paso del tiempo para los atributos del jugador
    public void TimePassage()
    {
        Hunger -= 2;
        Thirst -= 2;
        //TODO Parte para detectar que ha muerto
        if (Health <= 0)
        {
            Die();
        }
        if (Hunger > 80 && Thirst < 80)
        {
            Heal(1);
        }
        if (Hunger < 20 && Thirst < 20)
        {
            TakeDamage(1);
        }
        if (Hunger == 0 || Thirst == 0)
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

    // Método para la muerte del jugador
    private void Die()
    {
        Restart(false);
        isDie = true;
        PlayerDeath.Invoke();
    }

    // Método para quitar vida
    public void TakeDamage(int amount)
    {
        Health -= amount;
        changeHitColor.isHit = true;
    }

    // Método para curar vida
    public void Heal(int amount)
    {
        Health += amount;
    }

    // Método para cambiar el tipo de daño
    public void ChangeAttackType(AttackType type)
    {
        attackType = type;
    }


    // Potencias de daño según el arma equipada
    public int attackDamage = 10;
    // Método para modificar la potencia de daño
    public void ChangeDamage(int newDamage)
    {
        attack.damage = newDamage;
    }

    public UnityEngine.UI.Image itemIcon;
    public TextMeshProUGUI itemName;

    [HideInInspector]
    public ItemData equippedItem;

    // Método para equiparse un objeto
    public void EquipObject(string item)
    {
        ItemData itemData = inventory.GetItemData(item);
        if (itemData != null)
        {
            equippedItem = itemData;
            itemName.text = itemData.displayName;
            itemIcon.sprite = itemData.icon;

            // Según el objeto equipado se modifica el tipo de daño
            if (item == "Axe" && attackType == AttackType.Cut)
            {
                ChangeDamage(30);
            }
            if (item == "Pickaxe" && attackType == AttackType.Mine)
            {
                ChangeDamage(30);
            }
            if (item == "Sword" && attackType == AttackType.Enemy)
            {
                ChangeDamage(30);
            }
        }
    }

    // Establece un objetivo para el Path Finding
    public void SetTarget(Vector2 position)
    {
        explore.SetActive(false);
        flee.SetActive(false);
        agent.SetDestination(position);
    }

    // Activa el modo explorar
    public void Explore()
    {
        agent.ResetPath();
        if (agent.destination == null) { Debug.Log("el path ha sido borrado"); }
        flee.SetActive(false);
        explore.SetActive(true);
    }

    // Método para huir de enemigos
    public void Flee(Transform enemy)
    {
        agent.ResetPath();
        explore.SetActive(false);
        flee.SetActive(true);
        flee.enemy = enemy;
    }

    // Método que cancela el movimiento del personaje
    public void CancelMovement()
    {
        agent.ResetPath();
        flee.SetActive(false);
        explore.SetActive(false);
    }

    // Método para establecer las características del personaje
    public void Restart(bool attributes)
    {
        if (attributes)
        {
            Health = maxLevelProperties;
            Thirst = maxLevelProperties;
            Tiredness = maxLevelProperties;
            Hunger = maxLevelProperties;
        }
        CancelMovement();
        attackType = AttackType.None;
    }

    IEnumerator Stay()
    {
        CancelMovement();
        yield return new WaitForSeconds(2);
    }

    // Método que devuelve string que indica el movimiento que se está realizando
    public string GetMovement()
    {
        string result = "";
        if (explore.active)
        {
            result = "Explore";
        }
        else if (flee.active)
        {
            result = "Flee";
        }
        else if (agent.destination != null)
        {
            result = "Target " + target;
        }
        result += "\n";
        return result;
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
        // float direction = Mathf.Sign(currentPosition.x - lastPosition.x);
        // if (direction > 0)
        // {
        //     transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        // }
        // if (direction < 0)
        // {
        //     transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        // }

        lastPosition = currentPosition;
    }

    // Prefabs para poder construir
    public GameObject campfirePrefab;
    public GameObject cabagePrefab;

    // Comprueba si se puede construir una estructura a un lado del jugador, y en ese caso la coloca en dicha posición
    public bool Build(GameObject structure, float checkRadius)
    {
        if (structure != null)
        {
            Vector2 spawn = new Vector2(transform.position.x - 3, transform.position.y);

            // Comprueba que no hay colisiones con otros objetos
            if (Physics.OverlapSphere(spawn, checkRadius).Length == 0)
            {
                if (structure == campfirePrefab)
                {
                    campfire = Instantiate(structure, spawn, Quaternion.identity).GetComponent<Campfire>();
                }
                else
                {
                    cabage = Instantiate(structure, spawn, Quaternion.identity).GetComponent<Cabage>();
                }
                return true;
            }
        }
        return false;
    }

    // Función que comprueba si se coloca una hoguera o refugio mediante la presión de teclas
    public void Controls()
    {
        // Si presionas C se creará una hoguera cerca del jugador
        if (Input.GetKeyDown(KeyCode.C) && inventory.HasItemData(inventory.campfireData))
        {
            if (Build(campfirePrefab, 3f))
            {
                inventory.Remove(inventory.campfireData);
            }
        }
        // Si presionas C se creará una hoguera cerca del jugador
        if (Input.GetKeyDown(KeyCode.V) && inventory.HasItemData(inventory.cabageData))
        {
            if (Build(cabagePrefab, 3f))
            {
                inventory.Remove(inventory.cabageData);
            }
        }
    }

    void Update()
    {
        ManualControl();

        timeO += Time.deltaTime;
        if (timeO >= interval)
        {
            if (!isDie)
            {
                TimePassage();
            }
            timeO = 0f;
        }
        CalculeTiredness();

        // detector.DetectObjectsInVision();
        attack.DoAttack("Health", attackType);
        Controls();
        Movement();
    }
}
