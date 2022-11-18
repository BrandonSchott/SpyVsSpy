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
    // Start is called before the first frame update
    void Start()
    {
        transform.position = currentNode.transform.position;
        targetNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetNode.transform.position) < 0.1)
        {
            previousNode = currentNode;
            currentNode = targetNode;

            //if (currentNode != destinationNode)
            //{
            //    targetNode = currentNode.GetComponent<PathNode>().connections[0];
            //    foreach (var node in currentNode.GetComponent<PathNode>().connections)
            //    {
            //        if (Vector3.Distance(destinationNode.transform.position, node.transform.position) <
            //           Vector3.Distance(destinationNode.transform.position, targetNode.transform.position) && node != previousNode)
            //        {
            //            targetNode = node;
            //        }
            //    }
            //}
            bool validPath = false;
            while (!validPath)
            {
                pathNodeScipt = currentNode.GetComponent<PathNode>();
                int index = pathNodeScipt.connections.Count;
                GameObject ideaNode = pathNodeScipt.connections[Random.Range(0, index)];
                if(ideaNode.GetComponent<PathNode>() != null && ideaNode != previousNode)
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
}
