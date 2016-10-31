using UnityEngine;
using System.Collections.Generic;

public class EncounterGroup
{

    private int[] rowCounts = new int[3];
    private int[] rowSpaces = new int[3];

    /// <summary>
    /// Constructs an Encounter group.
    /// </summary>
    /// <param name="enemies">List of enemies produced by EncounterGenerator.</param>
    /// <param name="fSpace">Space between enemies in the front row.</param>
    /// <param name="bSpace">Space between enemies in the back row.</param>
    /// <param name="aSpace">Space between airborne enemies.</param>
    public EncounterGroup(List<EnemyGenerator> enemies, int fSpace, int bSpace, int aSpace)
    {
        //math out relative spaces
    }

    /// <summary>
    /// <para>Determines the spacial width of an encounter group.</para>
    /// <para>The radius of the group is equal to half the width of the widest row.</para>
    /// </summary>
    /// <returns>Radius of group.</returns>
    public int getRadius()
    {
        int widest = -1;

        return widest;
    }

    /// <summary>
    /// Spawns the rows of enemies to the battle screen.
    /// </summary>
    /// <param name="center">Center of group spawn point as determined by the encounter.</param>
    public void spawnGroup(Vector3 center)
    {
        List<Battler> bats = new List<Battler>();
        //math out locations relative to center
        //tell BattleManager to spawn at each location
    }
}

[CreateAssetMenu(fileName = "EncGroupGen", menuName = "Encounter/Group", order = 2)]
public class EncGroupGenerator : ScriptableObject
{   
    //the enemies sure to appear
    public List<EnemyGenerator> baseEnemies;

    //the enemies with a chance to appear
    public List<EnemyGenerator> AuxEnemies;

    public int fSpace, bSpace, aSpace;

    /// <summary>
    /// Generates encounter group from given fields.
    /// </summary>
    /// <returns>Generated Encounter group.</returns>
    public EncounterGroup generate()
    {
        //build group
        List<EnemyGenerator> enemyList = new List<EnemyGenerator>();
        //math out which enemies to add

        return new EncounterGroup(enemyList, fSpace, bSpace, aSpace);
    }
}