using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 用于捕捉点击事件
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private Image Level1Image;  // 使用 Image 替代 Button
    [SerializeField] private Image Level2Image;

    [SerializeField] private Image Slime00;  
    [SerializeField] private Image Slime01;  
    [SerializeField] private Image Slime02;  
    [SerializeField] private Image Slime03;  
    [SerializeField] private Image Slime04;  
    [SerializeField] private Image Slime05;  

    private Vector3 originalScale1;  // 保存图片的初始缩放
    private Vector3 originalScale2;

    private Vector3 originalScale00;
    private Vector3 originalScale01;
    private Vector3 originalScale02;
    private Vector3 originalScale03;
    private Vector3 originalScale04;
    private Vector3 originalScale05;

    // Dictionary to store original scale of each Slime
    private Dictionary<Image, Vector3> slimeOriginalScales = new Dictionary<Image, Vector3>();
    private Dictionary<Image, Vector3> levelOriginalScales = new Dictionary<Image, Vector3>();
    private Image currentlySelectedSlime;  // 保存当前被选中的 Slime

    // Start is called before the first frame update
    void Start()
    {
        // 保存每个 Level 图片的初始缩放值
        levelOriginalScales[Level1Image] = Level1Image.transform.localScale;
        levelOriginalScales[Level2Image] = Level2Image.transform.localScale;

        // 保存每个 Slime 的初始缩放值
        slimeOriginalScales[Slime00] = Slime00.transform.localScale;
        slimeOriginalScales[Slime01] = Slime01.transform.localScale;
        slimeOriginalScales[Slime02] = Slime02.transform.localScale;
        slimeOriginalScales[Slime03] = Slime03.transform.localScale;
        slimeOriginalScales[Slime04] = Slime04.transform.localScale;
        slimeOriginalScales[Slime05] = Slime05.transform.localScale;

        // 为每张 Level 图片添加点击和悬浮事件
        AddLevelEffects(Level1Image, Loader.Scene.Level1Scene);
        AddLevelEffects(Level2Image, Loader.Scene.Level2Scene);

        // 为每张图片添加点击事件
        AddSlimeEffects(Slime00);
        AddSlimeEffects(Slime01);
        AddSlimeEffects(Slime02);
        AddSlimeEffects(Slime03);
        AddSlimeEffects(Slime04);
        AddSlimeEffects(Slime05);
    }

    // 为图片添加点击事件和缩放效果
    private void AddLevelEffects(Image levelImage, Loader.Scene scene)
    {
        // 为每张图片添加事件触发器
        EventTrigger trigger = levelImage.gameObject.AddComponent<EventTrigger>();

        // 设置点击事件
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnImageClick(levelImage, scene); });
        trigger.triggers.Add(entryClick);

        // 设置悬浮事件
        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnPointerEnter(levelImage); });
        trigger.triggers.Add(entryHover);

        // 设置离开事件
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(levelImage); });
        trigger.triggers.Add(entryExit);
    }

    // 为每个 Slime 添加点击事件和缩放效果
    private void AddSlimeEffects(Image slime)
    {
        // 为每张图片添加点击事件
        EventTrigger trigger = slime.gameObject.AddComponent<EventTrigger>();

        // 设置点击事件
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnSlimeClick(slime); });
        trigger.triggers.Add(entryClick);

        // 设置悬浮事件
        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnPointerEnter(slime); });
        trigger.triggers.Add(entryHover);

        // 设置离开事件
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(slime); });
        trigger.triggers.Add(entryExit);
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

    // Slime 点击处理逻辑
    private void OnSlimeClick(Image slime)
    {
        Debug.Log("Slime clicked: " + slime.gameObject.name);

        // 如果已经有选中的 Slime，先将它恢复到原始大小
        if (currentlySelectedSlime != null)
        {
            currentlySelectedSlime.transform.localScale = slimeOriginalScales[currentlySelectedSlime];  // 恢复原始大小
        }

        // 开始缩小-放大的协程
        StartCoroutine(SlimeShrinkAndGrow(slime));

        // 更新为新的选中 Slime
        currentlySelectedSlime = slime;
    }

    // 协程：先缩小，再放大
    private IEnumerator SlimeShrinkAndGrow(Image slime)
    {
        // 先缩小到 0.8 倍
        slime.transform.localScale = slimeOriginalScales[slime] * 0.8f;

        // 等待 0.1 秒
        yield return new WaitForSeconds(0.1f);

        // 再放大到 1.2 倍
        slime.transform.localScale = slimeOriginalScales[slime] * 1.4f;
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

    // 悬浮进入时放大
    private void OnPointerEnter(Image image)
    {
        // 如果悬浮的 Slime 已经被选中，不需要放大
        if (currentlySelectedSlime == image)
        {
            return;
        }

        Debug.Log("Hovering over: " + image.gameObject.name);
        image.transform.localScale = image.transform.localScale * 1.1f;  // 放大到 1.1 倍
    }

    // 悬浮离开时恢复原大小
    private void OnPointerExit(Image image)
    {
        // 如果离开的 Slime 已经被选中，不需要恢复
        if (currentlySelectedSlime == image)
        {
            return;
        }

        Debug.Log("Pointer exit from: " + image.gameObject.name);

        // 根据是否是 Level 或 Slime 恢复到相应的原始大小
        if (slimeOriginalScales.ContainsKey(image))
        {
            image.transform.localScale = slimeOriginalScales[image];  // 恢复 Slime 原大小
        }
        else if (levelOriginalScales.ContainsKey(image))
        {
            image.transform.localScale = levelOriginalScales[image];  // 恢复 Level 图片原大小
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
