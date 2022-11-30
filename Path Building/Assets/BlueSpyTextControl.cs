using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlueSpyTextControl : MonoBehaviour
{
    [SerializeField]
    GameObject BlueSpy;

    // Update is called once per frame
    void Update()
    {
        if (BlueSpy.activeSelf == false)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Found & Respawning";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyState == BetterBlueSpyAI.State.Run)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Evading Capture";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.FindGuard)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Finding Guard";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.StealKey)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Stealing Key";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.Documents)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Stealing Documents";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.Vent)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Status: Escaping";
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "N/A";
        }
    }
}
