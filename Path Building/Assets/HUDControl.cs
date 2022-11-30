using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDControl : MonoBehaviour
{
    [SerializeField]
    GameObject RedSpy, BlueSpy, Guard1, Guard2;

    [SerializeField]
    GameObject redText, blueText, Guard1Text, Guard2Text;
    // Update is called once per frame
    void Update()
    {
        if(BlueSpy.activeSelf == false)
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "Status: Found & Respawning";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyState == BetterBlueSpyAI.State.Run)
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "Status: Evading Capture";
        }
        else if(BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.FindGuard)
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "Status: Finding Guard";
        }
        else if(BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.StealKey)
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "Status: Stealing Key";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.Documents)
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "Status: Stealing Documents";
        }
        else if (BlueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.Vent)
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "Status: Escaping";
        }
        else
        {
            blueText.GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if (RedSpy.activeSelf == false)
        {
            redText.GetComponent<TextMeshProUGUI>().text = "Status: Found & Respawning";
        }
        else if (BlueSpy.GetComponent<BetterRedSpyAI>().spyState == BetterRedSpyAI.State.Run)
        {
            redText.GetComponent<TextMeshProUGUI>().text = "Status: Evading Capture";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.GoToDoors)
        {
            redText.GetComponent<TextMeshProUGUI>().text = "Status: Going To Doors";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.LockPick)
        {
            redText.GetComponent<TextMeshProUGUI>().text = "Status: Picking Lock";
        }
        else if (RedSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.SwitchDocuments)
        {
            redText.GetComponent<TextMeshProUGUI>().text = "Status: Destroying Documents";
        }
        else
        {
            redText.GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if(Guard1.GetComponent<BetterGuardAI>().guardState == BetterGuardAI.State.patrol)
        {
            Guard1Text.GetComponent<TextMeshProUGUI>().text = "Status: Patroling";
        }
        else if (Guard1.GetComponent<BetterGuardAI>().guardState == BetterGuardAI.State.chase)
        {
            Guard1Text.GetComponent<TextMeshProUGUI>().text = "Status: Chasing Intruder";
        }
        else
        {
            Guard1Text.GetComponent<TextMeshProUGUI>().text = "N/A";
        }

        if (Guard2.GetComponent<BetterGuardAI>().guardState == BetterGuardAI.State.patrol)
        {
            Guard2Text.GetComponent<TextMeshProUGUI>().text = "Status: Patroling";
        }
        else if (Guard2.GetComponent<BetterGuardAI>().guardState == BetterGuardAI.State.chase)
        {
            Guard2Text.GetComponent<TextMeshProUGUI>().text = "Status: Chasing Intruder";
        }
        else
        {
            Guard2Text.GetComponent<TextMeshProUGUI>().text = "N/A";
        }
    }
}
