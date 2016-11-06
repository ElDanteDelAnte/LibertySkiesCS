using UnityEngine;
using System.Collections.Generic;

public abstract class BattleAction
{
    protected Battler user;
    protected List<Battler> targets;

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
    public abstract void act();
    
        //soft-pause battle progression (not all cases)
        //deduct stam/mana/etc. cost from user

        //do damage/heal damage/etc.
        //resume battle progression
    

    /// <summary>
    /// Obtains a list of valid targets for the action for the Battler to select from.
    /// </summary>
    /// <param name="allied">Whether the would-be user is on the player's team.</param>
    /// <returns>Valid targets.</returns>
    public static List<Battler> getValidTargets(bool allied)
    {
        return new List<Battler>();
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
