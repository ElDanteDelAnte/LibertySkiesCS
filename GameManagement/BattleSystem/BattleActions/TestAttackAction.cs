using UnityEngine;
using System.Collections.Generic;
using System;

public class TestAttackAction : BattleAction
{
    public TestAttackAction(Battler user) : base(user)    { }
    public TestAttackAction(Battler user, List<Battler> targets) : base(user, targets)  { }

    /// <summary>
    /// Display who attacks whom.
    /// </summary>
    public override void act()
    {
        Battler targ = targets[0];

        string readout = user.combatant.CharName + " (" + user.batID + ") attacks " + targ.combatant.CharName + " (" + targ.batID + ").";
        Debug.Log(readout);
    }
}
