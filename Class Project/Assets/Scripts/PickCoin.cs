using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCoin : MonoBehaviour
{
    public AudioSource pickSound;
    public Text coinText;
    private int coinVal = 100;
    public static int numCoins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        pickSound.Play();
        if(numCoins < 1000)
        {
        numCoins += coinVal;
        coinText.text = "Gold Coins: " + numCoins;
        } else {
            coinText.text = "Gold Coins: " + 999;
        }
    }
}
