using UnityEngine;
using System.Collections.Generic;

public abstract class BattleAction
{
    private Battler user;
    private List<Battler> targets;

    /// <summary>
    /// Constructor for targetless action.
    /// </summary>
    /// <param name="user">The source of the action.</param>
    public BattleAction(Battler use)
    {
        user = use;
    }

    /// <summary>
    /// Constructor for a targeted action.
    /// </summary>
    /// <param name="use">The source of the action.</param>
    /// <param name="target">All targets of the action.</param>
    public BattleAction(Battler use, List<Battler> target)
    {
        user = use;
        targets = target;
    }

    /// <summary>
    /// Causes the action to take effect.
    /// </summary>
    public void act()
    {
        //soft-pause battle progression (not all cases)
        //deduct stam/mana/etc. cost from user

        //do damage/heal damage/etc.
        //resume battle progression
    }

    /// <summary>
    /// Checks to see if the conditions to use the condition are met.
    /// </summary>
    /// <returns>True if the action is valid, false otherwise.</returns>
    public bool isValid()
    {
        return true;
    }
}
