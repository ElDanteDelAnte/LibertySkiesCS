using UnityEngine;
using System.Collections.Generic;

public class EncounterGroup
{

    private int[] rowCounts = new int[3];
    private float[] rowSpaces = new float[3];

    /// <summary>
    /// <para>Constructs an EncounterGroup.</para>
    /// <para>Counts out the number of enemies in each row.</para>
    /// </summary>
    /// <param name="enemies">List of enemies produced by EncounterGenerator.</param>
    /// <param name="fSpace">Space between enemies in the front row.</param>
    /// <param name="bSpace">Space between enemies in the back row.</param>
    /// <param name="aSpace">Space between airborne enemies.</param>
    public EncounterGroup(List<EnemyGenerator> enemies, int fSpace, int bSpace, int aSpace)
    {
        rowSpaces[0] = fSpace;
        rowSpaces[1] = bSpace;
        rowSpaces[2] = aSpace;

        //init each rowCount to 0
        for (int i = 0; i < 3; i++)
            rowCounts[i] = 0;

        //count number of enemies in each row
        foreach (EnemyGenerator enemyGen in enemies)
        {
            switch (enemyGen.pos)
            {
                case BattleManager.BattlePositions.FRONT:
                    rowCounts[0]++;
                    break;
                case BattleManager.BattlePositions.BACK:
                    rowCounts[1]++;
                    break;
                case BattleManager.BattlePositions.AIR:
                    rowCounts[2]++;
                    break;
            }
        }
    }

    /// <summary>
    /// <para>Determines the spacial width of an encounter group.</para>
    /// <para>The radius of the group is equal to half the width of the widest row.</para>
    /// </summary>
    /// <returns>Radius of group.</returns>
    public float getRadius()
    {
        float fWidth = rowCounts[0] * rowSpaces[0];
        float bWidth = rowCounts[1] * rowSpaces[1];
        float aWidth = rowCounts[2] * rowSpaces[2];

        float widest = (fWidth > bWidth) ? fWidth : bWidth;     //compare front and back rows
        if (aWidth > widest)                                    //compare longer row to air row
            widest = aWidth;

        return widest / 2f;
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

        enemyList = baseEnemies;    //TEST LINE

        //math out which enemies to add

        return new EncounterGroup(enemyList, fSpace, bSpace, aSpace);
    }
}