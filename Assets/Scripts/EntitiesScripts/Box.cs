using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private TextMeshProUGUI E = null;

    [SerializeField] private Edible potion = null; 

    public bool CanOpen = false;
    private bool IsOpen = false;
    void Update()
    {

        if (CanOpen)
        {
            E.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !IsOpen)
            {
                IsOpen = true;
                animator.SetTrigger("Open");
                potion.gameObject.SetActive(true);
            }
        }
        else
        {
            E.gameObject.SetActive(false);
        }
    }

    public void SetOpenTrue()
    {
        CanOpen = true;
    }
    public void SetOpenFalse()
    {
        CanOpen = false;
    }

}
