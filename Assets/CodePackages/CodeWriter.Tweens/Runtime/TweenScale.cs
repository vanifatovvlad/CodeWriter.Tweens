using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public class TweenScale : TweenEasingBase<Vector3>
    {
        private Transform m_transform;

        public Transform transform
        {
            get { return m_transform; }
        }

        public TweenScale(Transform transform)
        {
            if (transform == null)
                throw new ArgumentNullException("transform");

            m_transform = transform;
        }

        protected override void DoUpdate(float t)
        {
            m_transform.localScale = from + (to - from) * t;
        }
    }
}