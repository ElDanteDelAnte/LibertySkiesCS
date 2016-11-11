using UnityEngine;
using System.Collections;
using System;

public class ReturnBehavior : IBatBehavior
{
    private Battler battler;    //the battler to be moved
    private float speed = 50f;  //the speed at which to move

    /// <summary>
    /// Constructs a behavior to return the battle sprite to its home position.
    /// </summary>
    /// <param name="bat">The battle sprite to move.</param>
    public ReturnBehavior(Battler bat)
    {
        battler = bat;
    }

    /// <summary>
    /// Return a sprite to home potistion at a given speed.
    /// </summary>
    /// <param name="bat">The battle sprite to move.</param>
    /// <param name="sp">The speed at which the sprite moves.</param>
    public ReturnBehavior(Battler bat, float sp)
    {
        battler = bat;
        speed = sp;
    }
    
    public void act()
    {
        battler.transform.position = Vector3.MoveTowards(battler.transform.position, battler.HomePos, speed * Time.deltaTime);
        //throw new NotImplementedException();
    }

    public bool isDone()
    {
        bool done = battler.transform.position.Equals(battler.HomePos);

        if (done)
            BattleManager.inst.actionInterrupt = false;     //Ready to dequeue next action.

        return done;
        //throw new NotImplementedException();
    }

    
}
