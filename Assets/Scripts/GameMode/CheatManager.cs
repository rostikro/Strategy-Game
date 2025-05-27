using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private string inputBuffer = "";
    private float maxBufferTime = 5f;
    private float bufferTimer;

    private Dictionary<string, System.Action> cheatCodes = new ();

    void Start()
    {
        cheatCodes.Add("HESOYAM", GiveResources);
        cheatCodes.Add("REMOVE", RemoveFood);
    }

    void Update()
    {
        // Reset buffer if inactive for too long
        bufferTimer += Time.unscaledDeltaTime;
        if (bufferTimer > maxBufferTime)
        {
            inputBuffer = "";
            bufferTimer = 0f;
        }

        // Read typed letters
        foreach (char c in Input.inputString.ToUpper())
        {
            if (char.IsLetterOrDigit(c))
            {
                inputBuffer += c;
                bufferTimer = 0f; // Reset timer on input
                CheckCheatCodes();
            }
        }
    }

    void CheckCheatCodes()
    {
        foreach (var code in cheatCodes)
        {
            if (inputBuffer.EndsWith(code.Key))
            {
                Debug.Log($"Cheat Activated: {code.Key}");
                code.Value.Invoke();
                inputBuffer = ""; // Clear after cheat
                break;
            }
        }
    }
    
    void GiveResources()
    {
        GameMode.Instance.BringFood(1000);
        GameMode.Instance.BringWood(1000);
        GameMode.Instance.BringStone(1000);
    }

    void RemoveFood()
    {
        GameMode.Instance.ConsumeAllFood();
    }
}
