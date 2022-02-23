using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class coin : MonoBehaviour
{
    [SerializeField] private Text CoinText;
    public int Coin = 0;

    public static coin instance;

    private void Awake()
    {
        instance = this;
    }
    public void addCoin()
    {
        Coin++;
        CoinText.text = "X" + Coin.ToString();
    }

}
