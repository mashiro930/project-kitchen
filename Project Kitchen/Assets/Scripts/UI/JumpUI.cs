using System.Collections;
using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TextMeshProUGUI flashingText; // ������� TextMeshPro ���
    public float flashSpeed = 1f;        // ������˸�ٶ�
    public float minAlpha = 0f;          // ���͸����
    public float maxAlpha = 1f;          // ���͸����

    private bool isFlashing = true;

    void Start()
    {
        if (flashingText != null)
        {
            StartCoroutine(SmoothFlashText()); // ��ʼƽ����˸
        }
    }

    IEnumerator SmoothFlashText()
    {
        float alpha = maxAlpha;  // ��ʼ͸����Ϊ���
        bool fadingOut = true;   // ����͸���ȵݼ����ǵ���

        while (isFlashing)
        {
            // ���� fadingOut ��״̬�����ǵݼ�͸���Ȼ��ǵ���͸����
            if (fadingOut)
            {
                alpha -= Time.deltaTime * flashSpeed; // �𽥼���͸����
                if (alpha <= minAlpha)
                {
                    alpha = minAlpha;
                    fadingOut = false; // ͸���ȵ������ʱ����ʼ����
                }
            }
            else
            {
                alpha += Time.deltaTime * flashSpeed; // ������͸����
                if (alpha >= maxAlpha)
                {
                    alpha = maxAlpha;
                    fadingOut = true; // ͸���ȵ������ʱ����ʼ�ݼ�
                }
            }

            // �����ı�����ɫ͸����
            flashingText.color = new Color(flashingText.color.r, flashingText.color.g, flashingText.color.b, alpha);

            yield return null; // ÿ֡�ȴ�
        }
    }
}

