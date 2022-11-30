using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryControl : MonoBehaviour
{
    [SerializeField]
    GameObject redSpy, blueSpy;

    // Update is called once per frame
    void Update()
    {
        if(blueSpy.GetComponent<BetterBlueSpyAI>().spyMission == BetterBlueSpyAI.Mission.Success)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Blue Spy Wins";
            Time.timeScale = 0;
        }
        else if (redSpy.GetComponent<BetterRedSpyAI>().spyMission == BetterRedSpyAI.Mission.Complete)
        {
                    gameObject.GetComponent<TextMeshProUGUI>().text = "Red Spy Wins";
            Time.timeScale = 0;

        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
