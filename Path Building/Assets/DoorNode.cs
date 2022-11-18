using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNode : MonoBehaviour
{
    public List<GameObject> connections;

    DoorNode()
    {
        connections = new List<GameObject>();
    }

    public void AddConnection(GameObject target)
    {
        connections.Add(target);
    }

    public void ClearConnections()
    {
        connections.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.5f);

        foreach (GameObject target in connections)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.transform.position + new Vector3(0, 0.5f, 0));
        }
    }
}
