using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AutoSpawnButton autoSpawnButton;
    [SerializeField] private AutoSpawnMultiplier autoSpawnMultiplier;
    [SerializeField] private GameObject statisticsPanel;
    [SerializeField] private string[] awesomeWords;
    [SerializeField] private GameObject tutorialScreen;

    private FloatTextSpawner floatTextSpawner;
    private Camera camera;
    public float randomComboTextSpawn = 20;

    private void Start()
    {
        floatTextSpawner = GetComponent<FloatTextSpawner>();
        camera = Camera.main;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();

        autoSpawnButton.CheckBuyingUpgrade(score);
        autoSpawnMultiplier.CheckBuyingUpgrade(score);
    }

    internal void UpdateStatistics(int[] diceEyeCount)
    {
        for (int i = 0; i < statisticsPanel.transform.childCount; i++)
        {
            Transform child = statisticsPanel.transform.GetChild(i);
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            text.text = diceEyeCount[i + 1].ToString() + "x";
        }
    }

    internal void ShowScoreFloatText(int diceEyes, Vector3 position, float timeToSpawnFloatText)
    {
        string text = "+" + diceEyes;
        Vector2 screenPos = camera.WorldToScreenPoint(position);
        floatTextSpawner.SpawnFloatingTextAfterTime(text, screenPos, 1, timeToSpawnFloatText);
    }

    public void ShowCombo(float comboScore)
    {
//        Debug.Log("COMBO:  " + comboMultiplier);
        string text = awesomeWords[Random.Range(0, awesomeWords.Length - 1)]
                      + "\n"
                      + comboScore;
        Vector2 screenPos = new Vector2(Screen.width / 2, Screen.height / 2) +
                            Vector2.one * (UnityEngine.Random.insideUnitSphere * randomComboTextSpawn);
        floatTextSpawner.SpawnFloatingText(text, screenPos, 8);
    }

    public void ShowTutorial()
    {
        tutorialScreen.SetActive(true);
    }
}