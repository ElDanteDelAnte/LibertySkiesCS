using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Encounter", menuName = "Encounter/Standard")]
public class BattleEncounter : ScriptableObject
{
    //list of enemies
    //THESE WILL BE CHANGED TO GROUPS
    public Battler[] frontRow;
    public Battler[] backRow;
    public Battler[] airborne;

    public EncGroupGenerator[] groups;

    //public List<enemyPlacement> enemyTeam;

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
        
        /* Front Row */
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

        /* Back Row */
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

        /* Airborne */
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
    }

    /// <summary>
    /// <para>Places copies of allied battler sprites onto the battle grid.</para>
    /// <para>Called by BattleManager.</para>
    /// <para></para>
    /// </summary>
    /// <returns>List of allied battlers.</returns>
    public List<Battler> spawnParty()
    {
        List<Battler> allies = new List<Battler>();
        List<PlayableCharacter> party = GameManager.inst.activeParty;

        Vector3 frontPlacement = new Vector3();
        frontPlacement.x = 20;
        frontPlacement.y = -10;
        frontPlacement.z = 0;

        Vector3 backPlacement = new Vector3();
        backPlacement.x = 30;
        backPlacement.y = -10;
        backPlacement.z = 3;

        Vector3 airPlacement = new Vector3();
        airPlacement.x = 25;
        airPlacement.y = 0;
        airPlacement.z = 3;

        foreach (PlayableCharacter ally in party)
        {
            GameObject sprite = ally.Sprites.battleSprite;
            Battler copy;

            switch (ally.pos)
            {
                case BattleManager.BattlePositions.FRONT:
                    copy = Instantiate(sprite, frontPlacement, Quaternion.identity) as Battler;
                    frontPlacement.z += 5;      //set placement
                    copy.pos = ally.pos;        //set position
                    copy.combatant = ally;      //set combatant
                    allies.Add(copy);           //add to list
                    break;
                case BattleManager.BattlePositions.BACK:
                    copy = Instantiate(sprite, backPlacement, Quaternion.identity) as Battler;
                    backPlacement.z += 5;       //set placement
                    copy.pos = ally.pos;        //set position
                    copy.combatant = ally;      //set combatant
                    allies.Add(copy);           //add to list
                    break;
                case BattleManager.BattlePositions.AIR:
                    copy = Instantiate(sprite, airPlacement, Quaternion.identity) as Battler;
                    airPlacement.z += 7;        //set placemnt
                    copy.pos = ally.pos;        //set position
                    copy.combatant = ally;      //set combatant
                    allies.Add(copy);           //add to list
                    break;
            }
        }
        return allies;
    }

    /* TEST: BattleManager.test_spawnEnemies()
    public BattleEncounter(List<enemyPlacement> enemies)
    {
        enemyTeam = enemies;
    }*/

    //rewards

    //any additional effects
}
