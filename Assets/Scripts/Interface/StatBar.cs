using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public UnityEngine.UI.Image health;
    public UnityEngine.UI.Image hunger;
    public UnityEngine.UI.Image tiredness;
    public UnityEngine.UI.Image thirst;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        health.fillAmount = player.Health / 100;
        hunger.fillAmount = player.Hunger / 100;
        tiredness.fillAmount = player.Tiredness / 100;
        thirst.fillAmount = player.Thirst / 100;
    }
}
