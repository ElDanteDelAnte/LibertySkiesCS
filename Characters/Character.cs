using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Universal to enemies and party members.
/// </summary>
public class Character
{
    //base stats
    public enum Stats
    {
        VITALITY, MIGHT, WIT, SPIRIT, AGILITY
    }

    public int[] baseStats = new int[Enum.GetNames(typeof(Stats)).Length];

    //learned skills
    public enum Skills
    {
        SWORDS, GUNS, ARCHERY, SPEARS, AXES, BLUNTS, STAVES, UNARMED,                   //weapon skills
        MMAGIC, BMAGIC, FMAGIC, TMAGIC,                                                 //magic skills
        MEDICINE, STEALTH, SPEECH, TECH, SURVIVAL,                                      //utility skills
        CRAFTING, FARMING, MINING                                                       //gathering skills
    }


    private string name;
    public string Name { get { return name; } }

    //Levels for each skill. Array is exactly as long as the number of skills
    public int[] skillLVS = new int[Enum.GetNames(typeof(Skills)).Length];

    //friend or foe?
    private bool allied;
    public bool Allied {  get { return allied; } }

    //race

    //equipment

    //known abilities

    //spriteset
    private SpriteSet sprites;
    public SpriteSet Sprites { get { return sprites; } }

    //dialogues

    //default AI

    public BattleManager.BattlePositions pos;
    
    /// <summary>
    /// Creates a character from a set of base stats and skills.
    /// </summary>
    /// <param name="name">Character's name.</param>
    /// <param name="stats">Character's base stats.</param>
    /// <param name="skills">Character's starting skill levels.</param>
    /// <param name="ally">Whether the character is friendly to the party.</param>
    public Character(string name, int[] stats, int[] skills, SpriteSet sprset /*, race*/,
        bool ally, BattleManager.BattlePositions posit)
    {
        //assert
        if (stats.Length != baseStats.Length)
            Debug.Log("Stat array incorrect size!");
        if (skills.Length != skillLVS.Length)
            Debug.Log("Skill array incorrect size!");

        this.name = name;
        
        //init stats
        for (int i = 0; i < baseStats.Length; i++)
            baseStats[i] = stats[i];
       
        //init skills
        for (int i = 0; i < skillLVS.Length; i++)
            skillLVS[i] = skills[i];

        //init spriteset
        this.sprites = sprset;

        //set team
        this.allied = ally;

        //set position
        this.pos = posit;
    }

    /// <summary>
    /// <para>Converts enemy character into a playable character.</para>
    /// </summary>
    /// <returns>Playable version of this character.</returns>
    public PlayableCharacter toPlayable()
    {
        return new PlayableCharacter(this.name, this.baseStats, this.skillLVS, this.sprites, this.pos);
    }

    
}
