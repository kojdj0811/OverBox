using Hyerin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUIManager : MonoBehaviour
{
    [System.Serializable]
    public class Computing_Order 
    {
        public int price;
        public int howMany;
        public bool isAlreadyBuy;
        public Image image;
        public Computing_Order(int price, int howMay)
        {
            this.price = price;
            howMany = howMay;
            isAlreadyBuy = false;
        }
    }

    public TextMeshProUGUI least_Coins;

    [SerializeField]
    private Computing_Order[] computing = new Computing_Order[6];

    [SerializeField]
    private Text[] texts;


    [SerializeField]
    private KeyCode[] inputSet = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D };
    int[] ordering_Arr;

    public Color EnableColor;
    public Color DisableColor;


    private int cur_Coin_GM;
    
    private void OnEnable()
    {
        cur_Coin_GM = Moru.GameManager.Instance.curCoin; //?躹

        least_Coins.text =  Moru.GameManager.Instance.curCoin.ToString();
        foreach (var comp in computing)
        {
            comp.isAlreadyBuy = false;
            comp.image.color = EnableColor;
        }
        ordering_Arr = new int[6] { 0, 0, 0, 0, 0, 0 };
        for(int i = 0; i< texts.Length; i++)
        {
            texts[i].text = computing[i].howMany.ToString();
        }
    }

    private void OnDisable()
    {
        Moru.GameManager.Instance.curCoin = cur_Coin_GM;

        Player.Instance.state = Player.State.Movable;
        Moru.MoruDefine.delegate_Delivery.Invoke(ordering_Arr);
        Moru.GameManager.Instance.curCoin = Moru.GameManager.MaxCoin;
    }


    public void ordering()
    {
        for(int i=0;i< (int)Moru.MoruDefine.Product.MAX; i++)
        {
            if (Input.GetKeyUp(inputSet[i]))
            {
                if (cur_Coin_GM >= computing[i].price && !computing[i].isAlreadyBuy)
                {
                    ordering_Arr[i] += computing[i].howMany;
                    cur_Coin_GM -= computing[i].price;
                    computing[i].isAlreadyBuy = true;
                    computing[i].image.color = DisableColor;
                    least_Coins.text = cur_Coin_GM.ToString();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
     

            gameObject.SetActive(false);

        }
    }
}
