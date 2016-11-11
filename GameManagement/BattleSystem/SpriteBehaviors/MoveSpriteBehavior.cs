using UnityEngine;
using System.Collections;
using System;

public class MoveSpriteBehavior : IBatBehavior
{
    private Battler battler;    //the battle sprite to be moved
    private Vector3 destPos;    //the location to move to
    private float speed = 50f;  //the rate at which to move (50 for "step forward")

    /// <summary>
    /// Constructs a behavior to move a battle sprite to a location.
    /// </summary>
    /// <param name="bat">The battle sprite to move.</param>
    /// <param name="dest">The position to move to.</param>
    /// <param name="sp">The speed at which to move.</param>
    public MoveSpriteBehavior(Battler bat, Vector3 dest, float sp)
    {
        battler = bat;
        destPos = dest;
        speed = sp;
    }

    /// <summary>
    /// Used for basic "step forward" action.
    /// </summary>
    /// <param name="bat">The battle sprite to move.</param>
    public MoveSpriteBehavior(Battler bat)
    {
        float distance = 10;    //abs. val. distance
        battler = bat;
        Vector3 home = battler.HomePos;

        distance = (battler.allied) ? -distance : distance;     //direction based on team

        Vector3 dest = new Vector3(home.x + distance, home.y, home.z); //actual destination
        destPos = dest;
    }
    
    /// <summary>
    /// Moves the sprite toward its destination.
    /// </summary>
    public void act()
    {
        battler.transform.position = Vector3.MoveTowards(battler.transform.position, destPos, speed * Time.deltaTime);
        //throw new NotImplementedException();
    }

    /// <summary>
    /// Returns whether the sprite has arrived at its destination.
    /// </summary>
    /// <returns>Whether the sprite has returned to its home position.</returns>
    public bool isDone()
    {
        return battler.transform.position.Equals(destPos);
        //throw new NotImplementedException();
    }
}
