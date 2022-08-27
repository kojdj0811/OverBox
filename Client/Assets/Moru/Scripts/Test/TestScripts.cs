using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    public UnityEngine.UI.Image imageTest;

    public void OnChangeImage()
    {
        if (imageTest != null)
        {
            int value = Random.Range(0, Moru.MoruDefine.Item_Icon.Count);
            imageTest.sprite = Moru.MoruDefine.Item_Icon[Mathf.Clamp(value,0, Moru.MoruDefine.Item_Icon.Count)];
        }
    }

    public void OrderTest()
    {
        int[] orderList = new int[(int)Moru.MoruDefine.Product.MAX] { 15, 8, 18, 35, 21, 9 };
        Moru.GameManager.Instance.OnOrderItem(orderList);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = ~0;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
                var obstacle = hit.transform.GetComponent<Obstacle>();
                var player = FindObjectOfType<Hyerin.Player>();
                if (player == null)
                {
                    GameObject playerObj = new GameObject("플레이어");
                    player = playerObj.AddComponent<Hyerin.Player>();
                }
                if (obstacle)
                {
                    obstacle.OnInteractive(player);
                }
            }
        }
    }
}
