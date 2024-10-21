using System.Collections;
using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TextMeshProUGUI flashingText; // 引用你的 TextMeshPro 组件
    public float flashSpeed = 1f;        // 控制闪烁速度
    public float minAlpha = 0f;          // 最低透明度
    public float maxAlpha = 1f;          // 最高透明度

    private bool isFlashing = true;

    void Start()
    {
        if (flashingText != null)
        {
            StartCoroutine(SmoothFlashText()); // 开始平滑闪烁
        }
    }

    IEnumerator SmoothFlashText()
    {
        float alpha = maxAlpha;  // 初始透明度为最高
        bool fadingOut = true;   // 控制透明度递减还是递增

        while (isFlashing)
        {
            // 根据 fadingOut 的状态决定是递减透明度还是递增透明度
            if (fadingOut)
            {
                alpha -= Time.deltaTime * flashSpeed; // 逐渐减少透明度
                if (alpha <= minAlpha)
                {
                    alpha = minAlpha;
                    fadingOut = false; // 透明度到达最低时，开始递增
                }
            }
            else
            {
                alpha += Time.deltaTime * flashSpeed; // 逐渐增加透明度
                if (alpha >= maxAlpha)
                {
                    alpha = maxAlpha;
                    fadingOut = true; // 透明度到达最高时，开始递减
                }
            }

            // 设置文本的颜色透明度
            flashingText.color = new Color(flashingText.color.r, flashingText.color.g, flashingText.color.b, alpha);

            yield return null; // 每帧等待
        }
    }
}

