using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class Well : MonoBehaviour, IInteractable
{

    public int WaterLevel { get; private set; }
    public int MaxWaterLevel = 100;
    public float drinkDistance = 1;
    public float timeToDrink = 3f;
    public float lastDrink;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = MaxWaterLevel;
        lastDrink = -timeToDrink;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PerformAction()
    {
        if (WaterLevel > 0)
        {
            WaterLevel--;
        }
    }

    public bool Drink(Vector2 playerPosition)
    {
        float actualTime = Time.time;

        if (WaterLevel > 0 && Vector2.Distance(transform.position, playerPosition) < drinkDistance && (actualTime - lastDrink >= timeToDrink))
        {
            WaterLevel--;
            lastDrink = actualTime;
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, drinkDistance);
    }

}
