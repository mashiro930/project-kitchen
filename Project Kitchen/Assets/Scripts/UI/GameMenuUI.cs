using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections; // ����������¼��������ռ�

public class GameMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private Vector3 originalScale;  // ���水ť��ʼ����ֵ

    void Start()
    {
        Debug.Log("GameMenuUI Start called");

        originalScale = startButton.transform.localScale; // �����ʼ����ֵ

        // ���ð�ť����¼�
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

    // ʵ��������������¼��Ľӿ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter detected");
        // �����������startButton
        if (eventData.pointerEnter == startButton.gameObject)
        {
            Debug.Log("��������� Start ��ť��");
            startButton.transform.localScale = originalScale * 1.1f; // �Ŵ�1.1��
        }
        // �����������quitButton
        else if (eventData.pointerEnter == quitButton.gameObject)
        {
            Debug.Log("��������� Quit ��ť��");
            quitButton.transform.localScale = originalScale * 1.1f; // �Ŵ�1.1��
        }
    }

    // ʵ����������뿪�¼��Ľӿ�
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit detected");
        // ������뿪startButtonʱ
        if (eventData.pointerEnter == startButton.gameObject)
        {
            Debug.Log("����뿪 Start ��ť");
            startButton.transform.localScale = originalScale; // �ָ�ԭʼ��С
        }
        // ������뿪quitButtonʱ
        else if (eventData.pointerEnter == quitButton.gameObject)
        {
            Debug.Log("����뿪 Quit ��ť");
            quitButton.transform.localScale = originalScale; // �ָ�ԭʼ��С
        }
    }

    // ��ť����ʱ������Ч��Э��
    private IEnumerator ButtonPressEffect(Button button)
    {
        button.transform.localScale = originalScale * 0.9f; // ����ʱ��С��90%
        yield return new WaitForSeconds(0.1f); // �ȴ�0.1��
        button.transform.localScale = originalScale * 1.1f; // �ָ�������ʱ�ķŴ�״̬
    }
}

