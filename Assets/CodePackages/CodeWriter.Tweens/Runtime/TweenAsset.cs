using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeWriter.Tweens
{
    public abstract class TweenAsset : ScriptableObject
    {
        public const string DEFAULT_INJECT = "main";

        public abstract string Title { get; }
        public abstract TweenBase CreateTween(ITweenAssetInjector injector);
        public abstract void RegisterInjections(List<TweenAssetInjection> list);
    }

    public class TweenMenuItemAttribute : System.Attribute
    {
        public string Name { get; set; }

        public TweenMenuItemAttribute(string name)
        {
            Name = name;
        }
    }

    [System.Serializable]
    public class TweenAssetInjection
    {
        public string Name { get; private set; }
        public System.Type Type { get; private set; }

        public TweenAssetInjection(string name, System.Type type)
        {
            Name = name;
            Type = type;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as TweenAssetInjection;
            if (other == null)
                return false;

            return other.Name == Name && other.Type == Type;
        }
    }

    public interface ITweenAssetInjector
    {
        Object GetInjection<T>(string name) where T : Object;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TweenAsset), true, isFallback = true)]
    public class TweenAssetEditor : Editor
    {
        private static readonly string[] m_excludedProps = new string[] { "m_Script" };

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, m_excludedProps);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}