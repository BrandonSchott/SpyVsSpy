using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBlueSpyAI : MonoBehaviour
{
    [SerializeField]
    GameObject currentNode, targetNode, destinationNode;

    [SerializeField]
    GameObject vent, documents, door1, door2;

    GameObject guard, previousNode;

    RaycastHit hit;

    enum State
    {
        DoingMission,
        Run
    }

    [SerializeField]
    State spyState;

    enum Mission
    {
        FindGuard,
        StealKey,
        Documents,
        Vent
    }

    [SerializeField]
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
                                    if (ideaNode != previousNode && ideaNode.GetComponent<DoorNode>() == null)
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
                        if (Physics.Raycast(currentNode.transform.position, targetNode.transform.position - currentNode.transform.position, out hit, 50))
                        {
                            if (hit.transform.tag == "Guard")
                            {
                                guard = hit.transform.gameObject;
                                spyMission = Mission.StealKey;
                            }
                        }
                        break;
                    case Mission.StealKey:

                        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                        {
                            previousNode = currentNode;
                            currentNode = targetNode;

                            foreach (var node in currentNode.GetComponent<PathNode>().connections)
                            {
                                if (Vector3.Distance(guard.transform.position, node.transform.position) <
                                   Vector3.Distance(guard.transform.position, targetNode.transform.position) && node != previousNode)
                                {
                                    targetNode = node;
                                }
                            }
                        }
                        else
                        {
                            transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3f);
                        }

                        break;
                    case Mission.Documents:
                        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                        {
                            previousNode = currentNode;
                            currentNode = targetNode;

                            if (currentNode.GetComponent<PathNode>() != null)
                            {
                                targetNode = currentNode.GetComponent<PathNode>().connections[0];

                                foreach (var node in currentNode.GetComponent<PathNode>().connections)
                                {
                                    if (Vector3.Distance(documents.transform.position, node.transform.position) <
                                       Vector3.Distance(documents.transform.position, targetNode.transform.position))
                                    {
                                        targetNode = node;
                                    }
                                }
                            }
                            else
                            {
                                targetNode = currentNode.GetComponent<DoorNode>().connections[0];

                                foreach (var node in currentNode.GetComponent<DoorNode>().connections)
                                {
                                    if (Vector3.Distance(documents.transform.position, node.transform.position) <
                                       Vector3.Distance(documents.transform.position, targetNode.transform.position))
                                    {
                                        targetNode = node;
                                    }
                                }
                            }

                        }
                        else
                        {
                            transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2.5f);
                        }
                        if (Vector3.Distance(transform.position, documents.transform.position) < 1)
                        {
                            spyMission = Mission.Vent;
                        }

                        break;
                    case Mission.Vent:
                        break;
                }

                break;
            case State.Run:
                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    if (currentNode.GetComponent<PathNode>() != null)
                    {
                        targetNode = currentNode.GetComponent<PathNode>().connections[0];

                        foreach (var node in currentNode.GetComponent<PathNode>().connections)
                        {
                            if (node.GetComponent<DoorNode>() != null)
                            {
                                if (node.GetComponent<DoorNode>().locked == false)
                                {
                                    if (Vector3.Distance(guard.transform.position, node.transform.position) >
                                        Vector3.Distance(guard.transform.position, targetNode.transform.position))
                                    {
                                        targetNode = node;
                                    }
                                }

                            }
                            else
                            {
                                if (Vector3.Distance(guard.transform.position, node.transform.position) >
                               Vector3.Distance(guard.transform.position, targetNode.transform.position))
                                {
                                    targetNode = node;
                                }
                            }
                        }
                    }
                    else
                    {
                        targetNode = currentNode.GetComponent<DoorNode>().connections[0];

                        foreach (var node in currentNode.GetComponent<PathNode>().connections)
                        {

                            if (Vector3.Distance(guard.transform.position, node.transform.position) >
                           Vector3.Distance(guard.transform.position, targetNode.transform.position))
                            {
                                targetNode = node;
                            }

                        }
                    }
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 5f);
                }
                if (Vector3.Distance(transform.position, guard.transform.position) > 7.5f)
                {
                    guard = null;
                    if (spyMission == Mission.StealKey)
                    {
                        spyMission = Mission.FindGuard;
                    }
                    spyState = State.DoingMission;
                }
                break;
        }
    }

    public void Run(GameObject guardChasing)
    {
        spyState = State.Run;
        guard = guardChasing;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Guard" && spyMission == Mission.StealKey)
        {
            spyMission = Mission.Documents;
            door1.SendMessage("Unlocked");
            door2.SendMessage("Unlocked");
        }
    }
}
