using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Assertions;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeWriter.Tweens
{
    public class TweenPlayer : MonoBehaviour, ITweenAssetInjector
    {
        [SerializeField]
        private bool m_playOnAwake = false;

        [SerializeField]
        private TweenAsset m_asset;

        [SerializeField]
        private Entry[] m_entries;

        [SerializeField]
        private UnityEvent m_onCompleted;

        private TweenBase m_tween;

        public UnityEvent onCompleted
        {
            get { return m_onCompleted; }
        }

        private void OnEnable()
        {
            if (m_playOnAwake)
            {
                Play();
            }
        }

        [ContextMenu("Play")]
        public void Play()
        {
            if (m_tween != null)
                m_tween.Cancel();

#if !UNITY_EDITOR
            if (m_tween == null)
#endif
            {
                m_tween = m_asset != null ? m_asset.CreateTween(this) : Tween.idle();

                if (m_onCompleted != null)
                    m_tween.externComplete = m_onCompleted.Invoke;
            }

            gameObject.StartTween(m_tween);
        }

        [ContextMenu("Stop")]
        public void Stop()
        {
            if (m_tween != null)
                m_tween.Cancel();
        }

        public UnityEngine.Object GetInjection<T>(string name) where T : UnityEngine.Object
        {
            var typeName = typeof(T).FullName;
            foreach (var entry in m_entries)
            {
                if (entry.name == name && entry.type == typeName)
                    return entry.obj;
            }
            throw new ArgumentException("Injection with name " + name + " of type " + typeof(T).Name + " not found");
        }

        [Serializable]
        public class Entry
        {
            public string name;
            public string type;
            public UnityEngine.Object obj;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TweenPlayer), true)]
    public sealed class TweenPlayerEditor : Editor
    {
        private static readonly string[] m_excluded = new[] { "m_Script", "m_asset", "m_entries" };

        private SerializedProperty m_asset;
        private SerializedProperty m_entries;

        private List<TweenAssetInjection> m_injections = new List<TweenAssetInjection>();

        private void OnEnable()
        {
            Assert.IsNotNull(serializedObject);

            m_asset = serializedObject.FindProperty("m_asset");
            Assert.IsNotNull(m_asset);

            m_entries = serializedObject.FindProperty("m_entries");
            Assert.IsNotNull(m_entries);

            Inject();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            var oldEnabled = GUI.enabled;
            GUI.enabled &= EditorApplication.isPlaying && targets.Length == 1;
            if (GUILayout.Button("Play", EditorStyles.miniButtonLeft, GUILayout.Width(80)))
            {
                var anim = (UIAnimation)target;
                anim.Play();
            }
            if (GUILayout.Button("Cancel", EditorStyles.miniButtonMid, GUILayout.Width(80)))
            {
                var anim = (UIAnimation)target;
                anim.Stop();
            }

            GUILayout.Box("", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(true));
            GUI.enabled = oldEnabled;
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            var newAsset = (TweenAsset)EditorGUILayout.ObjectField(m_asset.displayName, m_asset.objectReferenceValue, typeof(TweenAsset), false);
            if (newAsset != m_asset.objectReferenceValue)
            {
                m_asset.objectReferenceValue = newAsset;
                Inject();
            }

            if (newAsset == null)
            {
                EditorGUILayout.HelpBox("Asset is null. Use idle animation instead.", MessageType.Info);
            }

            DrawPropertiesExcluding(serializedObject, m_excluded);

            Type type = null;
            EditorGUI.indentLevel++;
            for (int i = 0; i < m_injections.Count; i++)
            {
                var injection = m_injections[i];
                if (injection.Type != type)
                {
                    type = injection.Type;
                    EditorGUI.indentLevel--;
                    GUILayout.Label(type.Name, EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                }

                var entry = FindEntry(injection);
                if (entry == null)
                {
                    EditorGUILayout.HelpBox("Entry is Null", MessageType.Error);
                    continue;
                }

                var objProp = entry.FindPropertyRelative("obj");
                objProp.objectReferenceValue = EditorGUILayout.ObjectField(injection.Name, objProp.objectReferenceValue, injection.Type, true);
            }
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }

        private SerializedProperty FindEntry(TweenAssetInjection injection)
        {
            var name = injection.Name;
            var typeName = injection.Type.FullName;
            for (int i = 0; i < m_entries.arraySize; i++)
            {
                var entryProp = m_entries.GetArrayElementAtIndex(i);
                var nameProp = entryProp.FindPropertyRelative("name");
                var typeProp = entryProp.FindPropertyRelative("type");
                if (nameProp.stringValue == name && typeProp.stringValue == typeName)
                    return entryProp;
            }

            return null;
        }

        private void Inject()
        {
            m_injections.Clear();
            if (m_asset.objectReferenceValue != null)
            {
                var asset = (TweenAsset)m_asset.objectReferenceValue;
                asset.RegisterInjections(m_injections);
            }

            m_injections = m_injections.OrderBy(o => o.Type.Name).Distinct().ToList();

            for (int i = 0; i < m_entries.arraySize; i++)
            {
                var entryProp = m_entries.GetArrayElementAtIndex(i);
                var nameProp = entryProp.FindPropertyRelative("name");
                var typeProp = entryProp.FindPropertyRelative("type");
                var name = nameProp.stringValue;
                var type = typeProp.stringValue;
                if (!m_injections.Any(o => o.Name == name && o.Type.FullName == type))
                {
                    m_entries.DeleteArrayElementAtIndex(i);
                }
            }

            foreach (var inj in m_injections)
            {
                if (FindEntry(inj) == null)
                {
                    var index = m_entries.arraySize;
                    m_entries.InsertArrayElementAtIndex(index);
                    var entryProp = m_entries.GetArrayElementAtIndex(index);
                    entryProp.FindPropertyRelative("name").stringValue = inj.Name;
                    entryProp.FindPropertyRelative("type").stringValue = inj.Type.FullName;
                    entryProp.FindPropertyRelative("obj").objectReferenceValue = null;
                }
            }

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
#endif
}
