using TMPro;
using UnityEngine;

public class AnimalCounter : MonoBehaviour
{

    public static int animalCount = 0;
    public static bool firstAnimalPlaced = false;
    public static int maxAnimalCount = 20;
    public GameObject gameOverPanel;
    public GameObject mainPanel;
    public MoneyManager moneyManager;
    public TMP_Text scoreText;

    private void Update()
    {
        if(firstAnimalPlaced && animalCount==0)
        {
            GetComponent<SceneInitializer>().enabled= false;
            scoreText.text = moneyManager.money.ToString();
            mainPanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

}
