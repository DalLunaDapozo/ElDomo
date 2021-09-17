using TMPro;
using UnityEngine;
using System.Collections;

public class PrologueText : MonoBehaviour
{
    [Header("Text OPtions")]
    private TextMeshProUGUI textholder;
    [SerializeField] private string input = "a";


    [Header("Time parameters")]
    [SerializeField] private float delay = 1f;
    [SerializeField] private float delayBetweenLines = 1f;

    public bool finished;
    private void Awake()
    {
        textholder = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        StartCoroutine(WriteText(input, textholder, delay, delayBetweenLines));
    }

    protected IEnumerator WriteText(string input, TextMeshProUGUI textholder, float delay, float delayBetweenlines)
    {


        for (int i = 0; i < input.Length; i++)
        {
            textholder.text += input[i];
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delayBetweenlines);

        finished = true;
    }
}
