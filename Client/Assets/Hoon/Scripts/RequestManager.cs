using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Moru.MoruDefine;

public class RequestManager : MonoBehaviour
{
    public static RequestManager instance;

    public GameObject requestPrefab;
    public Transform generatePos;

    [SerializeField]
    private List<Moru.MoruDefine.OrderRequest> orderList;
    private Moru.MoruDefine.OrderRequest[] requestSet;





    public List<Moru.MoruDefine.OrderRequest> getOrderList() { return orderList; }

    private void Start()
    {
        instance = this;
        requestSet = Moru.GameManager.Instance.requests;
        generateRequest();
        generateRequest();
        generateRequest();
        generateRequest();
        completeRequest(2);
    }

    public void generateRequest()
    {
       
        Moru.MoruDefine.OrderRequest newRequest = requestSet[Random.Range(0, requestSet.Length)];

        GameObject obj = Instantiate(requestPrefab, generatePos);

        obj.transform.Find("Recipe").GetComponent<RecipeController>().showProducts(newRequest.requestList);
        orderList.Add(newRequest);


    }

    public void completeRequest(int idx)
    {
        Destroy(gameObject.transform.GetChild(idx).gameObject);
        orderList.RemoveAt(idx);
        generateRequest();
    }

    public void completeRequest(OrderRequest completeRequest)
    {
        Destroy(gameObject.transform.GetChild(orderList.IndexOf(completeRequest)).gameObject);
        orderList.Remove(completeRequest);
        generateRequest();
    }


}
