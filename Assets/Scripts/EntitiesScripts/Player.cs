using System.Collections;
using UnityEngine;

public enum States { idle, running, attacking, guarding, hurt, pistolup };
public class Player : Fighter
{

    [SerializeField] public States state = States.idle;

    [SerializeField] protected int maxstamina;
    [SerializeField] protected int currentstamina;

    [SerializeField] protected HealthBar staminabar = null;

    [SerializeField] public int ammunition = 30;

    [SerializeField] AudioSource audioSource = null;


    public static bool IsGuard = false;
    public static bool PistolUp = false;
    public static bool HasAmmo = true;
    public static Vector2 controls;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    protected override void Start()
    {
    

        currentstamina = maxstamina;
        healthbar.SetHealth(currentstamina);
        staminabar.SetMaxHealth(maxstamina);
    }


    void Update()
    {
        if (!GameController.IsPause && !DialogueController.DialogueOn && !startCutScene.IsCutSceneOn)
        {
            RunningInput();
            SecondaryInput();

            FlipSprite();
            OnRunning();
            OnGuarding();
            OnPistolUp();
        }

        StateAttacking();
        
        if (DialogueController.DialogueOn)
        {
            animator.SetBool("IsRunning", false);
        }

        if (IsGuard || PistolUp || state == States.attacking)
        {
            rigidbody.velocity = Vector2.zero;
        }
        
        if (ammunition <= 0)
        {
            HasAmmo = false;
        }
        else
        {
            HasAmmo = true;
        }
    }

    private void FixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("player_death") && state == States.running && !DialogueController.DialogueOn)
        {
            PhysicsMovement();
        }
    }
    
    private void PhysicsMovement()
    {

        rigidbody.velocity = new Vector2(controls.x * horizontal_speed, controls.y * vertical_speed);
            
    }
    private void FlipSprite()
    {
        if (GameController.GameIsOver == false || !DialogueController.DialogueOn)
        {
            if (controls.x < 0)
            {
                spriteRenderer.transform.localRotation = new Quaternion(0.0f, 180f, 0.0f, 0.0f);
            }
            else if (controls.x > 0)
            {
                spriteRenderer.transform.localRotation = new Quaternion(0.0f, 0f, 0.0f, 0.0f);
            }
        }
    }
    private void OnRunning()
    {
        if (controls != Vector2.zero)
        {

            if (WeaponsSwitch.mode == Mode.sword)
            {
                CombatManager.instance.canReceiveInput = false;
            }
            else
            {
                CombatManager.instance.canReceiveInput = true;
            }

            animator.SetBool("IsRunning", true);
            state = States.running;
        }
        else
        {
            CombatManager.instance.canReceiveInput = true;
            animator.SetBool("IsRunning", false);
            state = States.idle;
        }
   
    
    }
    private void RunningInput()
    {
        if (state != States.guarding && state != States.pistolup && state != States.attacking)
        {
            controls = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
    private void OnGuarding()
    {

        animator.SetBool("Guarding", IsGuard);

        if (IsGuard)
        {
            animator.SetBool("IsRunning", false);
            state = States.guarding;
        }
        if (IsGuard && currentstamina >= 0)
        {
           
            staminabar.SetHealth(currentstamina);

            return;
        }
        if (currentstamina <= 0)
        {
            IsGuard = false;
            currentstamina = 0;
        }

    }
    private void OnPistolUp()
    {
        animator.SetBool("PistolUp", PistolUp);
        if (PistolUp)
        {
            animator.SetBool("IsRunning", false);
            state = States.pistolup;
        } 
    }
    protected override void GetSlash(int attk)
    {
        if (IsGuard)
        {
            currentstamina -= 10;
        }
        else
        {
            base.GetSlash(attk);
        } 
    }
    private void SecondaryInput()
    {
        if (WeaponsSwitch.mode == Mode.sword)
        {
            if (currentstamina > 0)
            {
                IsGuard = Input.GetMouseButton(1);
            }
        }
        else if (WeaponsSwitch.mode == Mode.pistol)
        {
            PistolUp = Input.GetMouseButton(1);
        }
    }
    private void StateAttacking()
    {
        if (Input.GetButtonDown("Fire1") || animator.GetCurrentAnimatorStateInfo(0).IsName("transition_1") || animator.GetCurrentAnimatorStateInfo(0).IsName("transition_2"))
        {
            state = States.attacking;
            if (PistolUp && HasAmmo)
            {
                AudioController.instance.PlaySound(sfx[1], 0.5f, 1f);
                ammunition--;
            }
            if (PistolUp && !HasAmmo)
            {
                AudioController.instance.PlaySound(sfx[0], 0.5f, 1);
            }
        }
    }

    private void StepSound(AudioClip clip)
    {
        audioSource.volume = 0.05f;
        audioSource.pitch = Random.Range(0.6f, 1f);
        audioSource.PlayOneShot(clip);
    }
    public void PrintEvent(string s)
    {
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            collision.gameObject.SendMessage("SetOpenTrue");

        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            collision.gameObject.SendMessage("SetOpenFalse");
        }

    }
}
