using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DialogueBaseClass : MonoBehaviour
{
    public bool finished;

    protected IEnumerator WriteText(string input, TextMeshProUGUI textholder, float delay, float delayBetweenlines)
    {
       
        for (int i = 0; i < input.Length; i++)
        {
            textholder.text += input[i];
            yield return new WaitForSeconds(delay);
        }

        finished = true;
    }
}




