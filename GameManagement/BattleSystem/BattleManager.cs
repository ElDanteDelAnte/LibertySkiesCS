using UnityEngine;
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
        Debug.Log("Spawning Enemies");
        encounter.spawnEnemies();

        //spawn party
        Debug.Log("Spawning Party");
        encounter.spawnParty();

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
    
    //battle flow control fields
    private bool playerPaused = false;
    private bool actionInterrupt = false;
    private bool stillFighting = true;


	public void Start()
    {
        

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
            Debug.Log("Ally Spawned");
            //batSpr.AddComponent<AggroDisplay>();
            playerTeam.Add(batSpr);
        }
        else
        {
            Debug.Log("Enemy Spawned");
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
            tickCombat();
        }// end pause check

    }

    /// <summary>
    /// Increments all combatants' ATB if able, and performs intended action if able.
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
                    runAction(bat.intendedAction);              //perform action
                    bat.intendedAction = bat.nextAction();      //determine next action
                }
            }
            else //cancel action
            {
                //determine next action
                bat.intendedAction = bat.nextAction();
            }
            
        }
    }

    /// <summary>
    /// Performs a Battler's action.
    /// </summary>
    private void runAction(BattleAction action)
    {
        actionInterrupt = true;     //soft pause

        Debug.Log(action.User + "'s turn.");

        //deduct cost from user

        action.act();               //perform action

        actionInterrupt = false;    //resume
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