using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinDetect : MonoBehaviour

{
   

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerController>().gainCoin)
           ObjectPooler.Instance.ReturnToPool("Coin", gameObject);
    }
}
