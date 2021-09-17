using UnityEngine;



public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;
    
    public bool canReceiveInput;
    public bool InputReceived;

    private void Start()
    {
        canReceiveInput = true;
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!GameController.IsPause && !DialogueController.DialogueOn)
        {
            Attack();
        }
    }
        
    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (WeaponsSwitch.mode == Mode.sword || Player.PistolUp)
            {
                if (canReceiveInput)
                {
                    InputReceived = true;
                    canReceiveInput = false;
                }
                else
                {
                    return;
                }
            }

        }
        
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
    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        }
        else
        {
            canReceiveInput = false;
        }
    }


}
