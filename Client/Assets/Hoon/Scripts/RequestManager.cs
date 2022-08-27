using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public static RequestManager instance;

    //public List<Request> requestSet;
    public GameObject requestPrefab;
    public Transform generatePos;


    private Moru.MoruDefine.OrderRequest[] requestSet;
    [SerializeField]
    private List<Moru.MoruDefine.OrderRequest> orderList;
    public List<Moru.MoruDefine.OrderRequest> getOrderList() { return orderList; }





    private void Start()
    {
        instance = this;
        requestSet = Moru.GameManager.Instance.requests;
        generateRequest();
        generateRequest();
        generateRequest();
        generateRequest();
    }

    public void generateRequest()
    {
       
        Moru.MoruDefine.OrderRequest newRequest = requestSet[Random.Range(0, requestSet.Length)];
        //Request newRequest = requestSet[Random.Range(0, requestSet.Count)];

        GameObject obj = Instantiate(requestPrefab, generatePos);

       //obj.transform.Find("ClientInfo/ClientName").GetComponent<TextMeshProUGUI>().text = newRequest.clientName;
        obj.transform.Find("Recipe").GetComponent<RecipeController>().showProducts(newRequest.requestList);

        orderList.Add(newRequest);


    }

    
}
