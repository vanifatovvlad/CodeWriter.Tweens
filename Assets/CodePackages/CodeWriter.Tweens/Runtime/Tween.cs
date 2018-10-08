using System;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public static partial class Tween
    {
        public static TweenIdle idle(TweenStart onStart = null,
                                     TweenComplete onComplete = null,
                                     TweenCancel onCancel = null)
        {
            return new TweenIdle
            {
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenDelay delay(float delay,
                                       TweenStart onStart = null,
                                       TweenComplete onComplete = null,
                                       TweenCancel onCancel = null)
        {
            return new TweenDelay
            {
                delay = delay,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenInvoke invoke(Action action,
                                         TweenStart onStart = null,
                                         TweenComplete onComplete = null,
                                         TweenCancel onCancel = null)
        {
            return new TweenInvoke(action)
            {
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenEasing fromTo(float from,
                                         float to,
                                         float duration,
                                         TweenEase easing = null,
                                         TweenUpdate onUpdate = null,
                                         TweenStart onStart = null,
                                         TweenComplete onComplete = null,
                                         TweenCancel onCancel = null)
        {
            easing = easing ?? Easings.Linear;

            return new TweenEasing
            {
                from = from,
                to = to,
                easing = easing,
                duration = duration,
                externUpdate = onUpdate,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenColorEasing fromTo(Color from,
                                         Color to,
                                         float duration,
                                         TweenEase easing = null,
                                         ColorTweenUpdate onUpdate = null,
                                         TweenStart onStart = null,
                                         TweenComplete onComplete = null,
                                         TweenCancel onCancel = null)
        {
            easing = easing ?? Easings.Linear;

            return new TweenColorEasing
            {
                from = from,
                to = to,
                easing = easing,
                duration = duration,
                externUpdate = onUpdate,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenEasing fromTo(Func<float> from,
                                         Func<float> to,
                                         float duration,
                                         TweenEase easing = null,
                                         TweenUpdate onUpdate = null,
                                         TweenStart onStart = null,
                                         TweenComplete onComplete = null,
                                         TweenCancel onCancel = null)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (to == null)
                throw new ArgumentNullException("to");

            easing = easing ?? Easings.Linear;

            return new TweenEasing
            {
                fromGetter = from,
                toGetter = to,
                easing = easing,
                duration = duration,
                externUpdate = onUpdate,
                externStart = onStart,
                externComplete = onComplete,
                externCancel = onCancel,
            };
        }

        public static TweenSequence sequence(TweenBase tweenA, TweenBase tweenB)
        {
            if (tweenA == null)
                throw new ArgumentNullException("tweenA");

            if (tweenB == null)
                throw new ArgumentNullException("tweenB");

            var sequenceTween = new TweenSequence();
            sequenceTween.Add(tweenA);
            sequenceTween.Add(tweenB);
            return sequenceTween;
        }

        public static TweenSequence sequence(params TweenBase[] tweens)
        {
            if (tweens == null)
                throw new ArgumentNullException("tweens");

            var sequenceTween = new TweenSequence();
            foreach (var tween in tweens)
            {
                sequenceTween.Add(tween);
            }
            return sequenceTween;
        }

        public static TweenParallel parallel(TweenBase tweenA, TweenBase tweenB)
        {
            if (tweenA == null)
                throw new ArgumentNullException("tweenA");

            if (tweenB == null)
                throw new ArgumentNullException("tweenB");

            var paralletTween = new TweenParallel();
            paralletTween.Add(tweenA);
            paralletTween.Add(tweenB);
            return paralletTween;
        }

        public static TweenParallel parallel(params TweenBase[] tweens)
        {
            if (tweens == null)
                throw new ArgumentNullException("tweens");

            var parallelTween = new TweenParallel();
            foreach(var tween in tweens)
            {
                parallelTween.Add(tween);
            }
            return parallelTween;
        }
    }
}