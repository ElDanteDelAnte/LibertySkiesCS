using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class SpriteSetDatabase : MonoBehaviour
{
    //singleton
    public static SpriteSetDatabase inst;
    void Awake()
    {
        inst = this;
    }

    public SpriteSet[] masterSpriteSet;
}
