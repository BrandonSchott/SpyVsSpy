using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpyControl : MonoBehaviour
{
    [SerializeField]
    GameObject currentNode, destinationNode, targetNode, previousNode, guard, documentNode, ventNode;

    PathNode pathNodeScipt;

    enum Objective
    {
        Key,
        Document,
        Vent,
        Run
    }

    RaycastHit hit;

    [SerializeField]
    Objective currentObjective, currentMissionObjective;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = currentNode.transform.position;
        targetNode = currentNode;
        currentObjective = Objective.Key;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentObjective)
        {
            case Objective.Key:

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
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2.5f);
                }

                break;
            case Objective.Document:
                destinationNode = documentNode;

                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    if (currentNode != destinationNode)
                    {

                        if (currentNode.GetComponent<PathNode>() != null)
                        {
                            targetNode = currentNode.GetComponent<PathNode>().connections[0];

                            foreach (var node in currentNode.GetComponent<PathNode>().connections)
                            {
                                if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
                                   Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
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
                                if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
                                   Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
                                {
                                    targetNode = node;
                                }
                            }
                        }
                    }
                    else
                    {
                        currentObjective = Objective.Vent;
                    }
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2.5f);
                }

                break;
            case Objective.Vent:
                destinationNode = ventNode;

                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    if (currentNode != destinationNode)
                    {

                        if (currentNode.GetComponent<PathNode>() != null)
                        {
                            targetNode = currentNode.GetComponent<PathNode>().connections[0];

                            foreach (var node in currentNode.GetComponent<PathNode>().connections)
                            {
                                if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
                                   Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
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
                                if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
                                   Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
                                {
                                    targetNode = node;
                                }
                            }
                        }
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2.5f);
                }
                break;
            case Objective.Run:
                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    foreach (var node in currentNode.GetComponent<PathNode>().connections)
                    {
                        if (Vector3.Distance(guard.transform.position, node.transform.position) >
                           Vector3.Distance(guard.transform.position, targetNode.transform.position) && node.GetComponent<PathNode>() != null)
                        {
                            targetNode = node;
                        }
                    }
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3f);
                }
                if(guard.GetComponent<GuardAI>().spyDetected == false)
                {
                    currentObjective = currentMissionObjective;
                }

                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Guard")
        {
            Debug.Log("Got Key");
            currentObjective = Objective.Document;
        }
    }

    public void Run()
    {
        Debug.Log("I AM NOW RUNNING");
        if(currentObjective != Objective.Run)
        {
            currentMissionObjective = currentObjective;
        }

        currentObjective = Objective.Run;
    }
}
