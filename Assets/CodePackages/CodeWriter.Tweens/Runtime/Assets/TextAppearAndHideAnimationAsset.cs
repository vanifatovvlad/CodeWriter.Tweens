using System.Collections.Generic;
#if TMP_PRESENT
using TMPro;
#endif
using UnityEngine;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Custom/Text Appear And Hide")]
    public class TextAppearAndHideAnimationAsset : TweenAsset
    {
        public string m_text = DEFAULT_INJECT;

        [Header("Delays")]
        public float m_preStartDelay = 0;
        public float m_middleDelay = 0.5f;

        [Header("Durations")]
        public float m_showDuration = 0.5f;
        public float m_hideDuration = 0.5f;

        [Header("Easings")]
        public EasingType m_showEasing = EasingType.Linear;
        public EasingType m_hideEasing = EasingType.Linear;

        [Header("Scales")]
        public Vector3 m_startScale = Vector3.one;
        public Vector3 m_middleScale = Vector3.one;
        public Vector3 m_endScale = Vector3.one;

        [Header("Position (relative)")]
        public Vector2 m_startPosition;
        public Vector2 m_middlePosition;
        public Vector2 m_endPosition;

        public override string Title
        {
            get { return "Text Appear and Hide"; }
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
#if TMP_PRESENT
            var text = (TMP_Text)injector.GetInjection<TMP_Text>(m_text);
            var rect = (RectTransform)text.transform;

            var initScale = rect.localScale;
            var initPos = rect.anchoredPosition;

            var startPos = initPos + m_startPosition;
            var midPos = initPos + m_middlePosition;
            var endPos = initPos + m_endPosition;

            return Tween.sequence(
                Tween.delay(m_preStartDelay),
                Tween.parallel(
                    Tween.fromTo(0, 1, m_showDuration, m_showEasing.GetEase(), v => text.alpha = v),
                    Tween.moveRectFromTo(rect, startPos, midPos, m_showDuration, m_showEasing.GetEase()),
                    Tween.scaleFromTo(rect, m_startScale, m_middleScale, m_showDuration, m_showEasing.GetEase())
                ),
                Tween.delay(m_middleDelay),
                Tween.parallel(
                    Tween.fromTo(1, 0, m_hideDuration, m_hideEasing.GetEase(), v => text.alpha = v),
                    Tween.moveRectFromTo(rect, midPos, endPos, m_hideDuration, m_hideEasing.GetEase()),
                    Tween.scaleFromTo(rect, m_middleScale, m_endScale, m_hideDuration, m_hideEasing.GetEase())
                ),
                Tween.invoke(() =>
                {
                    rect.anchoredPosition = initPos;
                    rect.localScale = initScale;
                })
            );
#else
            return Tween.idle();
#endif
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
#if TMP_PRESENT
            list.Add(new TweenAssetInjection(m_text, typeof(TMP_Text)));
#endif
        }
    }
}