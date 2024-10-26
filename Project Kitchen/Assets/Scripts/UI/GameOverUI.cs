using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private TextMeshProUGUI nextLevel;
    public Image star1;
    public Image star2;
    public Image star3;
    public int Score;
    public int Score2;
    public int Score3;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button levelButton;
    [SerializeField] private Button restartButton;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameMenuScene);
        });
        levelButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.SelectScene);
        });
        restartButton.onClick.AddListener(() =>
        {
            if (level == 1)
            {
                Loader.Load(Loader.Scene.Level1Scene);
            }
            else {
                Loader.Load(Loader.Scene.Level2Scene);
            }
        
        });
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            Show();
        }
    }

    private void Show()
    {
        numberText.text = OrderManager.Instance.GetSuccessDeliveryCount().ToString();
        if (OrderManager.Instance.GetSuccessDeliveryCount() >= Score)
        {
            result.text = "You Win!!";
            if (OrderManager.Instance.GetSuccessDeliveryCount() >= Score2)
            {
                if (OrderManager.Instance.GetSuccessDeliveryCount() >= Score3)
                {
                    star3.gameObject.SetActive(true);
                    nextLevel.text = "Perfect Pass!";
                }
                else
                {
                    star2.gameObject.SetActive(true);
                    nextLevel.text = "Need "+ (Score3-OrderManager.Instance.GetSuccessDeliveryCount()) + " Score to get Next Star";
                }
            }
            else {
                star1.gameObject.SetActive(true);
                nextLevel.text = "Need " + (Score2 - OrderManager.Instance.GetSuccessDeliveryCount()) + " Score to get Next Star";
            }
        }
        else {
            result.text = "You Lose:<";
        }
        uiParent.SetActive(true);
    }

    private void Hide()
    {
        uiParent.SetActive(false);
    }
}
