using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 用于捕捉点击事件
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private Image Level1Image;  // 使用 Image 替代 Button
    [SerializeField] private Image Level2Image;

    private Vector3 originalScale1;  // 保存图片的初始缩放
    private Vector3 originalScale2;

    // Start is called before the first frame update
    void Start()
    {
        originalScale1 = Level1Image.transform.localScale; // 保存初始缩放值
        originalScale2 = Level2Image.transform.localScale;

        // 添加点击事件
        AddImageEffects(Level1Image, Loader.Scene.Level1Scene);
        AddImageEffects(Level2Image, Loader.Scene.Level2Scene);
    }

    // 为图片添加点击事件和缩放效果
    private void AddImageEffects(Image image, Loader.Scene scene)
    {
        // 为每张图片添加点击事件
        EventTrigger trigger = image.gameObject.AddComponent<EventTrigger>();

        // 设置点击事件
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnImageClick(image, scene); });
        trigger.triggers.Add(entryClick);
    }

    // 图片点击处理逻辑
    private void OnImageClick(Image image, Loader.Scene scene)
    {
        Debug.Log("Image clicked, loading scene: " + scene);

        // 调用 Loader 来加载对应的场景
        Loader.Load(scene);  // 传递枚举类型

        // 触发缩放效果
        StartCoroutine(ImagePressEffect(image));
    }

    // 图片按下时的缩放效果
    private IEnumerator ImagePressEffect(Image image)
    {
        Transform imageTransform = image.transform;
        Vector3 originalScale = imageTransform.localScale;
        Vector3 pressedScale = originalScale * 0.9f; // 按下时缩小到90%
        imageTransform.localScale = pressedScale;

        // 等待0.1秒再恢复原始大小
        yield return new WaitForSeconds(0.1f);
        imageTransform.localScale = originalScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
