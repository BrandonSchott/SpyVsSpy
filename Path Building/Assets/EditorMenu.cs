using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class EditorMenu : MonoBehaviour
{
    static GameObject pathnode;
    static GameObject doornode;
    static GameObject[] spawner;
    static GameObject[] allNodes;

    // Create a Sub Menu Item Called Spawn Floor Nodes under menu item grid generation
    [MenuItem("Grid Generation/Spawn Floor Nodes", priority = 0)]

    //Spawn pathnodes on a selected floor
    static void SpawnFloorNodes()
    {
        // transform of the floor
        Transform floor;

        //Extents of the floor
        Vector3 floorExtents;

        //the Count for giving them to all unique ID
        int count = 0;

        // Load pathnode prefab from the resources folder
        pathnode = Resources.Load("PathNode") as GameObject;

        // Store the transform of the selected floor
        floor = Selection.transforms[0];

        // Store the extents of the selected floor leaving a snmall margin around the edges byu subtracating a 
        floorExtents = floor.gameObject.GetComponent<BoxCollider>().bounds.extents - new Vector3(1, 0, 1);

        ClearFloorNodes();

        //starting at bottom left spawn things
        for (float i = floorExtents.x * -2; i <= 0; i += 2)
        {
            for (float j = floorExtents.z * -2; j <= 0; j += 2)
            {
                GameObject spawnedNode = Instantiate(pathnode, floor.position + floorExtents + new Vector3(i, 0, j), floor.rotation);
                count++;
                spawnedNode.name = "Pathname" + count;
                spawnedNode.transform.parent = floor;
            }
        }
    }

    [MenuItem("Grid Generation/Spawn Floor Nodes", true)]

    static bool ValidateSpawnFloorNodes()
    {
        bool validated = true;

        if (Selection.transforms.Length == 1)
        {
            foreach (Transform sphere in Selection.transforms)
            {
                if (sphere.tag != "Floor")
                {
                    validated = false;
                }
            }
        }
        else
        {
            validated = true;
        }
        return validated;
    }

    [MenuItem("Grid Generation/Clear Floor Nodes", priority = 0)]

    static void ClearFloorNodes()
    {
        for (int breakout = 0; Selection.gameObjects[0].transform.childCount > 0; breakout++)
        {
            DestroyImmediate(Selection.gameObjects[0].transform.GetChild(0).gameObject);

            if (breakout > 10000)
            {
                Debug.Log("infinite");
                break;
            }
        }

    }

    // Create a Sub Menu Item Called Spawn Floor Nodes under menu item grid generation
    [MenuItem("Grid Generation/Spawn Trigger Nodes", priority = 0)]

    //Spawn pathnodes on a selected floor
    static void SpawnTriggerNodes()
    {
        RaycastHit hit;
        // transform of the floor
        Transform floor;

        //Extents of the floor
        Vector3 floorExtents;

        //the Count for giving them to all unique ID
        int count = 0;

        // Load pathnode prefab from the resources folder
        pathnode = Resources.Load("PathNode") as GameObject;
        doornode = Resources.Load("DoorNode") as GameObject;

        // Store the transform of the selected floor
        floor = Selection.transforms[0];

        // Store the extents of the selected floor leaving a snmall margin around the edges byu subtracating a 
        floorExtents = floor.gameObject.GetComponent<BoxCollider>().bounds.extents - new Vector3(1, 0, 1);

        ClearFloorNodes();

        //starting at bottom left spawn things
        for (float i = floorExtents.x * -2; i <= 0; i += 2)
        {
            for (float j = floorExtents.z * -2; j <= 0; j += 2)
            {
                if (Physics.Raycast(floor.position + floorExtents + new Vector3(i, 0, j), Vector3.down, out hit))
                {
                    if (hit.transform.tag != "Wall")
                    {
                        if (hit.transform.tag == "Door")
                        {
                            GameObject spawnedNode = Instantiate(doornode, new Vector3(hit.point.x, hit.point.y, hit.point.z), floor.rotation);
                            count++;
                            spawnedNode.name = "door" + count;
                            spawnedNode.transform.parent = floor;
                        }
                        else
                        {
                            GameObject spawnedNode = Instantiate(pathnode, new Vector3(hit.point.x, hit.point.y, hit.point.z), floor.rotation);
                            count++;
                            spawnedNode.name = "Pathname" + count;
                            spawnedNode.transform.parent = floor;
                        }

                    }

                }
            }
        }
    }

    [MenuItem("Grid Generation/Build Paths", priority = 100)]

    static void BuildPaths()
    {
        allNodes = GameObject.FindGameObjectsWithTag("Pathnode");
        foreach (GameObject node in allNodes)
        {

            if (!node.GetComponent<PathNode>())
            {
                node.GetComponent<DoorNode>().ClearConnections();
            }
            else
            {
                node.GetComponent<PathNode>().ClearConnections();
            }

            foreach (GameObject target in allNodes)
            {
                // Make sure the target node is not the current node
                if (node.transform != target.transform)
                {
                    if (Vector3.Distance(node.transform.position, target.transform.position) <= 2.1f &&
                        !Physics.SphereCast(new Ray(node.transform.position + new Vector3(0, 1, 0), target.transform.position - node.transform.position), 0.4f, Vector3.Distance(node.transform.position, target.transform.position)) &&
                        !Physics.CheckSphere(node.transform.position + new Vector3(0, 1, 0), 0.4f))
                    {
                        if (!node.GetComponent<PathNode>())
                        {
                            node.GetComponent<DoorNode>().AddConnection(target);
                        }
                        else
                        {
                            node.GetComponent<PathNode>().AddConnection(target);
                        }
                    }
                }
            }
        }
    }
}
