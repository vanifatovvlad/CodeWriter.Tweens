using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public class TweenMove : TweenEasingBase<Vector3>
    {
        private Transform m_transform;
        private Space m_space = Space.Self;

        public Transform transform
        {
            get { return m_transform; }
        }

        public Space space
        {
            get { return m_space; }
            set
            {
                CheckModifyRunningTween();
                m_space = value;
            }
        }

        public TweenMove(Transform transform)
        {
            if (transform == null)
                throw new ArgumentNullException("transform");

            m_transform = transform;
        }

        protected override void DoUpdate(float t)
        {
            var value = from + (to - from) * t;

            switch (m_space)
            {
                case Space.World:
                    m_transform.position = value;
                    break;

                case Space.Self:
                    m_transform.localPosition = value;
                    break;
            }
        }
    }
}