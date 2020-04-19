using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using CharTween;

public class FloatingScore : MonoBehaviour
{
    public Color startingColour;
    public float distance, duration;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void Init(int score)
    {
        text.SetText(string.Format("+{0}", score));
        var tweener = text.GetCharTweener();

        transform.DOLocalMoveY(distance, duration);

        for (int i = 0; i < tweener.CharacterCount; ++i)
        {
            tweener.SetColor(i, Color.white);

            var circleTween = tweener.DOCircle(i, 0.01f, 0.5f)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);

            var colourTween = tweener.DOColor(i, Color.yellow, 0.25f)
                .SetLoops(-1, LoopType.Yoyo);

            var fadeTween = tweener.DOColor(i, Color.clear, 1.0f).SetDelay(duration).OnComplete(() => Kill(tweener));

            // Offset animations based on character index in string
            var timeOffset = Mathf.Lerp(0, 1, i / (float)(tweener.CharacterCount - 1));
            circleTween.fullPosition = timeOffset;
            colourTween.fullPosition = timeOffset;
        }
    }

    private void Kill(CharTweener tweener)
    {
        tweener.DOKill(true);
        text.SetText("NO_TEXT");
        Destroy(this.gameObject);
    }
}
