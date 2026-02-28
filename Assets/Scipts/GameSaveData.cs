using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData : MonoBehaviour
{
    public int rows;
    public int cols;
    public int score;
    public int combo;

    public List<bool> matchedCards = new();
    public List<int> cardIds = new();
}

