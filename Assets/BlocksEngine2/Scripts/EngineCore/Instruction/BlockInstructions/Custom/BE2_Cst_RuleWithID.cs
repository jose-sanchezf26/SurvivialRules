using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;


public class BE2_Cst_RuleWithID : BE2_InstructionBase, I_BE2_Instruction
{
    public UnityEngine.UI.Image executionImage;
    I_BE2_BlockSectionHeaderInput _input0;
    I_BE2_BlockSectionHeaderInput _input1;
    string _value;
    bool _isFirstPlay = true;
    float startTime = 0;
    float minutes;
    float seconds;

    // void Start()
    // {
    //     startTime = Time.time;
    // }

    protected override void OnButtonStop()
    {
        _isFirstPlay = true;
    }

    public override void OnStackActive()
    {
        _isFirstPlay = true;
    }

    public new void Function()
    {
        if (_isFirstPlay)
        {
            _input0 = Section0Inputs[0];
            _input1 = Section0Inputs[1];
            _value = _input0.StringValue;

            if (_value == "1" || _value == "true")
            {
                // _isFirstPlay = false;
                executionImage.gameObject.SetActive(true);
                TargetObject.Player.actualRulesText = /*+ TimeTracker()*/ "Rule : " + _input1.StringValue + "\n\n";
                ExecuteSection(0);
                if (TargetObject.Player.actualRulesText != TargetObject.Player.lastRulesText)
                {
                    TargetObject.Player.rulesText.text += TargetObject.Player.actualRulesText;
                    TargetObject.Player.lastRulesText = TargetObject.Player.actualRulesText;
                }
                TargetObject.Player.actualRulesText = "";
            }
            else
            {
                _isFirstPlay = true;
                executionImage.gameObject.SetActive(false);
                ExecuteNextInstruction();
            }
        }
        else
        {
            _isFirstPlay = true;
            // ExecuteNextInstruction();
        }
    }

    public string TimeTracker()
    {
        // Calcula el tiempo transcurrido desde el inicio del juego
        float elapsedTime = Time.time - startTime;

        // Formatea el tiempo en minutos y segundos
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Actualiza el texto del temporizador
        return string.Format("[{0:00}:{1:00}] ", minutes, seconds);
    }

    // v2.12 - added Reset method to the instructions to enable reuse by Function Blocks
    public new void Reset()
    {
        _isFirstPlay = true;
    }

}
