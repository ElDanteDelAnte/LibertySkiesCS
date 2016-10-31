using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    //singleton
    public static BattleManager inst;

    public BattleEncounter encounter;   //the encounter for this instance of the scene

    //which row the combatant is in
    public enum BattlePositions
    {
        FRONT, BACK, AIR
    }

    void Awake()
    {
        inst = this;
        //encounter = GameManager.inst.encounter;

        //spawn enemies
        enemyTeam = encounter.spawnEnemies();

        //spawn party
        playerTeam = encounter.spawnParty();

        //combine teams
        foreach (Battler enm in enemyTeam)
            combatants.Add(enm);
        foreach (Battler prt in playerTeam)
            combatants.Add(prt);
    }
    
    

    //team divisions
    private List<Battler> enemyTeam;
    private List<Battler> playerTeam;
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
    public void spawnEnemy(Character comb, Vector3 location)
    {
        GameObject sprite = comb.Sprites.battleSprite;                                         //get sprite
        GameObject copy = Instantiate(sprite, location, Quaternion.identity) as GameObject;     //spawn enemy

        //add components
        Battler batSpr = copy.AddComponent<Battler>() as Battler;
        batSpr.combatant = comb;
        //sprite.addComponent<>();

        //add to team lists
        if (comb.Allied)
            playerTeam.Add(batSpr);
        else
            enemyTeam.Add(batSpr);

        combatants.Add(batSpr);
    }

    // Update is called once per frame
    void Update()
    {
        //used to determine if method is efficient or if coroutine is required
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
            bool able = (bat.intendedAction != null && bat.intendedAction.isValid());

            if (able)
            {
                if (ready)
                {
                    bat.intendedAction.act();                   //perform action
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