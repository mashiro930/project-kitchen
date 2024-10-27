using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // ���ڲ�׽����¼�
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField] private Image Level1Image;  // ʹ�� Image ��� Button
    [SerializeField] private Image Level2Image;

    private Vector3 originalScale1;  // ����ͼƬ�ĳ�ʼ����
    private Vector3 originalScale2;

    // Start is called before the first frame update
    void Start()
    {
        originalScale1 = Level1Image.transform.localScale; // �����ʼ����ֵ
        originalScale2 = Level2Image.transform.localScale;

        // ��ӵ���¼�
        AddImageEffects(Level1Image, Loader.Scene.Level1Scene);
        AddImageEffects(Level2Image, Loader.Scene.Level2Scene);
    }

    // ΪͼƬ��ӵ���¼�������Ч��
    private void AddImageEffects(Image image, Loader.Scene scene)
    {
        // Ϊÿ��ͼƬ��ӵ���¼�
        EventTrigger trigger = image.gameObject.AddComponent<EventTrigger>();

        // ���õ���¼�
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnImageClick(image, scene); });
        trigger.triggers.Add(entryClick);
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

    // Update is called once per frame
    void Update()
    {

    }
}
