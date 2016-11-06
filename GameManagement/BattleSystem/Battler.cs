using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A character's in-battle representation.
/// </summary>
public class Battler : MonoBehaviour
{
    //the set point every Battler returns to after an animation
    private Vector3 homePos;
    public Vector3 HomePos { get { return homePos; } }
    private Vector3 destPos;    //position the Battle Sprite is moving towards

    public int batID;           //ID of the Battler

    //which team
    public bool allied;
    
    

    public BattleManager.BattlePositions pos;

    public Character combatant;     //Must be set after Initialize()!

    //battle stats; must be set after Initialize()/Awake()!
    public int ATBmax;
    public int ATBcount;

    public int barrier;


    public BattleAction intendedAction;

    //Initialize battle stats
    void Start()
    {
        this.allied = combatant.Allied; //set to corresponding team, possibly redundant
        homePos = transform.position;   //set "home" position to initial position
        this.pos = combatant.pos;       //set position, possibly redundant
    }

    /// <summary>
    /// Increments ATB and all passive effects if able.
    /// </summary>
    /// <returns>True if the combatant is ready and able to take an action.</returns>
    public bool tick()
    {
        bool able = true;
        //check, bump ATB
        //check, bump stam
        //inc buffs
        //passive effects
        return able && ATBcount > ATBmax;
    }

    /// <summary>
    /// TEST VERSION
    /// The battler determines their next action based on AI or player control.
    /// </summary>
    /// <returns>The combatant's next intended action.</returns>
    public BattleAction nextAction()
    {
        List<Battler> singTarg = new List<Battler>();       //should only contain one element

        /* TEST AI */
        if (allied)     //for Kami
        {
            int targ = Random.Range(0, BattleManager.inst.EnemyTeam.Capacity);
            singTarg.Add(BattleManager.inst.EnemyTeam[targ]);
            return new TestAttackAction(this, singTarg);
        }

        //else          //for enemies
        if (pos == BattleManager.BattlePositions.FRONT)     //fighters attack Kami
        {
            singTarg.Add(BattleManager.inst.PlayerTeam[0]);
            return new TestAttackAction(this, singTarg);
        }

        //else          //for back row (healers)
        List<Battler> frontEnemies = new List<Battler>();   //enemies in the front row



        //find front enemies
        foreach (Battler fr in BattleManager.inst.EnemyTeam)
        {
            if (fr.pos == BattleManager.BattlePositions.FRONT)
                frontEnemies.Add(fr);
        }

        //select from targets
        int healTarg = Random.Range(0, frontEnemies.Capacity);
        singTarg.Add(frontEnemies[healTarg]);

        return new TestHealAction(this, singTarg);
    }

    //TEST: Steping forward for combat
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destPos, 5 * Time.deltaTime);
    }

    /// <summary>
    /// Linearly moves the battler sprite to a position on the battle field.
    /// </summary>
    /// <param name="posit">Position to move to.</param>
    private void moveToPos(Vector3 posit)
    {

    }

    /// <summary>
    /// Linearly moves the battler sprite a set distance forward on the battlefield.
    /// </summary>
    public void stepForward()
    {

    }

    /// <summary>
    /// Returns the battler sprite to its home position on the battlefield with linear movement.
    /// </summary>
    public void toHomePos()
    {

    }

    //post-battle operations
}
