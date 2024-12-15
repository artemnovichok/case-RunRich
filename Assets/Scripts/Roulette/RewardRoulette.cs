using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Player;

public class RewardRoulette : MonoBehaviour
{
    public static RewardRoulette instance;
    static System.Action videoAdComplete;
    [Header("UI Elements")]
    public Slider slider;     
    public Button stopButton;  
    public TextMeshProUGUI rewardText;  

    [Header("Roulette Settings")]
    public float speed = 1f;  
    public float[] multipliers = { 2, 3, 4, 5, 4, 3, 2 };  

    private bool isMoving = true;

    void Start()
    {
        instance = this;
        stopButton.onClick.AddListener(StopSlider);
        StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        while (isMoving)
        {
            slider.value = Mathf.PingPong(Time.realtimeSinceStartup * speed, 7);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private void StopSlider()
    {
        isMoving = false;
        stopButton.enabled = false;
        OnReward();
    }

    public void OnReward()
    {
        stopButton.onClick.RemoveListener(StopSlider);
        int index = Mathf.FloorToInt(slider.value);
        float reward = multipliers[index];
        Debug.Log($"Reward Multiplier: x{reward}");
        rewardText.text = $"x{reward}";
        GameObject.FindAnyObjectByType<PlayerMoneyComponent>().ScaleMoney((int)reward);
    }
}
