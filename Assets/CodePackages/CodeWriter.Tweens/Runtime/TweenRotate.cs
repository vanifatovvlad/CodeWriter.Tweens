using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public class TweenRotate : TweenEasingBase<Quaternion>
    {
        private Transform m_transform;
        private Space m_space = Space.Self;

        private float m_angle;
        private Vector3 m_axis;

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

        public TweenRotate(Transform transform)
        {
            if (transform == null)
                throw new ArgumentNullException("transform");

            m_transform = transform;
        }

        protected override void DoStart()
        {
            base.DoStart();
            
            Quaternion.RotateTowards(Quaternion.identity, Quaternion.Inverse(from) * to, 360).ToAngleAxis(out m_angle, out m_axis);
        }

        protected override void DoUpdate(float t)
        {
            var value = from * Quaternion.AngleAxis(m_angle * t, m_axis);
            //var value = Quaternion.Slerp(from, to, t);

            switch (m_space)
            {
                case Space.World:
                    m_transform.rotation = value;
                    break;

                case Space.Self:
                    m_transform.localRotation = value;
                    break;
            }
        }
    }
}