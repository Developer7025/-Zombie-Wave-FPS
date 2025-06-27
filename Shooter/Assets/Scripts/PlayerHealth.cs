using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float curHealth;

    public bool died;

    public Text healthText;

    public GameObject InGameUI;
    public GameObject GameOver;
    void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        healthText.text = curHealth.ToString();
    }


    public void GotHit(float damage)
    {
        if (!died)
        {
            FindAnyObjectByType<Audio>().play("playerHurt");
            if (curHealth > 0)
                curHealth -= damage;
            else
                if (curHealth <= 0)
            {
                died = true;
                InGameUI.SetActive(false);
                GameOver.SetActive(true);

                curHealth = 0;
            }
        }
    }


}
