using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LubaUI : MonoBehaviour
{
    private PlayerResources playerResources;
    private Enemy enemyS;

    public int health; // These are the PlayTest Stats however I have linked them to JayLee stats
    public int lastPlayerHealth;
    public int MaxHealt;// Reminder, Player also loses health

    public float enemyHealth;
    public float lastEnemyHealth;
    public TMP_Text maxHealthTxt;
    public TMP_Text crntLvl;
    public TMP_Text coins;

    public Slider healthSlider;
    public Slider levelSlider;

    #region
    public GameObject runPanel; // This is the Lose Panel
    public GameObject pauseMenu;
    public TMP_Text damageTextPrefab;
    public GameObject floatingText;
    #endregion

    private float lastHealth;

    public float enemyCrntHealth;

    private Enemy enemyScript;

    public int dmgValue;


    void Start()
    {

        lastPlayerHealth = health;
        lastEnemyHealth = enemyHealth;
        playerResources = PlayerResources.Instance;
        if (playerResources != null)
        {
            Debug.Log("Player Instance Found");
        }
        else
        {
            Debug.LogError("PlayerResources singleton instance not found.");
        }

        // Health Slider
        healthSlider.maxValue = MaxHealt;
        healthSlider.value = health;

        // Level Slider
        levelSlider.maxValue = playerResources.experienceRequired;
        levelSlider.value = playerResources.experience;

        pauseMenu.SetActive(false);

      
    }

    void Update()
    {

        

        MaxHealt = playerResources.maxHealth;
        health = playerResources.health;

        
        healthSlider.maxValue = MaxHealt;
        healthSlider.value = health;

        levelSlider.maxValue = playerResources.experienceRequired;
        levelSlider.value = playerResources.experience;

        // Health Bar Text
        maxHealthTxt.text = health.ToString() + "/" + MaxHealt.ToString();  // For Actual Game Health Bar Text

        // Coins Text
        coins.text = playerResources.materials.ToString();

        // Level Text
        crntLvl.text = "LV." + playerResources.level.ToString();

        if (health <= 0)
        {
            runPanel.SetActive(true);
            // Other Lose Panel Logic
        }




       
      
        displayPlayerDamage();
        displayEnemyDamage();


    }

   





    void displayEnemyDamage()
    {
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject != null)
        {
            enemyScript = enemyObject.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyHealth = enemyScript.health;
            }
        }
        if (enemyHealth != lastEnemyHealth)
        {
            float damage = lastEnemyHealth - enemyHealth;
            Debug.Log($"Enemy lost {damage} health");

            lastEnemyHealth = enemyHealth;

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null)
            {
                // Instantiate the TMP_Text at the enemy's position
                Vector3 enemyPosition = enemy.transform.position;

                GameObject damageText = Instantiate(floatingText, enemyPosition, Quaternion.identity);
                TMP_Text tmpText = damageText.GetComponentInChildren<TMP_Text>();

                // Check if the TMP component is found
                if (tmpText != null)
                {
                    // Change the text value
                    tmpText.text = $"-{damage}";
                }
                else
                {
                    Debug.LogWarning("Text is null");
                }
            }
            else
            {
                Debug.LogWarning("Enemy GameObject with tag 'Enemy' not found");
            }


            if (lastEnemyHealth != enemyHealth)
            {
                Debug.LogWarning("Code for enem health display not functional");
            }

        }
    }

    void displayPlayerDamage()
    {
        if (health != lastPlayerHealth)
        {
            int damage = lastPlayerHealth - health;
            Debug.Log($"player lost  {damage} health");
            lastPlayerHealth = health;

            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Instantiate the TMP_Text at the player's position
                Vector3 playerPosition = player.transform.position;

                Instantiate(floatingText, playerPosition, Quaternion.identity);
                TMP_Text tmpText = floatingText.GetComponentInChildren<TMP_Text>();

                // Check if the TMP component is found
                if (tmpText != null)
                {
                    // Change the text value
                    tmpText.text = $"-{damage}";
                }
                else
                {
                    Debug.LogWarning("Text is null");
                }
            }
            else
            {
                Debug.LogWarning("Player GameObject with tag 'Player' not found");
            }

            if (lastPlayerHealth != health)
            {
                Debug.LogWarning("Code for health display not functional");
            }

        }
    }

    
    // BELOW IS THE LOGIC FOR SHOWING THE LEVEL UP UI

    public GameObject[] gameObjects;

    private int currentIndex = -1;

    // THIS IS THE METHOD @JAY_LEE
    public void ActivateLevelUp()
    {
        // Increment the index
        currentIndex++;

        // If the index goes out of bounds, reset it to the first object
        if (currentIndex >= gameObjects.Length)
        {
            currentIndex = 0;
        }

        // Activate the next object
        if (currentIndex < gameObjects.Length)
        {
            gameObjects[currentIndex].SetActive(true);
        }
    }

    // Method to reset Level Up UI
    public void LevelRestart()
    {
        currentIndex = -1;
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
