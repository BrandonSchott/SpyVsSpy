using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemControl : MonoBehaviour
{
    [SerializeField]
    GameObject blueSpy;

    [SerializeField]
    GameObject blueSpyNode;


    float blueSpyCaptured;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > blueSpyCaptured + 3 && blueSpy.activeSelf == false)
        {
            blueSpy.SetActive(true);
            blueSpy.GetComponent<BetterBlueSpyAI>().currentNode = blueSpyNode;
            blueSpy.GetComponent<BetterBlueSpyAI>().targetNode = blueSpyNode;
            blueSpy.transform.position = blueSpy.GetComponent<BetterBlueSpyAI>().currentNode.transform.position;
        }    
    }

    public void Captured(GameObject spy)
    {
        if(spy == blueSpy)
        {
            blueSpyCaptured = Time.time;
        }
    }
}
