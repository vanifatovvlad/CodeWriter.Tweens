using System;
using System.Collections.Generic;

namespace CodeWriter.Tweens
{
    public sealed class Tweener : IDisposable
    {
        private readonly Queue<ITweenControl> m_queue = new Queue<ITweenControl>();
        private readonly List<ITweenControl> m_parallel = new List<ITweenControl>();
        private ITweenControl m_currentEnqueued;
        private bool m_updating;

        public Tweener()
        {
            TweenPerformer.AddTweener(this);
        }

        public void Dispose()
        {
            CancelTweens();
            TweenPerformer.RemoveTweener(this);
        }

        public void StartTween(TweenBase tween, TweenStartMode startMode = TweenStartMode.Immediate)
        {
            if (tween == null)
                throw new ArgumentNullException("tween");

            var tweenControl = (ITweenControl)tween;

            if (startMode == TweenStartMode.Immediate)
            {
                m_parallel.Add(tweenControl);
                tweenControl.Start();
            }
            else
            {
                m_queue.Enqueue(tweenControl);
            }
        }

        public void CompleteTweens()
        {
            if (m_updating)
                throw new InvalidOperationException("Trying to complete tweens inside tween");

            PerformOnTweensAndClear(t => t.Complete());
        }

        public void CancelTweens()
        {
            if (m_updating)
                throw new InvalidOperationException("Trying to cancel tweens inside tween");

            PerformOnTweensAndClear(t => t.Cancel());
        }

        internal void Update(float deltaTime)
        {
            m_updating = true;
            UpdateQueued(deltaTime);
            UpdateUnqueued(deltaTime);
            m_updating = false;
        }

        private void PerformOnTweensAndClear(Action<ITweenControl> action)
        {
            if (m_currentEnqueued != null)
            {
                action(m_currentEnqueued);
                m_currentEnqueued = null;
            }

            foreach (var tween in m_queue)
            {
                action(tween);
            }
            m_queue.Clear();

            foreach (var yween in m_parallel)
            {
                action(yween);
            }
            m_parallel.Clear();
        }

        private void UpdateQueued(float deltaTime)
        {
            if (m_currentEnqueued == null)
            {
                if (m_queue.Count > 0)
                {
                    m_currentEnqueued = m_queue.Dequeue();
                    m_currentEnqueued.Start();
                    m_currentEnqueued.Step(deltaTime);
                }
            }
            else
            {
                m_currentEnqueued.Step(deltaTime);

                if (!m_currentEnqueued.isRunning)
                {
                    m_currentEnqueued = null;
                }
            }
        }

        private void UpdateUnqueued(float deltaTime)
        {
            for (int i = 0; i < m_parallel.Count;)
            {
                var tween = m_parallel[i];

                tween.Step(deltaTime);

                if (!tween.isRunning)
                {
                    m_parallel.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}