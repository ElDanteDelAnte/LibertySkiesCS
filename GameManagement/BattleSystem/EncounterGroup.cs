using UnityEngine;
using System.Collections.Generic;

public class EncounterGroup
{

    private int[] rowCounts = new int[3];
    private float[] rowSpaces = new float[3];
    private float[] rowWidth = new float[3];

    private float radius;
    public float Radius { get { return radius; } }

    private List<EnemyGenerator> enemies;

    /// <summary>
    /// <para>Constructs an EncounterGroup.</para>
    /// <para>Counts out the number of enemies in each row.</para>
    /// </summary>
    /// <param name="enem">List of enemies produced by EncounterGenerator.</param>
    /// <param name="fSpace">Space between enemies in the front row.</param>
    /// <param name="bSpace">Space between enemies in the back row.</param>
    /// <param name="aSpace">Space between airborne enemies.</param>
    public EncounterGroup(List<EnemyGenerator> enem, int fSpace, int bSpace, int aSpace)
    {
        rowSpaces[0] = fSpace;
        rowSpaces[1] = bSpace;
        rowSpaces[2] = aSpace;

        //init each rowCount to 0
        for (int i = 0; i < 3; i++)
            rowCounts[i] = 0;

        //count number of enemies in each row
        foreach (EnemyGenerator enemyGen in enem)
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

        enemies = enem;
        radius = getRadius();
    }

    /// <summary>
    /// <para>Determines the spacial width of an encounter group.</para>
    /// <para>The radius of the group is equal to half the width of the widest row.</para>
    /// </summary>
    /// <returns>Radius of group.</returns>
    private float getRadius()
    {
        //find row widths
        for (int i = 0; i < 3; i++)
            rowWidth[i] = rowCounts[i] * rowSpaces[i];
        
        //find width of widest row
        float widest = (rowWidth[0] > rowWidth[1]) ? rowWidth[0] : rowWidth[1];         //compare front and back rows
        if (rowWidth[2] > widest)                                                       //compare longer row to air row
            widest = rowWidth[2];

        
        return widest / 2f;
    }

    /// <summary>
    /// Spawns the rows of enemies to the battle screen.
    /// </summary>
    /// <param name="center">Center of group spawn point as determined by the encounter.</param>
    public void spawnGroup(Vector3 center)
    {
        //Vector3 fStart = new Vector3(center.x + 5f, center.y, center.z - (rowWidth[0] / 2));
        //Vector3 bStart = new Vector3(center.x - 5f, center.y, center.z - (rowWidth[1] / 2));
        //Vector3 aStart = new Vector3(center.x, center.y + 10, center.z - (rowWidth[2] / 2));

        int fSpawned = 0;
        int bSpawned = 0;
        int aSpawned = 0;

        Vector3 destination = new Vector3(0, 0, 0);

        //spawn each enemy
        foreach (EnemyGenerator enGen in enemies)
        {
            //determine location based on row
            switch (enGen.pos)
            {
                //front row
                case BattleManager.BattlePositions.FRONT:
                    destination = new Vector3(center.x + 5f, center.y, center.z - (rowWidth[0] / 2) + (rowSpaces[0] * fSpawned++));
                    break;
                //back row
                case BattleManager.BattlePositions.BACK:
                    destination = new Vector3(center.x - 5f, center.y, center.z - (rowWidth[1] / 2) + (rowSpaces[1] * bSpawned++));
                    break;
                //airborne
                case BattleManager.BattlePositions.AIR:
                    destination = new Vector3(center.x, center.y + 10, center.z - (rowWidth[2] / 2) + (rowSpaces[2] * aSpawned++));
                    break;
            }

            //generate enemy
            //Debug.Log("Destination: " + destination.x + ", " + destination.y + ", " + destination.z);
            enGen.generate(destination);
        }


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