using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mode { sword, pistol };
public class WeaponsSwitch : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    [SerializeField] public static Mode mode = Mode.sword;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Switch");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("switchright"))
        {
            mode = Mode.pistol;
        }
        else
        {
            mode = Mode.sword;
        }
    }
}
