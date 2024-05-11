using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MG_BlocksEngine2.Block.Instruction;
using MG_BlocksEngine2.Block;


public class BE2_Cst_Rules : BE2_InstructionBase, I_BE2_Instruction
{
    public new void Function()
    {
        if (!TargetObject.Player.agent.hasPath) { ExecuteSection(0); }

        Debug.Log("Ya ha terminado de ejecutarse");
    }
}
