using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    //singleton
    public static BattleManager inst;

    private BattleEncounter encounter;   //quick reference to GameManager's encounter
    private int batCount;

    public bool testing;                //so I can see important data like framerates

    //which row the combatant is in
    public enum BattlePositions
    {
        FRONT, BACK, AIR
    }

    void Awake()
    {
        inst = this;                                //MUST HAPPEN FIRST
        encounter = GameManager.inst.encounter;
        batCount = 0;

        //spawn enemies
        /*
        Debug.Log("Spawning Enemies");
        encounter.spawnEnemies();

        //spawn party
        Debug.Log("Spawning Party");
        encounter.spawnParty();
        */

        //combine teams
        /*
        foreach (Battler enm in enemyTeam)
            combatants.Add(enm);
        foreach (Battler prt in playerTeam)
            combatants.Add(prt);
        */
    }
    
    

    //team divisions
    private List<Battler> enemyTeam = new List<Battler>();
    private List<Battler> playerTeam = new List<Battler>();
    private List<Battler> combatants = new List<Battler>();

    public List<Battler> EnemyTeam { get { return enemyTeam; } }
    public List<Battler> PlayerTeam { get { return playerTeam; } }
    public List<Battler> Combatants { get { return combatants; } }

    //battle flow control fields
    public bool playerPaused;
    public bool actionInterrupt = false;
    private bool stillFighting = true;
    private Queue<BattleAction> actQue = new Queue<BattleAction>();

    private int tickTimer;
    public int tickInc = 20;


	public void Start()
    {
        playerPaused = false;
        tickTimer = 0;

        Debug.Log("Spawning Enemies");
        encounter.spawnEnemies();

        //spawn party
        Debug.Log("Spawning Party");
        encounter.spawnParty();
    }

    /// <summary>
    /// Spawns an Battler at a given location and adds it to the list of combatants.
    /// </summary>
    /// <param name="comb">The combatant to spawn.</param>
    /// <param name="location">The spot on the battlefield to spawn the enemy.</param>
    public void spawnCombatant(Character comb, Vector3 location)
    {
        GameObject sprite = comb.Sprites.battleSprite;                                          //get sprite
        GameObject copy = Instantiate(sprite, location, Quaternion.identity) as GameObject;     //spawn enemy


        //add components
        Battler batSpr = copy.AddComponent<Battler>();                                          //add Battler
        batSpr.combatant = comb;                                                                //set Battler combatant
        batSpr.batID = batCount++;                                                              //set batID
        //sprite.addComponent<StatReader>();

        //add to team lists
        if (comb.Allied)
        {
            Debug.Log("Ally Spawned: " + batSpr.batID);
            //batSpr.AddComponent<AggroDisplay>();
            playerTeam.Add(batSpr);
        }
        else
        {
            Debug.Log("Enemy Spawned: " + batSpr.batID);
            enemyTeam.Add(batSpr);
        }

        combatants.Add(batSpr);
    }

    // Update is called once per frame
    void Update()
    {
        //used to determine if method is efficient or if coroutine is required
        if (testing) 
            Debug.Log(Time.deltaTime);
        
        //check to pause
        checkPauseButton();

	    //if unpaused by the player and uninterrupted by action
        if (!(playerPaused || actionInterrupt))
        {
            //if action queued
            if (actQue.Count > 0)
                runAction(actQue.Dequeue());
            
            //queue empty
            else
            {
                //bump combat tick
                tickTimer++;
                if (tickTimer > tickInc)
                {
                    Debug.Log("Combat Tick");
                    tickTimer = 0;
                    tickCombat();
                }//end tick
            }//end bump

        }//end pause/interrupt check

    }//end Update

    /// <summary>
    /// <para>Increments all combatants' ATB if able, and performs intended action if able.</para>
    /// <para>Only to occur when there are no more actions to be taken.</para>
    /// </summary>
    private void tickCombat()
    {
        //tick each combatant and check if intended action still valid
        foreach (Battler bat in combatants)
        {
            bool ready = bat.tick();

            if (bat.intendedAction == null)
                bat.intendedAction = bat.nextAction();
            
            bool able = (bat.intendedAction != null && bat.intendedAction.isValid());

            if (able)
            {
                if (ready)
                {
                    Debug.Log("Action Queued");
                    actQue.Enqueue(bat.intendedAction);         //queue action

                    bat.intendedAction = null;                  //mark as queued
                }
                //able but unready: wait to be ready 
            }
            else //cancel action
            {
                Debug.Log("Action Cancelled");
                bat.intendedAction = null;                      //will decide action on next tick
            }
            
        }
    }

    /// <summary>
    /// <para>Performs a Battler's action.</para>
    /// <para>Assumes the action has been valid up until immediately before this call.</para>
    /// </summary>
    private void runAction(BattleAction action)
    {
        actionInterrupt = true;     //only one action at a time
        action.User.ATBcount = 0;   //reset ATBcount
        //deduct cost from user

        Debug.Log(action.User.batID + "'s turn.");  //will be changed to action-specific log

        action.queueBehavior();
    }

    /// <summary>
    /// Toggles the pause button.
    /// </summary>
    private void checkPauseButton()
    {

    }

    private bool isStillFighting()
    {
        return true;
    }
}