using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button settingsButton;

    private Vector3 originalScaleResume;
    private Vector3 originalScaleMenu;
    private Vector3 originalScaleSettings;

    private void Start()
    {
        // 保存按钮的初始缩放值
        originalScaleResume = resumeButton.transform.localScale;
        originalScaleMenu = menuButton.transform.localScale;
        originalScaleSettings = settingsButton.transform.localScale;

        Hide();
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGame();
            StartCoroutine(ButtonPressEffect(resumeButton, originalScaleResume));
        });
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameMenuScene);
            StartCoroutine(ButtonPressEffect(menuButton, originalScaleMenu));
        });
        settingsButton.onClick.AddListener(() =>
        {
            SettingsUI.Instance.Show();
            StartCoroutine(ButtonPressEffect(settingsButton, originalScaleSettings));
        });

        // 为按钮添加悬浮效果
        AddButtonHoverEffect(resumeButton, originalScaleResume);
        AddButtonHoverEffect(menuButton, originalScaleMenu);
        AddButtonHoverEffect(settingsButton, originalScaleSettings);
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        uiParent.SetActive(true);
    } 

    private void Hide()
    {
        uiParent.SetActive(false);
    }

    // 为按钮添加悬浮放大效果
    private void AddButtonHoverEffect(Button button, Vector3 originalScale)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // 悬浮进入事件
        EventTrigger.Entry entryHoverEnter = new EventTrigger.Entry();
        entryHoverEnter.eventID = EventTriggerType.PointerEnter;
        entryHoverEnter.callback.AddListener((data) => { OnPointerEnter(button, originalScale); });
        trigger.triggers.Add(entryHoverEnter);

        // 悬浮离开事件
        EventTrigger.Entry entryHoverExit = new EventTrigger.Entry();
        entryHoverExit.eventID = EventTriggerType.PointerExit;
        entryHoverExit.callback.AddListener((data) => { OnPointerExit(button, originalScale); });
        trigger.triggers.Add(entryHoverExit);
    }

    // 悬浮时放大
    private void OnPointerEnter(Button button, Vector3 originalScale)
    {
        button.transform.localScale = originalScale * 1.1f;  // 放大到1.1倍
    }

    // 鼠标离开时恢复原始大小
    private void OnPointerExit(Button button, Vector3 originalScale)
    {
        button.transform.localScale = originalScale;
    }

    // 点击时缩小再恢复
    private IEnumerator ButtonPressEffect(Button button, Vector3 originalScale)
    {
        button.transform.localScale = originalScale * 0.9f;  // 缩小到90%
        yield return new WaitForSeconds(0.1f);
        button.transform.localScale = originalScale;  // 恢复原始大小
    }
}
