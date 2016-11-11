using UnityEngine;
using System.Collections.Generic;

public abstract class BattleAction
{
    protected Battler user;
    protected List<Battler> targets;

    public Battler User { get { return user; } }

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

    /// <summary>
    /// Queues a list of instructions for the battle sprite.
    /// </summary>
    public void queueBehavior()
    {
        Queue<IBatBehavior> instr = new Queue<IBatBehavior>();
        instr.Enqueue(new MoveSpriteBehavior(user));        //step forward
        instr.Enqueue(new RunActBehavior(this));            //perform action
        instr.Enqueue(new WaitBehavior(20));                //wait 50 frames
        instr.Enqueue(new ReturnBehavior(user));            //return to home position

        user.instruct(instr);
    }
}
