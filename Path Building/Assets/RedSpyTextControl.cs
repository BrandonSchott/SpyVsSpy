using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedSpyTextControl : MonoBehaviour
{
    [SerializeField]
    GameObject RedSpy;

    // Update is called once per frame
    void Update()
    {
        if (RedSpy.activeSelf == false)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Found & Respawning";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyState == BetterRedSpyAI.State.Run)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Evading Capture";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.GoToDoors)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Going To Doors";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.LockPick)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Picking Lock";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.SwitchDocuments)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Destroying Documents";
        }

    }
}
