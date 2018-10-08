using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public static partial class Tween
    {
        public static TweenMove moveFromTo(Transform transform,
                                           Func<Vector3> from,
                                           Func<Vector3> to,
                                           float duration = 1f,
                                           Space space = Space.Self,
                                           TweenEase easing = null,
                                           TweenStart onStart = null,
                                           TweenComplete onComplete = null,
                                           TweenCancel onCancel = null)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (to == null)
                throw new ArgumentNullException("to");

            return new TweenMove(transform)
            {
                fromGetter = from,
                toGetter = to,
                easing = easing,
                space = space,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenMove moveFromTo(Transform transform,
                                           Vector3 from,
                                           Vector3 to,
                                           float duration = 1f,
                                           Space space = Space.Self,
                                           TweenEase easing = null,
                                           TweenStart onStart = null,
                                           TweenComplete onComplete = null,
                                           TweenCancel onCancel = null)
        {
            return new TweenMove(transform)
            {
                from = from,
                to = to,
                easing = easing,
                space = space,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenMove moveTo(Transform transform,
                                       Vector3 to,
                                       float duration = 1f,
                                       Space space = Space.Self,
                                       TweenEase easing = null,
                                       TweenStart onStart = null,
                                       TweenComplete onComplete = null,
                                       TweenCancel onCancel = null)
        {
            var tween = new TweenMove(transform)
            {
                to = to,
                easing = easing,
                duration = duration,
                space = space,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
            tween.fromGetter = () => tween.space == Space.Self ? tween.transform.localPosition : tween.transform.position;
            return tween;
        }

        public static TweenRectMove moveRectFromTo(RectTransform rectTransform,
                                                   Func<Vector2> from,
                                                   Func<Vector2> to,
                                                   float duration = 1f,
                                                   TweenEase easing = null,
                                                   TweenStart onStart = null,
                                                   TweenComplete onComplete = null,
                                                   TweenCancel onCancel = null)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (to == null)
                throw new ArgumentNullException("to");

            return new TweenRectMove(rectTransform)
            {
                fromGetter = from,
                toGetter = to,
                easing = easing,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }
        
        public static TweenRectMove moveRectFromTo(RectTransform rectTransform,
                                                   Vector2 from,
                                                   Vector2 to,
                                                   float duration = 1f,
                                                   TweenEase easing = null,
                                                   TweenStart onStart = null,
                                                   TweenComplete onComplete = null,
                                                   TweenCancel onCancel = null)
        {
            return new TweenRectMove(rectTransform)
            {
                from = from,
                to = to,
                easing = easing,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenRectMove moveRectTo(RectTransform rectTransform,
                                               Vector2 to,
                                               float duration = 1f,
                                               TweenEase easing = null,
                                               TweenStart onStart = null,
                                               TweenComplete onComplete = null,
                                               TweenCancel onCancel = null)
        {
            var tween = new TweenRectMove(rectTransform)
            {
                to = to,
                easing = easing,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
            tween.fromGetter = () => tween.rectTransform.anchoredPosition;
            return tween;
        }

        public static TweenScale scaleFromTo(Transform transform,
                                             Func<Vector3> from,
                                             Func<Vector3> to,
                                             float duration = 1f,
                                             TweenEase easing = null,
                                             TweenStart onStart = null,
                                             TweenComplete onComplete = null,
                                             TweenCancel onCancel = null)
        {
            return new TweenScale(transform)
            {
                fromGetter = from,
                toGetter = to,
                easing = easing,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenScale scaleFromTo(Transform transform,
                                             Vector3 from,
                                             Vector3 to,
                                             float duration = 1f,
                                             TweenEase easing = null,
                                             TweenStart onStart = null,
                                             TweenComplete onComplete = null,
                                             TweenCancel onCancel = null)
        {
            return new TweenScale(transform)
            {
                from = from,
                to = to,
                easing = easing,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenScale scaleTo(Transform transform,
                                         Vector3 to,
                                         float duration = 1f,
                                         TweenEase easing = null,
                                         TweenStart onStart = null,
                                         TweenComplete onComplete = null,
                                         TweenCancel onCancel = null)
        {
            var tween = new TweenScale(transform)
            {
                to = to,
                easing = easing,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
            tween.fromGetter = () => tween.transform.localScale;
            return tween;
        }

        public static TweenRotate rotateFromTo(Transform transform,
                                               Quaternion from,
                                               Quaternion to,
                                               float duration = 1f,
                                               Space space = Space.Self,
                                               TweenEase easing = null,
                                               TweenStart onStart = null,
                                               TweenComplete onComplete = null,
                                               TweenCancel onCancel = null)
        {
            return new TweenRotate(transform)
            {
                from = from,
                to = to,
                easing = easing,
                space = space,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenRotate rotateTo(Transform transform,
                                           Quaternion to,
                                           float duration = 1f,
                                           Space space = Space.Self,
                                           TweenEase easing = null,
                                           TweenStart onStart = null,
                                           TweenComplete onComplete = null,
                                           TweenCancel onCancel = null)
        {
            var tween = new TweenRotate(transform)
            {
                to = to,
                easing = easing,
                space = space,
                duration = duration,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
            tween.fromGetter = () => transform.localRotation;
            return tween;
        }
    }
}