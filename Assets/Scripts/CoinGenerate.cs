using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerate : MonoBehaviour
{
    public static CoinGenerate Instance { get; private set; }
    public float borderX = 4;
    public float borderY = 2;
    private float timer = 0;
    private int counter = 0;
    private bool locked = false;

    void Start()
    {
        Instance = this;
        Display();
    }

    void Update() {
        if (!locked) return;

        if (counter < 10)
        {
            timer = Random.Range(1, 3);
            Display();
        }
    }

    private void Display(){
        for (var i = 0; i < 10; i++)
        {
            Counter = counter + 1;
            timer   = timer + 0.25f;

            Invoke("CoinActiveted", timer);
        }

        locked = true;
    }

    private void CoinActiveted()
    {
        Vector2 position = new Vector2(Random.Range(borderX, -borderX), Random.Range(borderY, -borderY));
        GameObject coin = ObjectPooler.Instance.SpawnFromPool("Coin", position, Quaternion.identity);
    }

    public int Counter
    {
        get { return counter; }
        set { counter = value; }
    }
}
