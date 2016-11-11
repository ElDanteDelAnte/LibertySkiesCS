using UnityEngine;
using System.Collections;

public interface IBatBehavior
{
    //perform specified behavior
    void act();

    //tells if action is complete (and performs any closing actions)
    bool isDone();
}
