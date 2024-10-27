using UnityEngine;
using UnityEngine.EventSystems;

public class TestPointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter: " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit: " + gameObject.name);
    }
}
