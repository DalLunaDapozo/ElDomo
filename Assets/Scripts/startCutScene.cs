using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class startCutScene : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Boss boss = null;

    [SerializeField] private GameObject bossbar = null;

    [SerializeField] private GameObject dialogue = null;
    [SerializeField] private GameObject dialogue_2 = null;

    

    private BoxCollider2D boxtrigger;

    public static bool IsCutSceneOn = false;
    public static bool CanEndGame = false;

    [SerializeField] private BoxCollider2D boxCollider = null;

    private void Awake()
    {
        boxtrigger = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        OnBossDead();
        Debug.Log(CanEndGame);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(bossSequence());
        }
    }

    private void BoxColliderOn()
    {
        boxCollider.enabled = true;
    }

    void StopCutScene()
    {
        IsCutSceneOn = false;
    }

    void OnBossDead()
    {
        if (boss.IsDead)
        {
            CanEndGame = true;
            StartCoroutine(bossDead());
        }
    }

    private IEnumerator bossSequence()
    {
        animator.SetBool("bosscamera", true);
       

       
        DialogueController.DialogueOn = true;
        dialogue.gameObject.SetActive(true);
            
        
        yield return new WaitForSeconds(15f);
        
        boss.gameObject.SetActive(true);
        bossbar.gameObject.SetActive(true);
        AudioController.instance.SetTrack(1);

        Destroy(boxtrigger);
        Invoke("StopCutScene", 1f);
        Invoke("BoxColliderOn", 1.5f);

    }
    private IEnumerator bossDead()
    {
        DialogueController.DialogueOn = true;
        dialogue_2.gameObject.SetActive(true);
        bossbar.gameObject.SetActive(false);
        yield return new WaitForSeconds(15f);

        boss.IsDead = false;
        
        animator.SetBool("bosscamera", false);
        Destroy(boxCollider);

    }
}
