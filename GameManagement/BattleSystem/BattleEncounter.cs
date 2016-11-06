using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Encounter", menuName = "Encounter/Standard")]
public class BattleEncounter : ScriptableObject
{
    //list of enemies
    //THESE WILL BE CHANGED TO GROUPS
    //public EnemyGenerator[] frontRow;
    //public EnemyGenerator[] backRow;
    //public EnemyGenerator[] airborne;

    public EncGroupGenerator[] groupGens;

    //public List<enemyPlacement> enemyTeam;

    /* DEPRECIATED
    /// <summary>
    /// <para>Spawns each enemy onto the battle grid.</para>
    /// <para>Called by BattleManager.</para>
    /// <para>Will be overloaded in non-standard encounters.</para>
    /// <para>WILL LIKELY NEED REWRITING AFTER IMPLEMENTATION OF GENERATORS</para>
    /// </summary>
    /// <returns>List of enemies.</returns>
    public List<Battler> spawnEnemies()
    {
        List<Battler> enemies = new List<Battler>();
        Battler enemy;
        
        // Front Row
        Vector3 placement = new Vector3();
        placement.x = -20;
        placement.y = -10;

        //spawn each enemy
        for (int i = 0; i < frontRow.Length; i++)
        {
            
            //inc z position
            placement.z = 5 * i + 10;

            //Instantiate at position
            if (frontRow[i] != null)
            {
                
                enemy = Instantiate(frontRow[i], placement, Quaternion.identity) as Battler;
                enemy.pos = BattleManager.BattlePositions.FRONT;
                enemies.Add(enemy);
            }
        }

        // Back Row
        placement.x = -30;

        for (int j = 0; j < backRow.Length; j++)
        {
            //inc z position
            placement.z = 5 * j + 15;

            //instantiate at position
            if (backRow[j] != null)
            {
                enemy = Instantiate(backRow[j], placement, Quaternion.identity) as Battler;
                enemy.pos = BattleManager.BattlePositions.BACK;
                enemies.Add(enemy);
            }
        }

        // Airborne
        placement.x = -25;
        placement.y = 0;

        for (int k = 0; k < airborne.Length; k++)
        {
            //inc z position
            placement.z = 5 * k + 13;

            //Instantiate at position
            if (airborne[k] != null)
            {
                enemy = Instantiate(airborne[k], placement, Quaternion.identity) as Battler;
                enemy.pos = BattleManager.BattlePositions.AIR;
                enemies.Add(enemy);
            }
        }

        return enemies;
    }*/

    /// <summary>
    /// <para>Spawn each encounter group.</para>
    /// <para>Groups will be spaced out depending on the radius of each.</para>
    /// </summary>
    public void spawnEnemies()
    {
        Vector3 center = new Vector3(-20f, -5f, 0f);
        //List<EncounterGroup> groups = new List<EncounterGroup>();

        //generate groups
        foreach (EncGroupGenerator groupGen in groupGens)
        {
            Debug.Log("Generating Group");
            EncounterGroup group = groupGen.generate();     //generate group
            center.z += group.Radius;                       //find center of group
            group.spawnGroup(center);                       //spawn around center
            center.z += group.Radius;                       //move out of group radius
            center.z += 2f;                                 //space groups slightly

        }
    }

    /// <summary>
    /// <para>Places copies of allied battler sprites onto the battle grid.</para>
    /// <para>Called by BattleManager.</para>
    /// <para></para>
    /// </summary>
    /// <returns>List of allied battlers.</returns>
    public void spawnParty()
    {
        //List<Battler> allies = new List<Battler>();

        GameManager.inst.activeParty = new List<PlayableCharacter>();   //TEST LINE
        PlayableCharacter testPlayer = new Kami();                      //TEST LINE
        GameManager.inst.activeParty.Add(testPlayer);                   //TEST LINE

        List<PlayableCharacter> party = GameManager.inst.activeParty;   //just quick reference, doesn't create playerTeam

        Vector3 frontPlacement = new Vector3();
        frontPlacement.x = 15;
        frontPlacement.y = -5;
        frontPlacement.z = 0;

        Vector3 backPlacement = new Vector3();
        backPlacement.x = 25;
        backPlacement.y = -5;
        backPlacement.z = 3;

        Vector3 airPlacement = new Vector3();
        airPlacement.x = 20;
        airPlacement.y = 5;
        airPlacement.z = 3;

        foreach (PlayableCharacter ally in party)
        {

            switch (ally.pos)
            {
                case BattleManager.BattlePositions.FRONT:
                    BattleManager.inst.spawnCombatant(ally, frontPlacement);
                    frontPlacement.z += 5;      //set placement
                    break;
                case BattleManager.BattlePositions.BACK:
                    BattleManager.inst.spawnCombatant(ally, backPlacement);
                    backPlacement.z += 5;       //set placement
                    break;
                case BattleManager.BattlePositions.AIR:
                    BattleManager.inst.spawnCombatant(ally, airPlacement);
                    airPlacement.z += 7;        //set placemnt
                    break;
            }
        }
    }

    /* TEST: BattleManager.test_spawnEnemies()
    public BattleEncounter(List<enemyPlacement> enemies)
    {
        enemyTeam = enemies;
    }*/

    //rewards

    //any additional effects
}
