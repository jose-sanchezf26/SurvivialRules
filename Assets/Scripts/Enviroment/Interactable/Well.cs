using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, IInteractable
{
    
    public int WaterLevel {get; private set;}
    public int MaxWaterLevel = 5;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = MaxWaterLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformAction()
    {
        if(WaterLevel > 0)
        {
            WaterLevel--;
        }
    }

    public bool Drink()
    {
        if(WaterLevel > 0)
        {
            WaterLevel--;
            return true;
        }
        return false;
    }

}
