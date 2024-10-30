using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameClockUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI timeText;
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            progressImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
            timeText.text = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTimer()).ToString();
        }

    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePlayingState()) {
            show();
        }
    }
    private void show() { uiParent.SetActive(true); }
    private void hide() { uiParent.SetActive(false);}
}
