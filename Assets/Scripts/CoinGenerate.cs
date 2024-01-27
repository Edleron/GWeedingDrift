using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerate : MonoBehaviour
{
    public float borderX = 4;
    public float borderY = 2;
    private float timer = 0;

    void Start()
    {
        for (var i = 0; i < 10; i++)
        {
            timer += 0.25f;
            Invoke("CoinActiveted", timer);
        }       
       
    }

    // Update is called once per frame
    private void CoinActiveted()
    {

        Vector2 position = new Vector2(Random.Range(borderX, -borderX), Random.Range(borderY, -borderY));
        GameObject coin = ObjectPooler.Instance.SpawnFromPool("Coin", position, Quaternion.identity);
    }
}
