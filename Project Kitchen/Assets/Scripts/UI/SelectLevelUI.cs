using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // ���ڲ�׽����¼�
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private Image Level1Image;  // ʹ�� Image ��� Button
    [SerializeField] private Image Level2Image;

    [SerializeField] private Image Slime00;  
    [SerializeField] private Image Slime01;  
    [SerializeField] private Image Slime02;  
    [SerializeField] private Image Slime03;  
    [SerializeField] private Image Slime04;  
    [SerializeField] private Image Slime05;  

    private Vector3 originalScale1;  // ����ͼƬ�ĳ�ʼ����
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
    private Image currentlySelectedSlime;  // ���浱ǰ��ѡ�е� Slime

    // Start is called before the first frame update
    void Start()
    {
        // ����ÿ�� Level ͼƬ�ĳ�ʼ����ֵ
        levelOriginalScales[Level1Image] = Level1Image.transform.localScale;
        levelOriginalScales[Level2Image] = Level2Image.transform.localScale;

        // ����ÿ�� Slime �ĳ�ʼ����ֵ
        slimeOriginalScales[Slime00] = Slime00.transform.localScale;
        slimeOriginalScales[Slime01] = Slime01.transform.localScale;
        slimeOriginalScales[Slime02] = Slime02.transform.localScale;
        slimeOriginalScales[Slime03] = Slime03.transform.localScale;
        slimeOriginalScales[Slime04] = Slime04.transform.localScale;
        slimeOriginalScales[Slime05] = Slime05.transform.localScale;

        // Ϊÿ�� Level ͼƬ��ӵ���������¼�
        AddLevelEffects(Level1Image, Loader.Scene.Level1Scene);
        AddLevelEffects(Level2Image, Loader.Scene.Level2Scene);

        // Ϊÿ��ͼƬ��ӵ���¼�
        AddSlimeEffects(Slime00);
        AddSlimeEffects(Slime01);
        AddSlimeEffects(Slime02);
        AddSlimeEffects(Slime03);
        AddSlimeEffects(Slime04);
        AddSlimeEffects(Slime05);
    }

    // ΪͼƬ��ӵ���¼�������Ч��
    private void AddLevelEffects(Image levelImage, Loader.Scene scene)
    {
        // Ϊÿ��ͼƬ����¼�������
        EventTrigger trigger = levelImage.gameObject.AddComponent<EventTrigger>();

        // ���õ���¼�
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnImageClick(levelImage, scene); });
        trigger.triggers.Add(entryClick);

        // ���������¼�
        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnPointerEnter(levelImage); });
        trigger.triggers.Add(entryHover);

        // �����뿪�¼�
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(levelImage); });
        trigger.triggers.Add(entryExit);
    }

    // Ϊÿ�� Slime ��ӵ���¼�������Ч��
    private void AddSlimeEffects(Image slime)
    {
        // Ϊÿ��ͼƬ��ӵ���¼�
        EventTrigger trigger = slime.gameObject.AddComponent<EventTrigger>();

        // ���õ���¼�
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnSlimeClick(slime); });
        trigger.triggers.Add(entryClick);

        // ���������¼�
        EventTrigger.Entry entryHover = new EventTrigger.Entry();
        entryHover.eventID = EventTriggerType.PointerEnter;
        entryHover.callback.AddListener((data) => { OnPointerEnter(slime); });
        trigger.triggers.Add(entryHover);

        // �����뿪�¼�
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(slime); });
        trigger.triggers.Add(entryExit);
    }

    // ͼƬ��������߼�
    private void OnImageClick(Image image, Loader.Scene scene)
    {
        Debug.Log("Image clicked, loading scene: " + scene);

        // ���� Loader �����ض�Ӧ�ĳ���
        Loader.Load(scene);  // ����ö������

        // ��������Ч��
        StartCoroutine(ImagePressEffect(image));
    }

    // Slime ��������߼�
    private void OnSlimeClick(Image slime)
    {
        Debug.Log("Slime clicked: " + slime.gameObject.name);

        // ����Ѿ���ѡ�е� Slime���Ƚ����ָ���ԭʼ��С
        if (currentlySelectedSlime != null)
        {
            currentlySelectedSlime.transform.localScale = slimeOriginalScales[currentlySelectedSlime];  // �ָ�ԭʼ��С
        }

        // ��ʼ��С-�Ŵ��Э��
        StartCoroutine(SlimeShrinkAndGrow(slime));

        // ����Ϊ�µ�ѡ�� Slime
        currentlySelectedSlime = slime;
    }

    // Э�̣�����С���ٷŴ�
    private IEnumerator SlimeShrinkAndGrow(Image slime)
    {
        // ����С�� 0.8 ��
        slime.transform.localScale = slimeOriginalScales[slime] * 0.8f;

        // �ȴ� 0.1 ��
        yield return new WaitForSeconds(0.1f);

        // �ٷŴ� 1.2 ��
        slime.transform.localScale = slimeOriginalScales[slime] * 1.4f;
    }

    // ͼƬ����ʱ������Ч��
    private IEnumerator ImagePressEffect(Image image)
    {
        Transform imageTransform = image.transform;
        Vector3 originalScale = imageTransform.localScale;
        Vector3 pressedScale = originalScale * 0.9f; // ����ʱ��С��90%
        imageTransform.localScale = pressedScale;

        // �ȴ�0.1���ٻָ�ԭʼ��С
        yield return new WaitForSeconds(0.1f);
        imageTransform.localScale = originalScale;
    }

    // ��������ʱ�Ŵ�
    private void OnPointerEnter(Image image)
    {
        // ��������� Slime �Ѿ���ѡ�У�����Ҫ�Ŵ�
        if (currentlySelectedSlime == image)
        {
            return;
        }

        Debug.Log("Hovering over: " + image.gameObject.name);
        image.transform.localScale = image.transform.localScale * 1.1f;  // �Ŵ� 1.1 ��
    }

    // �����뿪ʱ�ָ�ԭ��С
    private void OnPointerExit(Image image)
    {
        // ����뿪�� Slime �Ѿ���ѡ�У�����Ҫ�ָ�
        if (currentlySelectedSlime == image)
        {
            return;
        }

        Debug.Log("Pointer exit from: " + image.gameObject.name);

        // �����Ƿ��� Level �� Slime �ָ�����Ӧ��ԭʼ��С
        if (slimeOriginalScales.ContainsKey(image))
        {
            image.transform.localScale = slimeOriginalScales[image];  // �ָ� Slime ԭ��С
        }
        else if (levelOriginalScales.ContainsKey(image))
        {
            image.transform.localScale = levelOriginalScales[image];  // �ָ� Level ͼƬԭ��С
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
