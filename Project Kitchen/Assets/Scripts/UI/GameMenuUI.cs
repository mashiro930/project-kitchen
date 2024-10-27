using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections; // 引入检测鼠标事件的命名空间

public class GameMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private Vector3 originalScale;  // 保存按钮初始缩放值

    void Start()
    {
        Debug.Log("GameMenuUI Start called");

        originalScale = startButton.transform.localScale; // 保存初始缩放值

        // 设置按钮点击事件
        startButton.onClick.AddListener(() =>
        {
            StartCoroutine(ButtonPressEffect(startButton));
            Loader.Load(Loader.Scene.SelectScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            StartCoroutine(ButtonPressEffect(quitButton));
            Application.Quit();
        });
    }

    // 实现鼠标悬浮进入事件的接口
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter detected");
        // 如果悬浮的是startButton
        if (eventData.pointerEnter == startButton.gameObject)
        {
            Debug.Log("鼠标悬浮在 Start 按钮上");
            startButton.transform.localScale = originalScale * 1.1f; // 放大到1.1倍
        }
        // 如果悬浮的是quitButton
        else if (eventData.pointerEnter == quitButton.gameObject)
        {
            Debug.Log("鼠标悬浮在 Quit 按钮上");
            quitButton.transform.localScale = originalScale * 1.1f; // 放大到1.1倍
        }
    }

    // 实现鼠标悬浮离开事件的接口
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit detected");
        // 当鼠标离开startButton时
        if (eventData.pointerEnter == startButton.gameObject)
        {
            Debug.Log("鼠标离开 Start 按钮");
            startButton.transform.localScale = originalScale; // 恢复原始大小
        }
        // 当鼠标离开quitButton时
        else if (eventData.pointerEnter == quitButton.gameObject)
        {
            Debug.Log("鼠标离开 Quit 按钮");
            quitButton.transform.localScale = originalScale; // 恢复原始大小
        }
    }

    // 按钮按下时的缩放效果协程
    private IEnumerator ButtonPressEffect(Button button)
    {
        button.transform.localScale = originalScale * 0.9f; // 按下时缩小到90%
        yield return new WaitForSeconds(0.1f); // 等待0.1秒
        button.transform.localScale = originalScale * 1.1f; // 恢复到悬浮时的放大状态
    }
}

