using UnityEngine;
using DG.Tweening;

public class PendulumEffect : MonoBehaviour
{
    public float duration = 1f; 
    public float angle = 30f; 

    void Start()
    {
        DOTween.To(() => transform.eulerAngles.z, x => transform.eulerAngles = new Vector3(0, 0, x), angle, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo); 
    }
}

