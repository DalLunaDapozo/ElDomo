using UnityEngine;
using UnityEngine.UI;
public class Partner : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Canvas E = null;
    [SerializeField] private GameObject dialogue = null;
    [SerializeField] private Image background = null;
    [SerializeField] private DialogueHolder dialogueHolder = null;


    private bool IsNear = false;

    public static bool IsReadyToLeave = false;


    private void Update()
    {
        animator.SetBool("IsNear", IsNear);
        E.gameObject.SetActive(IsNear);

        if (IsNear && Input.GetKeyDown(KeyCode.E) && !DialogueController.DialogueOn && !startCutScene.CanEndGame)
        {
            DialogueController.DialogueOn = true;
            dialogue.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
            dialogueHolder.gameObject.SetActive(true);
        
        }

        Debug.LogError(IsReadyToLeave);

        if (DialogueController.DialogueOn && IsNear && dialogue.gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
            {
                DialogueController.DialogueOn = false;
                dialogue.SetActive(false);
                background.gameObject.SetActive(false);
                dialogueHolder.gameObject.SetActive(false);
                
            }
        }

        if (IsNear && Input.GetKeyDown(KeyCode.E) && !DialogueController.DialogueOn &&  startCutScene.CanEndGame)
        {
            IsReadyToLeave = true;
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

