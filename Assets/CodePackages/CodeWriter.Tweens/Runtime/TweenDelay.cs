using System;

namespace CodeWriter.Tweens
{
    public class TweenDelay : TweenBase
    {
        private float m_delay = 1f;
        private float m_elapsed;

        public float delay
        {
            get { return m_delay; }
            set
            {
                CheckModifyRunningTween();

                if (value < 0)
                    throw new ArgumentOutOfRangeException("delay", "Delay must be greated or equals zero");
                
                m_delay = value;
            }
        }

        protected override void DoStart()
        {
            m_elapsed = 0f;
        }

        protected override void DoForceComplete()
        {

        }

        protected override void DoCancel()
        {

        }

        protected override bool DoStep(float deltaTime)
        {
            m_elapsed += deltaTime;
            return (m_elapsed >= m_delay);
        }
    }
}