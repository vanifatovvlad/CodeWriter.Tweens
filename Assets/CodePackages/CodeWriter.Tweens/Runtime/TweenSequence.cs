using System;

namespace CodeWriter.Tweens
{
    public class TweenSequence : TweenContainer
    {
        private TweenRepeat m_externRepeat;
        private int m_repeatCount = 1;
        private bool m_isInfinite = false;

        private int m_currentIndex;
        private ITweenControl m_currentTween;
        private int m_currentIteration;

        public TweenRepeat externRepeat
        {
            get { return m_externRepeat; }
            set
            {
                CheckModifyRunningTween();
                m_externRepeat = value;
            }
        }

        public int repeatCount
        {
            get { return m_repeatCount; }
            set
            {
                CheckModifyRunningTween();

                if (m_repeatCount <= 0)
                    throw new ArgumentOutOfRangeException("repeatCount", "RepeatCount must be greater than zero");

                m_repeatCount = value;
            }
        }

        public bool isInfinite
        {
            get { return m_isInfinite; }
            set
            {
                CheckModifyRunningTween();
                m_isInfinite = value;
            }
        }

        protected override void DoStart()
        {
            m_currentIndex = 0;
            m_currentTween = null;
            m_currentIteration = 1;
        }

        protected override void DoForceComplete()
        {
            for (int i = m_currentIndex; i < m_tweens.Count; i++)
            {
                m_tweens[i].Start();
                m_tweens[i].Complete();
            }
        }

        protected override void DoCancel()
        {
            for (int i = m_currentIndex; i < m_tweens.Count; i++)
            {
                m_tweens[i].Cancel();
            }
        }

        protected override bool DoStep(float deltaTime)
        {
            if (m_tweens.Count == 0)
                return true;

            var tween = m_tweens[m_currentIndex];

            if (tween != m_currentTween)
            {
                m_currentTween = tween;
                m_currentTween.Start();
            }

            m_currentTween.Step(deltaTime);

            if (!m_currentTween.isRunning)
            {
                m_currentTween = null;

                if (m_currentIndex < m_tweens.Count - 1)
                {
                    m_currentIndex++;
                }
                else
                {
                    if (isInfinite || m_currentIteration++ < repeatCount)
                    {
                        m_currentIndex = 0;

                        if (m_externRepeat != null)
                            m_externRepeat(m_currentIteration);
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}