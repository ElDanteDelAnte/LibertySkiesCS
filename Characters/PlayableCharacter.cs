using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// <para>Any potential party member.</para>
/// <para>Specific characters will be derrived from this.</para>
/// </summary>
public class PlayableCharacter : Character
{

    protected int[] skillEXP = new int[Enum.GetNames(typeof(Skills)).Length];
    protected int[] skillTNL = new int[Enum.GetNames(typeof(Skills)).Length];
    
    /// <summary>
    /// Raises a character's EXP in a given skill
    /// and levels up that skill as many times as needed.
    /// </summary>
    /// <param name="skill">The Skill in which to gain EXP.</param>
    /// <param name="exp">How much EXP gained.</param>
    public void gainExp(Skills skill, int exp)
    {
        int sk = (int)skill;
        skillEXP[sk] += exp;

        //continue leveling
        while (skillEXP[sk] > skillTNL[sk])
        {
            skillLVS[sk]++;                 //bump level
            skillEXP[sk] -= skillTNL[sk];   //deduct 1 LV worth of EXP
            //set new TNL
            //diplay level up

        }
    }

    //out-of-combat dialogues


    //save

    //load

    /// <summary>
    /// Constructor for a playable character.
    /// </summary>
    /// <param name="stats">Base stat set.</param>
    /// <param name="skills">Character's starting levels.</param>
    /// <param name="sprite">Character's sprite set.</param>
    public PlayableCharacter(string name, int[] stats, int[] skills, SpriteSet sprite, BattleManager.BattlePositions pos) : base(name, stats, skills, sprite, true, pos)
    {
        //TODO: Set TNLs!
    }

    //need to override AI without completely removing it


    /// <summary>
    /// <para>Converts a playable character into an enemy.</para>
    /// <para>Transfers player data directly to enemy.
    ///       Will be used in the ending sequence.</para>
    /// </summary>
    /// <returns>Enemy version of this playable character.</returns>
    public Character convToEnemy()
    {
        return new Character(this.Name, this.baseStats, this.skillLVS, this.Sprites, false, this.pos);
    }

}
