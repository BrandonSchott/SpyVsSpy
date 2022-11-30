using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{
    [SerializeField]
    GameObject doorNode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!doorNode.GetComponent<DoorNode>().locked)
        {
            gameObject.SetActive(false);
        }
    }
}
