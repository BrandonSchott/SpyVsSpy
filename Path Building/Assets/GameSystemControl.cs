using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemControl : MonoBehaviour
{
    [SerializeField]
    GameObject blueSpy, redSpy;

    [SerializeField]
    GameObject blueSpyNode, redSpyNode;


    float blueSpyCaptured, redSpyCaptured;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > blueSpyCaptured + 3 && blueSpy.activeSelf == false)
        {
            if (blueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.Vent)
            {
                blueSpy.GetComponent<BetterBlueSpyAI>().spyMission = BetterBlueSpyAI.Mission.Documents;
            }
            blueSpy.SetActive(true);
            blueSpy.GetComponent<BetterBlueSpyAI>().currentNode = blueSpyNode;
            blueSpy.GetComponent<BetterBlueSpyAI>().targetNode = blueSpyNode;
            blueSpy.transform.position = blueSpy.GetComponent<BetterBlueSpyAI>().currentNode.transform.position;

        }
        if (Time.time > redSpyCaptured + 3 && redSpy.activeSelf == false)
        {
            redSpy.SetActive(true);
            redSpy.GetComponent<BetterRedSpyAI>().currentNode = redSpyNode;
            redSpy.GetComponent<BetterRedSpyAI>().targetNode = redSpyNode;
            redSpy.transform.position = redSpy.GetComponent<BetterRedSpyAI>().currentNode.transform.position;
        }
    }

    public void Captured(GameObject spy)
    {
        if (spy == blueSpy)
        {
            blueSpyCaptured = Time.time;
        }
        if (spy == redSpy)
        {
            redSpyCaptured = Time.time;
        }
    }
}
