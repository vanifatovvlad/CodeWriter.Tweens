using System.Collections.Generic;
using UnityEngine;

namespace CodeWriter.Tweens
{
    public sealed class TweenPerformer : MonoBehaviour
    {
        private static bool m_hasInstance;
        private static List<Tweener> m_tweeners = new List<Tweener>();
        private static List<Tweener> m_toAdd = new List<Tweener>();
        private static List<Tweener> m_toRemove = new List<Tweener>();

        internal static void AddTweener(Tweener tweener)
        {
            EnsureInstance();

            m_toAdd.Add(tweener);
        }

        internal static void RemoveTweener(Tweener tweener)
        {
            m_toRemove.Add(tweener);
        }

        private static void EnsureInstance()
        {
            if (!m_hasInstance)
            {
                DontDestroyOnLoad(new GameObject(typeof(TweenPerformer).Name, typeof(TweenPerformer)));
                m_hasInstance = true;
            }
        }

        void Update()
        {
            AddPending();
            RemovePendings();
            UpdateTweeners();
        }

        private void AddPending()
        {
            if (m_toAdd.Count == 0)
                return;

            m_tweeners.AddRange(m_toAdd);
            m_toAdd.Clear();
        }

        private void RemovePendings()
        {
            if (m_toRemove.Count == 0)
                return;

            for (int i = 0; i < m_toRemove.Count; i++)
            {
                m_tweeners.Remove(m_toRemove[i]);
            }
            m_toRemove.Clear();
        }

        private void UpdateTweeners()
        {
            float deltaTime = Time.smoothDeltaTime;
            for (int i = 0; i < m_tweeners.Count; i++)
            {
                m_tweeners[i].Update(deltaTime);
            }
        }
    }
}