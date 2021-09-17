using System.Collections;
using UnityEngine;

public class Boss : Enemy
{

    

    private new void Start()
    {
        healthbar.SetMaxHealth(maxhealth);
        healthbar.SetHealth(currenthealth);

        
        
        if (IsDead == false)
        {
            InvokeRepeating("SetTarget", 0, 1);
            InvokeRepeating("AttackPlayer", 1, 3f);
        }
        
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Update()
    {
        if (!startCutScene.IsCutSceneOn)
        {
            base.Update();
        }

        if (currenthealth <= 0)
        {
            IsDead = true;
        }
    
    }

    protected override IEnumerator DeathAnimation()
    {
        
        return base.DeathAnimation();
    }

}
