using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private float transitionTime = 1.0f;

    private int nextScene = 0;
    private FloatLerper alphaLerper = null;

    private enum STATE { IDLE, OPENING, CLOSING }
    private STATE state = STATE.IDLE;

    public enum SCENES { MENU, GAMEPLAY };

    public static SceneController instance = null;

    private void Awake()
    {
        alphaLerper = new FloatLerper(transitionTime, AbstractLerper<float>.SMOOTH_TYPE.STEP_SMOOTHER);
        instance = this;
    }
    void Update()
    {
        UpdateTransition();
    }
    public void LoadScene(int sceneIndex)
    {
        nextScene = sceneIndex;
        OnTransitionStart();
    }
    public void LoadScene(SCENES scene)
    {
        nextScene = (int)scene;
        OnTransitionStart();
    }
    private void Initialize()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void UpdateTransition()
    {
        if (!alphaLerper.On)
        {
            return;
        }

        alphaLerper.Update();
        canvasGroup.alpha = alphaLerper.CurrentValue;

        if (alphaLerper.Reached)
        {
            OnReached();
        }
    }
    private void OnReached()
    {
        switch (state)
        {
            case STATE.OPENING:
                OnTransitionEnd();
                break;
            case STATE.CLOSING:
                OnTransitionAtMid();
                break;
        }
    }

    private void OnTransitionStart()
    {

        state = STATE.CLOSING;


        alphaLerper.SetValues(canvasGroup.alpha, 1.0f, true);


        canvasGroup.blocksRaycasts = true;
    }

    public void OnTransitionAtMid()
    {

        state = STATE.OPENING;

        SceneManager.LoadScene(nextScene);

        alphaLerper.SetValues(canvasGroup.alpha, 0.0f, true);
    }

    private void OnTransitionEnd()
    {
        canvasGroup.blocksRaycasts = false;
        state = STATE.IDLE;
    }

    

}
