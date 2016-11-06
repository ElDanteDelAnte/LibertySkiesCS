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
        //soft-pause battle progression (not all cases)
        //deduct stam/mana/etc. cost from user

        //display readout

        //do damage/heal damage/etc.
        //resume battle progression
    }
}
