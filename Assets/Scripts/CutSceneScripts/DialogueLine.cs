using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DialogueLine : DialogueBaseClass
{
    [Header("Text OPtions")]
    private TextMeshProUGUI textholder;
    [SerializeField] private string input = "a";


    [Header("Time parameters")]
    [SerializeField] private float delay = 1f;
    [SerializeField] private float delayBetweenLines = 1f;

    [Header("Character")]
    [SerializeField] private Sprite characterSprite = null;
    [SerializeField] private Image ImageHolder = null;
    
    [Header("DialogueHolder")]
    [SerializeField] private Sprite DialogueSprite = null;
    [SerializeField] private Image DialogueHolder = null;



    private void Awake()
    {
        textholder = GetComponent<TextMeshProUGUI>();
        ImageHolder.sprite = characterSprite;
        DialogueHolder.sprite = DialogueSprite;
        ImageHolder.preserveAspect = true;
    }

    private void Start()
    { 
        StartCoroutine(WriteText(input, textholder, delay, delayBetweenLines)); 
    }



}
