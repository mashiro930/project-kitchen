using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private Vector3 originalScale;  // ��ť�ĳ�ʼ����

    // Start is called before the first frame update
    void Start()
    {
        originalScale = startButton.transform.localScale; // �����ʼ����ֵ

        startButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.SelectScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // �����ɫ�仯������Ч��
        AddButtonEffects(startButton);
        AddButtonEffects(quitButton);
    }

    // Ϊ��ť�����ɫ������Ч��
    private void AddButtonEffects(Button button)
    {
        // ��ӵ������Ч��
        button.onClick.AddListener(() =>
        {
            StartCoroutine(ButtonPressEffect(button));
        });
    }

    // ��ť����ʱ����Ч����Э��
    private IEnumerator ButtonPressEffect(Button button)
    {
        Transform buttonTransform = button.transform;
        Vector3 pressedScale = originalScale * 0.9f; // ����ʱ��С��90%
        buttonTransform.localScale = pressedScale;

        // �ȴ�0.1���ٻָ�ԭʼ��С
        yield return new WaitForSeconds(0.1f);
        buttonTransform.localScale = originalScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
