using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Scale Settings")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 clickScale = new Vector3(0.9f, 0.9f, 1f);
    public float animationSpeed = 10f;

    private Vector3 targetScale;

    void Start()
    {
        targetScale = normalScale;
        transform.localScale = normalScale;
    }

    void Update()
    {
        // Smoothly animate towards target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Briefly shrink on click, then return to hover/normal
        StopAllCoroutines();
        StartCoroutine(ClickEffect());
    }

    private System.Collections.IEnumerator ClickEffect()
    {
        targetScale = clickScale;
        yield return new WaitForSeconds(0.1f);
        targetScale = hoverScale; // or normalScale if you prefer
    }
}