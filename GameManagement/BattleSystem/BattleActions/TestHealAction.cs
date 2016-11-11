using UnityEngine;
using System.Collections.Generic;
using System;

public class TestHealAction : BattleAction
{
    public TestHealAction(Battler user) : base(user)   { }
    public TestHealAction(Battler user, List<Battler> targets) : base(user, targets)    { }

    /// <summary>
    /// Display who heals whom.
    /// </summary>
    public override void act()
    {
        Battler targ = targets[0];

        string readout = user.combatant.CharName + " (" + user.batID + ") heals " + targ.combatant.CharName + " (" + targ.batID + ").";
        Debug.Log(readout);

    }
}
