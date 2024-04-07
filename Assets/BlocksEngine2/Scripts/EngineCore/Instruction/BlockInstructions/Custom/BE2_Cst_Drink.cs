using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;
using Unity.Mathematics;


public class BE2_Cst_Drink : BE2_InstructionBase, I_BE2_Instruction
{
    public new void Function()
    {
        TargetObject.Player.Drink();
        ExecuteNextInstruction();
    }

    public new string Operation()
    {
        string result = "";

        return result;
    }
}
