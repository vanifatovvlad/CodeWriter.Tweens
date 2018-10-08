using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public delegate float TweenEase(float elapsed, float start, float change, float duration);

    public abstract class TweenEasingBase<T> : TweenBase
    {
        private TweenEase m_easing = Easings.Linear;
        private float m_duration = 1f;
        private float m_elapsed;
        private T m_from;
        private T m_to;
        private Func<T> m_fromGetter;
        private Func<T> m_toGetter;

        public float duration
        {
            get { return m_duration; }
            set
            {
                CheckModifyRunningTween();

                if (value < 0)
                    throw new ArgumentOutOfRangeException("duration", "Duration must be greater or equals zero");

                m_duration = value;
            }
        }

        public TweenEase easing
        {
            get { return m_easing; }
            set
            {
                CheckModifyRunningTween();

                m_easing = value ?? Easings.Linear;
            }
        }

        public T from
        {
            get { return m_from; }
            set
            {
                CheckModifyRunningTween();
                m_from = value;
            }
        }

        public T to
        {
            get { return m_to; }
            set
            {
                CheckModifyRunningTween();
                m_to = value;
            }
        }

        public Func<T> fromGetter
        {
            get { return m_fromGetter; }
            set
            {
                CheckModifyRunningTween();
                m_fromGetter = value;
            }
        }

        public Func<T> toGetter
        {
            get { return m_toGetter; }
            set
            {
                CheckModifyRunningTween();
                m_toGetter = value;
            }
        }

        protected override void DoStart()
        {
            if (m_fromGetter != null)
                m_from = m_fromGetter();

            if (m_toGetter != null)
                m_to = m_toGetter();

            m_elapsed = 0f;
            DoUpdate(0);
        }

        protected override void DoForceComplete()
        {
            DoUpdate(1);
        }

        protected override void DoCancel()
        {

        }

        protected override bool DoStep(float deltaTime)
        {
            m_elapsed += deltaTime;
            float t = (m_elapsed < m_duration) ? m_easing(m_elapsed, 0, 1, m_duration) : 1;
            DoUpdate(t);
            return (m_elapsed >= m_duration);
        }

        protected abstract void DoUpdate(float t);
    }
}