using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Enacts a battler's action during the animation.
/// </summary>
public class RunActBehavior : IBatBehavior
{
    private BattleAction action;    //the action to be ran
    private bool used = false;      //whether the action has been used

    /// <summary>
    /// Constructs an instruction for a battler to perform a BattleAction.
    /// </summary>
    /// <param name="actn">BattleAction to be performed.</param>
    public RunActBehavior(BattleAction actn)
    {
        action = actn;
    }
    
    public void act()
    {
        action.act();
        used = true;
    }

    public bool isDone()
    {
        return used;
    }

}
