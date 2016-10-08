using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    //singleton
    public static BattleManager inst;


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
    
    public BattleEncounter encounter;   //the encounter for this instance of the scene

    //team divisions
    private List<Battler> enemyTeam;
    private List<Battler> playerTeam;
    private List<Battler> combatants = new List<Battler>();
    
    //battle flow control fields
    private bool paused = false;
    private bool stillFighting = true;
    private Queue<BattleAction> actQueue = new Queue<BattleAction>();


	public void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //check to pause
        checkPauseButton();

	    //if unpaused
        if (!paused && stillFighting)
        {
            //tick each combatant
            foreach (Battler bat in combatants)
                bat.tick();
        }

        //perform action from queue
        BattleAction actn = actQueue.Dequeue();
        actn.act();
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