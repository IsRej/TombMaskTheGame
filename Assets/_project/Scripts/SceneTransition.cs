using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class SceneTransition : MonoBehaviour
{

    public static SceneTransition Instance;

    [SerializeField] float AnimationDuration = 0.2f;
    [SerializeField] float AnimationDelay = 0.1f;
    [SerializeField] float XDisplacement = -2400f;
    [SerializeField] float AnimateInDelay = 1f;
    [SerializeField] RectTransform[] TransitionImages;

    void Awake()
    {
        Instance = this;
    }

    public void AnimateIn()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < TransitionImages.Length; i++) {
            TransitionImages[i].localPosition = new Vector3(0f, TransitionImages[i].localPosition.y, 0f);
            TransitionImages[i].DOLocalMoveX(XDisplacement, AnimationDuration).SetDelay(AnimateInDelay + (i * AnimationDelay));
        }
    }

    public void AnimateOut()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < TransitionImages.Length; i++) {
            TransitionImages[i].localPosition = new Vector3(-XDisplacement, TransitionImages[i].localPosition.y, 0f);
            TransitionImages[i].DOLocalMoveX(0f, AnimationDuration).SetDelay(i * AnimationDelay);
        }
    }
}
