using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] customerPrefabs;
    [SerializeField] Observer myObs;
    [SerializeField] Transform[] spawnPosition;
    List<bool> positionOccupied = new List<bool>();


    private int Score = 0;
    [SerializeField] int Lives = 3;
    [SerializeField] float spawnRate = 4f;//how long it take for a new customer to join
    private float spawnTimer = 4f; //timer for spawns
    [SerializeField] private int maxCustomerNum = 1;
    private int currentCustomerNum = 0;
    private bool canSpawn = true;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject scoreBoard;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            positionOccupied.Add(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCustomerNum < maxCustomerNum && canSpawn) 
        {
            if (HandleSpawnTimer())
            {
                CreateCustomer();
            }
        }
    }

    public void SetMaxCustomerNum(int value)
    {
        maxCustomerNum = value;
    }

    public int GetMaxCustomerNum()
    {
        return maxCustomerNum;
    }

    private void CreateCustomer()
    {
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            if (!positionOccupied[i])
            {
                var customer = Instantiate(customerPrefabs[Random.Range(0,customerPrefabs.Length)],spawnPosition[i]);
                customer.GetComponent<Customer_simple>().SetSpawnId(i);
                positionOccupied[i] = true;
                i = spawnPosition.Length;
                spawnTimer = spawnRate;
                currentCustomerNum += 1;
            }
        }
    }

    public void HandleSpawn(int spawnID, bool isTrue)
    {
        positionOccupied[spawnID] = isTrue; //this line is bugged? prevent customer from resolving and deleting self.
        //customer does not have an id!
    }

    public void ChangeNumCurrentCustomer(int valueToAdd)
    {
        currentCustomerNum += valueToAdd;
    }

    public void ChangeScore(int valueToAdd)
    {
        Score += valueToAdd;
        updateScoreboard();
    }

    public void EnableDisableSpawn(bool canSpawnBool)
    {
        canSpawn = canSpawnBool;
        Debug.LogWarning("Called canSpawn");
    }

    public void ChangePlayerLives(int valueToAdd)
    {
        if (Lives + valueToAdd > -1)
        {
            Lives += valueToAdd;
        }

        updateScoreboard();
    }

    private bool HandleSpawnTimer()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            return false;
        }

        return true;
    }

    public void checkForDifficultyCurve()
    {
        if (Score == 5|| Score == 15)
        {
            spawnRate += 2;
            maxCustomerNum += 1;
        }

        if (Lives <= 0)
        {
            endGame();
        }
    }

    private void endGame()
    {
        myObs.CallCutscene(true);
        gameOverCanvas.SetActive(true);
    }

    public void updateScoreboard()
    {
        scoreBoard.GetComponentInChildren<Text>().text = "Score : " + Score + "\n\nLives : " + Lives;
    }

}
