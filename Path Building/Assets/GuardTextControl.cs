using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuardTextControl : MonoBehaviour
{
    [SerializeField]
    GameObject Guard1;

    // Update is called once per frame
    void Update()
    {
        if (Guard1.GetComponent<BetterGuardAI>().guardState == BetterGuardAI.State.patrol)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Patroling";
        }
        else if (Guard1.GetComponent<BetterGuardAI>().guardState == BetterGuardAI.State.chase)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Chasing Intruder";
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "N/A";
        }
    }
}
