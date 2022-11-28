using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBlueSpyAI : MonoBehaviour
{
    [SerializeField]
    GameObject currentNode, targetNode, destinationNode;

    [SerializeField]
    GameObject vent, documents;

    GameObject guard, previousNode;



    enum State
    {
        DoingMission,
        Run
    }

    State spyState;

    enum Mission
    {
        FindGuard,
        StealKey,
        Documents,
        Vent
    }

    Mission spyMission;

    // Start is called before the first frame update
    void Start()
    {
        spyState = State.DoingMission;
        spyMission = Mission.FindGuard;
        transform.position = currentNode.transform.position;
        targetNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {
        switch (spyState)
        {
            case State.DoingMission:

                switch (spyMission)
                {
                    case Mission.FindGuard:
                        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                        {
                            previousNode = currentNode;
                            currentNode = targetNode;

                            bool validPath = false;
                            while (!validPath)
                            {
                                if (currentNode.GetComponent<PathNode>() != null)
                                {
                                    int index = currentNode.GetComponent<PathNode>().connections.Count;
                                    GameObject ideaNode = currentNode.GetComponent<PathNode>().connections[Random.Range(0, index)];
                                    if (ideaNode != previousNode && ideaNode.GetComponent<DoorNode>() == null )
                                    {
                                        targetNode = ideaNode;
                                        validPath = true;
                                    }
                                }
                            }

                        }
                        else
                        {
                            transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3.0f);
                        }
                        break;
                    case Mission.StealKey:
                        break;
                    case Mission.Documents:
                        break;
                    case Mission.Vent:
                        break;
                }

                break;
            case State.Run:
                break;
        }
    }

    public void Run()
    {
        spyState = State.Run;
    }
}
