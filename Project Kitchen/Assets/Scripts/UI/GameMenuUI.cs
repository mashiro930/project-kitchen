using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private Vector3 originalScale;  // 按钮的初始缩放

    // Start is called before the first frame update
    void Start()
    {
        originalScale = startButton.transform.localScale; // 保存初始缩放值

        startButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.SelectScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // 添加颜色变化和缩放效果
        AddButtonEffects(startButton);
        AddButtonEffects(quitButton);
    }

    // 为按钮添加颜色和缩放效果
    private void AddButtonEffects(Button button)
    {
        // 添加点击缩放效果
        button.onClick.AddListener(() =>
        {
            StartCoroutine(ButtonPressEffect(button));
        });
    }

    // 按钮按下时缩放效果的协程
    private IEnumerator ButtonPressEffect(Button button)
    {
        Transform buttonTransform = button.transform;
        Vector3 pressedScale = originalScale * 0.9f; // 按下时缩小到90%
        buttonTransform.localScale = pressedScale;

        // 等待0.1秒再恢复原始大小
        yield return new WaitForSeconds(0.1f);
        buttonTransform.localScale = originalScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
