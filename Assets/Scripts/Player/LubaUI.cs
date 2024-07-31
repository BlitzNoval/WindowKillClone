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

    public TMP_Text maxHealthTxt;
    public TMP_Text crntLvl;
    public TMP_Text coins;

    public Slider healthSlider;
    public Slider levelSlider;

    #region
    public GameObject runPanel; // This is the Lose Panel
    public GameObject pauseMenu;
    public GameObject damageTextPrefab;
    #endregion

    private float lastHealth;

    public float enemyCrntHealth;

    private Enemy enemy;

    public int dmgValue;


    void Start()
    {

        lastPlayerHealth = health;
        playerResources = PlayerResources.Instance;
        if (playerResources != null)
        {
            Debug.Log("Player Instance Found");
        }
        else
        {
            Debug.LogError("PlayerResources singleton instance not found.");
        }

        // Find the enemy component by tag
        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        if (enemyObject != null)
        {
            enemy = enemyObject.GetComponent<Enemy>();
            lastHealth = enemyCrntHealth = enemy.health;
        }
        else
        {
            Debug.LogError("Enemy component not found on GameObject with tag 'Enemy'.");
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

        GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
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




       
      //  DisplayDamage();
        displayPlayerDamage();
    }

    public void SpawnDamageNumber(Vector2 position, float value)
    {
        //damageTextPrefab
        GameObject newText = Instantiate(damageTextPrefab, position, Quaternion.identity);
        newText.GetComponent<TMP_Text>().text = $"{value}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {

            Debug.Log("Collsion Detectedin Luba Script");
           // Vector2 collisionPoint = collision.contacts[0].point;
        }
    }

    /*Roles and responsibilities:

Insert our roles and responsibilities here
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GameObject.FindWithTag("LubaUI").GetComponent<LubaUI>().SpawnDamageNumber(pos, amount);
     * 
     * */




    void displayPlayerDamage()
    {
        if (health != lastPlayerHealth)
        {
            int damage = lastPlayerHealth - health;
            Debug.Log($"player lost  {damage} health");
            lastPlayerHealth = health;


            if(lastPlayerHealth != health)
            {
                Debug.LogError("Code for health display not functional");
            }

            //instantiate text mesh at point of collision
        }
    }

    public void DisplayDamage() //Enemy
    {
        // Implement display damage logic here
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
