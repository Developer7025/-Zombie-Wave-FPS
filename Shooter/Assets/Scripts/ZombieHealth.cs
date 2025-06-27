using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    //die animation
    public float maxHealth = 100 ;
    public float currentHealth ;

    public bool getAttacked = false;
    public float attackedRestTime = 5f;
    public float attackedRestTimer = 0f;
    public bool ZDead ;
    void Start()
    {
        currentHealth = maxHealth ;
    }
    void Update()
    {
        if (getAttacked)
            if(attackedRestTimer < Time.time)
                getAttacked = false ;
    }

    public void DamageTaken (float DamagePoint)
    {

        currentHealth -= DamagePoint ;
        getAttacked = true;
        attackedRestTimer = Time.time + attackedRestTime ;
        if (currentHealth <= 0 ){
            ZDead = true ;
            Die();
        } 
    }
    void Die()
    {
        FindAnyObjectByType<Audio>().play("zombieDie");
        Destroy(gameObject);
    }
}
