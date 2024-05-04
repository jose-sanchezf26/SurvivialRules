using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;


public class BE2_Cst_Cook : BE2_InstructionBase, I_BE2_Instruction
{

    public new bool ExecuteInUpdate => true;
    public new void Function()
    {
        TargetObject.Player.Cook();
        if (!TargetObject.Player.isCooking)
        {
            ExecuteNextInstruction();
        }
    }

    public new string Operation()
    {
        string result = "";

        return result;
    }

}
