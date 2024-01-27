using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerate : MonoBehaviour
{
    public float borderX = 4;
    public float borderY = 2;

    void Start()
    {
        Invoke("CoinActiveted", 1.0f);
       
    }

    // Update is called once per frame
    private void CoinActiveted()
    {
        for (var i = 0; i < 10; i++)
        {
            Vector2 position = new Vector2(Random.Range(borderX, -borderX), Random.Range(borderY, -borderY));
            GameObject coin = ObjectPooler.Instance.SpawnFromPool("Coin", position, Quaternion.identity);
        }
    }
}
