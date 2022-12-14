using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BetterGuardAI : MonoBehaviour
{
    [SerializeField]
    List<GameObject> patrolPoints = new List<GameObject>();

    [SerializeField]
    GameObject currentNode, targetNode, destinationNode, gameSystem;

    GameObject previousNode, spyFound;

    Vector3 spyLastPosition;

    int targetPatrolPoint;
    public enum State
    {
        patrol,
        chase
    }
    [SerializeField]
    public State guardState;

    PathNode pathNodeScipt;
    DoorNode doorNodeScipt;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        guardState = State.patrol;
        targetPatrolPoint = 0;
        transform.position = currentNode.transform.position;
        targetNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (guardState)
        {
            case State.patrol:
                if (Vector3.Distance(transform.position, patrolPoints[targetPatrolPoint].transform.position) < 1)
                {
                    if (targetPatrolPoint + 1 >= patrolPoints.Count)
                    {
                        targetPatrolPoint = 0;
                    }
                    else
                    {
                        targetPatrolPoint++;
                    }
                }
                destinationNode = patrolPoints[targetPatrolPoint];

                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    if (currentNode != destinationNode)
                    {
                        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                        {
                            previousNode = currentNode;
                            currentNode = targetNode;
                            //problem START
                            if (currentNode.GetComponent<PathNode>() != null)
                            {
                                targetNode = currentNode.GetComponent<PathNode>().connections[0];

                                foreach (var node in currentNode.GetComponent<PathNode>().connections)
                                {
                                    if (node.GetComponent<DoorNode>() != null)
                                    {
                                        if (!node.GetComponent<DoorNode>().locked)
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
                                        if (node != previousNode)
                                        {
                                            if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
                                                Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) || targetNode == null)
                                            {
                                                targetNode = node;
                                            }
                                        }

                                    }
                                }
                            }
                            //Problem END
                            else
                            {
                                targetNode = currentNode.GetComponent<DoorNode>().connections[0];

                                foreach (var node in currentNode.GetComponent<DoorNode>().connections)
                                {
                                    if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
                                       Vector3.Distance(destinationNode.transform.position, targetNode.transform.position))
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
                    }
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2f);
                }

                if (Physics.Raycast(transform.position, targetNode.transform.position - currentNode.transform.position, out hit, 50))
                {
                    if (hit.transform.tag == "Spy")
                    {
                        spyLastPosition = hit.transform.position;
                        hit.transform.SendMessage("Run", this.gameObject);
                        guardState = State.chase;
                    }
                }
                break;


            case State.chase:
                if (Vector3.Distance(spyLastPosition, transform.position) < 1)
                {
                    bool gotAway = true;
                    foreach (GameObject node in targetNode.GetComponent<PathNode>().connections)
                    {
                        if (Physics.Raycast(targetNode.transform.position, node.transform.position - targetNode.transform.position, out hit, 50))
                        {
                            if (hit.transform.tag == "Spy")
                            {
                                spyLastPosition = hit.transform.position;
                                gotAway = false;
                            }
                        }
                    }

                    if (gotAway)
                    {
                        guardState = State.patrol;
                    }

                }

                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    foreach (var node in currentNode.GetComponent<PathNode>().connections)
                    {
                        if (Vector3.Distance(spyLastPosition, node.transform.position) <
                           Vector3.Distance(spyLastPosition, targetNode.transform.position))
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Spy" && guardState == State.chase)
        {
            gameSystem.SendMessage("Captured", other.gameObject);
            other.gameObject.SetActive(false);

            //Send message to gameController they are out
        }
    }
}
