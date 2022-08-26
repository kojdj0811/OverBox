using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public static RequestManager instance;

    public List<Request> requestSet;
    public GameObject requestPrefab;
    public Transform generatePos;

    [SerializeField]
    private List<Request> orderList;
    public List<Request> getOrderList() { return orderList; }


    private void Start()
    {
        instance = this;
        generateRequest();
        generateRequest();
        generateRequest();
        generateRequest();
    }

    public void generateRequest()
    {

        Request newRequest = requestSet[Random.Range(0, requestSet.Count)];
        GameObject obj = Instantiate(requestPrefab, generatePos);

        obj.transform.Find("ClientInfo/ClientName").GetComponent<TextMeshProUGUI>().text = newRequest.clientName;
        obj.transform.Find("Recipe").GetComponent<RecipeController>().showProducts(newRequest);

        orderList.Add(newRequest);


    }

    
}
