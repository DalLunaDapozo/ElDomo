using UnityEngine.UI;
using UnityEngine;

public class BodiesDialogue : MonoBehaviour
{
    [SerializeField] private Canvas E = null;
    [SerializeField] private GameObject dialogue = null;
    [SerializeField] private Image background = null;
    [SerializeField] private DialogueHolder dialogueHolder = null;
   
    private bool IsNear = false;
    private bool donewith = false;

    private void Update()
    {
        if (!donewith)
        {
            E.gameObject.SetActive(IsNear);
        }
     

        if (IsNear && Input.GetKeyDown(KeyCode.E) && !DialogueController.DialogueOn && !donewith)
        {
            DialogueController.DialogueOn = true;
            dialogue.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            dialogueHolder.gameObject.SetActive(true);
            E.gameObject.SetActive(false);
            donewith = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsNear = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsNear = false;

        }
    }


}
