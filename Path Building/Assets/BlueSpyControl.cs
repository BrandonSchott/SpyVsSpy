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

    Objective currentObjective;
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
        //if(!keyCollected)
        //{
        //    GuardAI guardCode= guard.GetComponent<GuardAI>();
        //    destinationNode = guardCode.currentNode;
        //}
        //else if(keyCollected && !documentCollected)
        //{
        //    destinationNode = documentNode;
        //}
        //else
        //{
        //    destinationNode = ventNode;
        //}
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
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3.5f);
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
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3.5f);
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
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3.5f);
                }
                break;
            case Objective.Run:
                break;
        }

        //if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
        //{
        //    previousNode = currentNode;
        //    currentNode = targetNode;

        //    if (currentNode != destinationNode)
        //    {
                
        //        if (currentNode.GetComponent<PathNode>() != null)
        //        {
        //            targetNode = currentNode.GetComponent<PathNode>().connections[0];

        //            foreach (var node in currentNode.GetComponent<PathNode>().connections)
        //            {
        //                if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
        //                   Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
        //                {
        //                    targetNode = node;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            targetNode = currentNode.GetComponent<DoorNode>().connections[0];

        //            foreach (var node in currentNode.GetComponent<DoorNode>().connections)
        //            {
        //                if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
        //                   Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
        //                {
        //                    targetNode = node;
        //                }
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 3.5f);
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Guard")
        {
            currentObjective = Objective.Document;
        }
    }
}
