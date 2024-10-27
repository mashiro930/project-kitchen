using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private Vector3 originalMenuScale;
    private Vector3 originalLevelScale;
    private Vector3 originalRestartScale;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        originalMenuScale = menuButton.transform.localScale;
        originalLevelScale = levelButton.transform.localScale;
        originalRestartScale = restartButton.transform.localScale;

        // 为按钮添加点击事件和悬浮效果
        AddButtonEffects(menuButton, originalMenuScale, Loader.Scene.GameMenuScene);
        AddButtonEffects(levelButton, originalLevelScale, Loader.Scene.SelectScene);
        AddButtonEffects(restartButton, originalRestartScale);

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

    // 为按钮添加点击、悬浮效果
    private void AddButtonEffects(Button button, Vector3 originalScale, Loader.Scene? scene = null)
    {
        // 添加悬浮进入事件
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter(button, originalScale); });
        trigger.triggers.Add(entryEnter);

        // 添加悬浮离开事件
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(button, originalScale); });
        trigger.triggers.Add(entryExit);

        // 添加点击缩小效果
        button.onClick.AddListener(() =>
        {
            ButtonPressEffect(button, originalScale);

            // 如果需要加载场景
            if (scene != null)
            {
                Loader.Load(scene.Value);
            }
        });
    }

    // 鼠标悬浮时放大按钮
    private void OnPointerEnter(Button button, Vector3 originalScale)
    {
        button.transform.localScale = originalScale * 1.1f;  // 放大至1.1倍
    }

    // 鼠标离开时恢复原始大小
    private void OnPointerExit(Button button, Vector3 originalScale)
    {
        button.transform.localScale = originalScale;  // 恢复原始大小
    }

    // 点击按钮时缩小效果
    private void ButtonPressEffect(Button button, Vector3 originalScale)
    {
        button.transform.localScale = originalScale * 0.9f; // 点击时缩小到90%
    }

    // 平滑缩放按钮
    private IEnumerator ScaleButton(Button button, Vector3 targetScale)
    {
        Transform buttonTransform = button.transform;
        Vector3 currentScale = buttonTransform.localScale;
        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            buttonTransform.localScale = Vector3.Lerp(currentScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonTransform.localScale = targetScale;
    }
}
