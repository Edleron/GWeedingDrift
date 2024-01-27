using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public List<int> levelProcess = new List<int>();

    public int level = 0;

    public int puanValue = 0;

    void Start()
    {
        Instance = this;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel()
    {
        level++;
    }

    public int GetLevelProperty()
    {
        return levelProcess[level];
    }
}
