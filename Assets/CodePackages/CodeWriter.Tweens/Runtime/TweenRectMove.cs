using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public class TweenRectMove : TweenEasingBase<Vector2>
    {
        private RectTransform m_rectTransform;

        public RectTransform rectTransform
        {
            get { return m_rectTransform; }
        }

        public TweenRectMove(RectTransform rectTransform)
        {
            if (rectTransform == null)
                throw new ArgumentNullException("rectTransform");

            m_rectTransform = rectTransform;
        }

        protected override void DoUpdate(float t)
        {
            m_rectTransform.anchoredPosition = from + (to - from) * t;
        }
    }
}