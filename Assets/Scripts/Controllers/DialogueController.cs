using UnityEngine;

public class DialogueController : MonoBehaviour
{


    [SerializeField] private Canvas dialogue_2 = null;
    
    public static bool DialogueOn;

    private bool donetalking = false;

    private void Start()
    {
        DialogueOn = true;
        donetalking = false;
    }

    private void Update()
    {
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !donetalking)
        {
            DialogueOn = true;
            dialogue_2.gameObject.SetActive(true);
            donetalking = true;
        }
    }

}
