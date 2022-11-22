using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    [SerializeField]
    public GameObject currentNode;
    [SerializeField]
    GameObject targetNode, previousNode;

    PathNode pathNodeScipt;

    [SerializeField]
    public bool spyDetected;

    RaycastHit hit;

    [SerializeField]
    Vector3 spyLastPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = currentNode.transform.position;
        targetNode = currentNode;
        spyDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Physics.Raycast(transform.position, targetNode.transform.position - currentNode.transform.position, out hit, 50))
        {
            if (hit.transform.tag == "Spy")
            {
                Debug.Log("Found You");
                spyDetected = true;
                spyLastPosition = hit.transform.position;
                hit.transform.SendMessage("Run");
            }
        }


        if (!spyDetected)
        {
            if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
            {
                previousNode = currentNode;
                currentNode = targetNode;

                bool validPath = false;
                while (!validPath)
                {
                    pathNodeScipt = currentNode.GetComponent<PathNode>();
                    int index = pathNodeScipt.connections.Count;
                    GameObject ideaNode = pathNodeScipt.connections[Random.Range(0, index)];
                    if (ideaNode.GetComponent<PathNode>() != null && ideaNode != previousNode)
                    {
                        targetNode = ideaNode;
                        validPath = true;
                    }
                }
            }
            else
            {
                transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2.0f);
            }
        }
        else
        {
            if(Vector3.Distance(spyLastPosition, transform.position) < 1)
            {
                spyDetected = false;
            }
            else
            {
                if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
                {
                    previousNode = currentNode;
                    currentNode = targetNode;

                    foreach (var node in currentNode.GetComponent<PathNode>().connections)
                    {
                        if (Vector3.Distance(spyLastPosition, node.transform.position) <
                           Vector3.Distance(spyLastPosition, targetNode.transform.position) && node != previousNode)
                        {
                            targetNode = node;
                        }
                    }
                }
                else
                {
                    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2f);
                }
            }
            //if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
            //{
            //    if (Vector3.Distance(transform.position, spyLastPosition) < 0.5)
            //    {
            //        spyDetected = false;
            //    }
            //    else
            //    {
            //    previousNode = currentNode;
            //        currentNode = targetNode;

            //        foreach (var node in currentNode.GetComponent<PathNode>().connections)
            //        {
            //            if (Vector3.Distance(spyLastPosition, node.transform.position) <
            //               Vector3.Distance(spyLastPosition, targetNode.transform.position) && node.GetComponent<PathNode>() != null)
            //            {
            //                targetNode = node;
            //            }
            //        }
            //    }

            //}
            //else
            //{
            //    transform.Translate((targetNode.transform.position - transform.position).normalized * Time.deltaTime * 2.0f);
            //}

        }

    }
}
