using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// <para>Master manager for the game.</para>
/// <para>In charge of changing screens, save/loading.</para>
/// <para>Attached to a GameOb that is not to be destroyed.</para>
/// </summary>
public class GameManager : MonoBehaviour
{
    //singleton
    public static GameManager inst;
    void Awake()
    {
        inst = this;                //instantiate singleton
        DontDestroyOnLoad(this);    //IMPARATIVE: do not destroy, ever. Carries info across scenes
    }

    //battle start
    public BattleEncounter encounter;
    /*
    public void battleStart(BattleEncounter enc)
    {
        encounter = enc;
        SceneManager.LoadScene("BattleScreen");
    }*/

    //all recruited characters
    public List<PlayableCharacter> allAllies;
    public List<PlayableCharacter> activeParty;

    //ship

    //quests

    //morality

    //other junk
    

    //save game

    //load game

    
}

