using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    [SerializeField] private Canvas pausemenu = null;
    [SerializeField] private Canvas @interface = null;
    [SerializeField] private Canvas dialogue = null;
    
    [SerializeField] private Player player = null;
    [SerializeField] private TextMeshProUGUI gameover = null;

    [SerializeField] private TextMeshProUGUI amunnition = null;

    [SerializeField] private AudioClip gameoverSound = null;

    [SerializeField] private LevelLoader scenecontroller = null;

    public static bool IsPause = false;
    public static bool GameIsOver = false;
   

    private bool soundplayed = false;

    private void Start()
    {
        GameIsOver = false;
        @interface.gameObject.SetActive(false);
        dialogue.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!DialogueController.DialogueOn)
        {
            PauseGameSwitch();
            OnPause();
        }

        if (Partner.IsReadyToLeave)
        {
            scenecontroller.StartCoroutine("LoadLevel", 0);
        }



        if (player.IsDead)
        {
            StartCoroutine("IEGameOver");
        }

        amunnition.text = player.ammunition.ToString();
 
        if (!DialogueController.DialogueOn && !IsPause)
        {
            @interface.gameObject.SetActive(true);
        }
        else 
        {
            @interface.gameObject.SetActive(false);
        }
      
    }

    private void PauseGameSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPause)
            {
                IsPause = true;

            }
            else if (IsPause)
            {
                IsPause = false;
            }

        }
    }
    private void OnPause()
    {
        if(IsPause || DialogueController.DialogueOn)
        {
            Time.timeScale = 0;
            pausemenu.gameObject.SetActive(true);
           
        }
        else if (!IsPause && !DialogueController.DialogueOn)
        {
            Time.timeScale = 1;
            pausemenu.gameObject.SetActive(false);
            
        }
       
    }

    public void OnPressContinue()
    {
        IsPause = false;
    }

    public void OnPressExit()
    {
        scenecontroller.StartCoroutine("LoadLevel", 0);
    }

    private IEnumerator IEGameOver()
    {
       
        GameIsOver = true;
        
        AudioController.instance.StopMusic();
        
        if (!soundplayed)
        {
            AudioController.instance.PlaySound(gameoverSound, 1f, 1f);
            soundplayed = true;
        }
        
        gameover.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(5f);

        scenecontroller.StartCoroutine("LoadLevel", 0);
    }
}
