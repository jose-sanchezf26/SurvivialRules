using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- most used BE2 namespaces for instruction scripts 
using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using UnityEngine.UIElements;

// --- additional BE2 namespaces used for specific cases as accessing BE2 variables or the event manager
// using MG_BlocksEngine2.Core;
// using MG_BlocksEngine2.Environment;

public class BE2_Cst_SetTarget : BE2_InstructionBase, I_BE2_Instruction
{
    I_BE2_BlockSectionHeaderInput _input0;
    BE2_InputValues _v0;

    public new void Function()
    {
        // Obtiene el valor del input del bloque
        _input0 = Section0Inputs[0];
        _v0 = _input0.InputValues;

        Vector2 targetPosition = new Vector2();
        if (_v0.isText)
        {
            //Posibles objetivos para establecer como target del jugador
            switch (_v0.stringValue)
            {
                case "Well":
                    targetPosition = TargetObject.Player.well.transform.position;
                    break;
                case "Campfire":
                    targetPosition = TargetObject.Player.campfire.transform.position;
                    break;
                default:
                    targetPosition = TargetObject.Player.detector.DetectedPosition(_v0.stringValue);
                    break;
            }
        }

        //Comprueba que se haya asignado algún target
        if (targetPosition != Vector2.zero)
        {
            TargetObject.Player.SetTarget(targetPosition);
        }
        ExecuteNextInstruction();
    }

    public new string Operation()
    {
        string result = "";

        return result;
    }
}
