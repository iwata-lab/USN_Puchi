using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    // Start is called before the first frame update

    public Text CoinCounterText;
    int coincount;
    int totatlCoin;
    void Start()
    {
        totatlCoin = CoinColorChange.GetCoinCount();
        CoinCounterText.text = totatlCoin.ToString() + " 枚";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
