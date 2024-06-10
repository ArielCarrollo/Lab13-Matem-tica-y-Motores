using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ClickEffect : MonoBehaviour, IPointerClickHandler
{
    public float targetScale = 1.2f;
    public float duration = 0.2f;

    private Vector3 initialScale; 

    void Start()
    {
        initialScale = transform.localScale; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(targetScale, duration) 
            .OnComplete(() =>
            {
                transform.DOScale(initialScale, duration); 
                GetComponent<SpriteRenderer>().DOFade(0, duration) 
                    .OnComplete(() => GetComponent<SpriteRenderer>().DOFade(1, duration)); 
            });
    }
}

