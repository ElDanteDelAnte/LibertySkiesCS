using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// <para>Instructs a battle sprite to wait for at least one frame.</para>
/// </summary>
public class WaitBehavior : IBatBehavior
{
    private int duration;       //how many frames to wait
    private int elapsed = 1;    //how many frames already waited

    /// <summary>
    /// <para>Constructs a behavior that instructs a battle sprite to wait for a given number of frames.</para>
    /// <para>(Even if a number below 1 is specified, it still takes one frame to dequeue the behavior.</para>
    /// </summary>
    /// <param name="dur">Number of frames to wait.</param>
    public WaitBehavior(int dur)
    {
        duration = dur;
    }

    /// <summary>
    /// Increments frames elapsed.
    /// </summary>
    public void act()
    {
        //Debug.Log("Wait Frame");
        elapsed++;
        //throw new NotImplementedException();
    }

    /// <summary>
    /// Returns whether the required duration has elapsed.
    /// </summary>
    /// <returns>The duration has elapsed.</returns>
    public bool isDone()
    {
        return elapsed >= duration;
        //throw new NotImplementedException();
    }
}
