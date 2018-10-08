using System;

namespace CodeWriter.Tweens
{
    public delegate void TweenStart();
    public delegate void TweenRepeat(int repeat);
    public delegate void TweenComplete();
    public delegate void TweenCancel();
    public delegate void TweenUpdate(float progress);

    public abstract class TweenBase : ITweenControl
    {
        private bool m_isRunning;

        private TweenStart m_externStart;
        private TweenComplete m_externComplete;
        private TweenCancel m_externCancel;

        public bool isRunning
        {
            get { return m_isRunning; }
        }

        public TweenStart externStart
        {
            get { return m_externStart; }
            set
            {
                CheckModifyRunningTween();
                m_externStart = value;
            }
        }

        public TweenCancel externCancel
        {
            get { return m_externCancel; }
            set
            {
                CheckModifyRunningTween();
                m_externCancel = value;
            }
        }

        public TweenComplete externComplete
        {
            get { return m_externComplete; }
            set
            {
                CheckModifyRunningTween();
                m_externComplete = value;
            }
        }

        void ITweenControl.Start()
        {
            if (isRunning)
                throw new InvalidOperationException("Must not start running tween");

            m_isRunning = true;

            if (m_externStart != null)
                m_externStart();

            DoStart();
        }

        void ITweenControl.Step(float deltaTime)
        {
            if (!isRunning)
                return;

            bool completed = DoStep(deltaTime);
            if (completed)
            {
                m_isRunning = false;

                if (m_externComplete != null)
                    m_externComplete();
            }
        }

        public void Complete()
        {
            if (!m_isRunning)
                return;

            m_isRunning = false;

            DoForceComplete();

            if (m_externComplete != null)
                m_externComplete();
        }

        public void Cancel()
        {
            if (!m_isRunning)
                return;

            m_isRunning = false;

            DoCancel();

            if (m_externCancel != null)
                m_externCancel();
        }

        protected abstract void DoStart();
        protected abstract bool DoStep(float deltaTime);
        protected abstract void DoForceComplete();
        protected abstract void DoCancel();

        protected void CheckModifyRunningTween()
        {
            if (m_isRunning)
            {
                throw new InvalidOperationException("Must not modify running tween");
            }
        }
    }
}