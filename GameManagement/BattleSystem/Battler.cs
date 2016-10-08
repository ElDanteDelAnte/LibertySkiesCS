using UnityEngine;
using System.Collections;

/// <summary>
/// A character's in-battle representation.
/// </summary>
public class Battler : MonoBehaviour
{
    //the set point every Battler returns to after an animation
    private Vector3 homePos;
    public Vector3 HomePos { get { return homePos; } }

    //which team
    private bool allied;
    public bool Allied {  get { return allied; } }
    
    //which row the combatant is in
    public enum BattlePositions
    {
        FRONT, BACK, AIR
    }

    public BattlePositions pos;

    public Character combatant;     //Must be set after Initialize()!

    //battle stats; must be set after Initialize()/Awake()!
    int ATBmax;
    int ATBcount = 0;

    int hp;
    int hp_max;
    int stam;
    int stam_max;
    int focus;
    int focus_max;


    //displays and meters

    //Initialize battle stats
    void Start()
    {
        this.allied = combatant.Allied; //set to corresponding team
        homePos = transform.position;   //set "home" position to initial position
    }

    //tick
    public void tick()
    {
        //check, bump ATB
        //check, bump stam
        //inc buffs
        //passive effects
    }
}
