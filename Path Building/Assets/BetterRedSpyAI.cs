using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterRedSpyAI : MonoBehaviour
{
    [SerializeField]
    public GameObject currentNode, targetNode;

    [SerializeField]
    GameObject documents, door, realDoor, otherRealDoor, blueSpy;

    GameObject guard, previousNode;

    RaycastHit hit;

    Renderer ren;
    Color OriginalColor;

    public enum State
    {
        DoingMission,
        Run
    }

    [SerializeField]
    public State spyState;

    public enum Mission
    {
        GoToDoors,
        LockPick,
        SwitchDocuments,
        Complete
    }

    [SerializeField]
    public Mission spyMission;

    [SerializeField]
    float doorArrivalTime;

    // Start is called before the first frame update
    void Start()
    {
        spyState = State.DoingMission;
        spyMission = Mission.GoToDoors;
        transform.position = currentNode.transform.position;
        targetNode = currentNode;
        ren = GetComponentInChildren<Renderer>();
        OriginalColor = ren.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        switch (spyState)
        {
            case State.DoingMission:

                switch (spyMission)
                {
                    case Mission.GoToDoors:
                        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                        {
                            previousNode = currentNode;
                            currentNode = targetNode;

                            if (currentNode.GetComponent<PathNode>() != null)
                            {
                                targetNode = currentNode.GetComponent<PathNode>().connections[0];
                                foreach (var node in currentNode.GetComponent<PathNode>().connections)
                                {
                                    if (node.GetComponent<PathNode>() != null)
                                    {
                                        if (Vector3.Distance(door.transform.position, node.transform.position) <
                                       Vector3.Distance(door.transform.position, targetNode.transform.position) && node != previousNode)
                                        {
                                            targetNode = node;
                                        }
                                    }
                                    else
                                    {
                                        if (Vector3.Distance(door.transform.position, node.transform.position) <
                                       Vector3.Distance(door.transform.position, targetNode.transform.position) && node != previousNode
                                       && !node.GetComponent<DoorNode>().locked)
                                        {
                                            targetNode = node;
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2f);
                        }
                        if (Vector3.Distance(transform.position, door.transform.position) < 0.1)
                        {
                            spyMission = Mission.LockPick;
                            doorArrivalTime = Time.time;
                                transform.tag = "Spy";
                                ren.material.color = OriginalColor;
                        }
                        break;
                    case Mission.LockPick:
                        if (Time.time > doorArrivalTime + 5)
                        {
                            realDoor.SendMessage("Unlocked");
                            otherRealDoor.SendMessage("Unlocked");
                            spyMission = Mission.SwitchDocuments;
                        }
                        break;
                    case Mission.SwitchDocuments:
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
                                       Vector3.Distance(documents.transform.position, targetNode.transform.position) && node != previousNode)
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
                            transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2f);
                            if (Vector3.Distance(transform.position, documents.transform.position) < 0.5 && blueSpy.GetComponent<BetterBlueSpyAI>().spyMission != BetterBlueSpyAI.Mission.Vent)
                            {
                                spyMission = Mission.Complete;
                            }
                        }
                        break;
                    case Mission.Complete:
                        break;
                }
                break;
            case State.Run:
                if (spyMission == Mission.LockPick)
                {
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
                        transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2f);
                    }
                    if (Vector3.Distance(transform.position, guard.transform.position) > 7.5f)
                    {
                        guard = null;
                        if (spyMission != Mission.SwitchDocuments)
                        {
                            spyMission = Mission.GoToDoors;
                        }
                        spyState = State.DoingMission;
                    }
                }
                else
                {
                    transform.tag = "Guard";
                    ren.material.color = Color.black;
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
}
