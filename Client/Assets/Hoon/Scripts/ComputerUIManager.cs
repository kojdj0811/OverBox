using Hyerin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerUIManager : MonoBehaviour
{
    
    public int[] productPrice = new int[(int)Moru.MoruDefine.Product.MAX];

    private int[] orderProductList = new int[(int)Moru.MoruDefine.Product.MAX];
    private KeyCode[] inputSet = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D };
    

    private void Start()
    {
        transform.Find("LeftCoin/CoinCnt").GetComponent<TextMeshProUGUI>().text = "Coins\n" + Moru.GameManager.Instance.curCoin;
        for (int i = 0; i < (int)Moru.MoruDefine.Product.MAX; i++)
            orderProductList[i] = 0;
    }


    public void ordering()
    {
        for(int i=0;i< (int)Moru.MoruDefine.Product.MAX; i++)
        {
            if (Input.GetKeyUp(inputSet[i]))
            {
                if (Moru.GameManager.Instance.curCoin >= productPrice[i])
                {
                    orderProductList[i]++;
                    Moru.GameManager.Instance.curCoin -= productPrice[i];
                    transform.Find("LeftCoin/CoinCnt").GetComponent<TextMeshProUGUI>().text = "Coins\n" + Moru.GameManager.Instance.curCoin;
                }
            }
        }
    }

    private void Update()
    {
        ordering();
        orderComplete();
    }



    public void orderComplete()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Player.Instance.state = Player.State.Movable;
            Moru.MoruDefine.delegate_Delivery.Invoke(orderProductList);
            Moru.GameManager.Instance.curCoin = Moru.GameManager.MaxCoin;

            gameObject.SetActive(false);

        }
    }
}
