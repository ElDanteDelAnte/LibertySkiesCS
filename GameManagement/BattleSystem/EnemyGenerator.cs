using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Creates an enemy from a set of parameters that scales to the player's level.
/// </summary>
public class EnemyGenerator : ScriptableObject
{
    public List<SpriteSet> spritepool;
    public CreatureRace race;
    //AI
    //inventory
    //base stats
    public int[] baseStats = new int[Enum.GetNames(typeof(Character.Stats)).Length];
    public int[] baseSkills = new int[Enum.GetNames(typeof(Character.Skills)).Length];

    /// <summary>
    /// Generate and spawn an enemy to the battle screen.
    /// </summary>
    /// <param name="location">Enemy's "home" position on the screen.</param>
    /// <returns>Combatant-configured Battler copy.</returns>
    public Battler generate(Vector3 location /*, scaleFactor */)
    {
        //select spriteset from pool
        SpriteSet sprt = spritepool[0];
        //select AI from pool

        //scale to level
        int[] finalStats = baseStats;   
        int[] finalSkills = baseSkills;
        
        Character enemy = new Character(finalStats, finalSkills, sprt /* race */, false);
        //race
        //attach weapon
        //known abilities
        Battler enBatSpr = enemy.Sprites.battleSprite;
        Battler batCopy = Instantiate(enBatSpr, location, Quaternion.identity) as Battler;

        batCopy.combatant = enemy;
        return batCopy;
    }
	
}
