using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    [SerializeField] private int totalAttempts;

    public int TotalAttempts
    {
        get
        {
            return totalAttempts;
        }
    }
}
