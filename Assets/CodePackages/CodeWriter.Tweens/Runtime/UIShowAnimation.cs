using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeWriter.Tweens
{
    //Obsolete
    [RequireComponent(typeof(RectTransform))]
    public sealed class UIShowAnimation : MonoBehaviour, ITweenAssetInjector
    {
        [SerializeField] RectTransform m_rectTransform;
        [SerializeField] CanvasGroup m_canvasGroup;

        [SerializeField] TweenAsset m_asset;
         
        [SerializeField, HideInInspector] int m_order;

        private TweenBase m_tween = Tween.idle();

#if UNITY_EDITOR
        void Reset()
        {
            Awake();
        }
#endif
        void Awake()
        {
            if (m_rectTransform == null)
                m_rectTransform = GetComponent<RectTransform>();

            if (m_canvasGroup == null)
                m_canvasGroup = GetComponent<CanvasGroup>();
        }

        void OnEnable()
        {
#if UNITY_EDITOR
            if (m_asset == null)
            {
                Debug.LogWarningFormat(this, "UITweenAsset == null at {0}", name);
                enabled = false;
                return;
            }
#endif
            m_tween.Cancel();
            m_tween = m_asset.CreateTween(this);
            gameObject.StartTween(m_tween);
        }

        public Object GetInjection<T>(string name) where T : Object
        {
            var type = typeof(T);
            if (type == typeof(RectTransform))
                return m_rectTransform;

            if (type == typeof(CanvasGroup))
                return m_canvasGroup;

            throw new System.ArgumentException("Injection with name " + name + " of type " + typeof(T).Name + " not found");
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UIShowAnimation))]
    public class UIShowAnimationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_rectTransform"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_canvasGroup"));

            var assetProp = serializedObject.FindProperty("m_asset");
            var asset = assetProp.objectReferenceValue;

            EditorGUI.BeginChangeCheck();
            asset = EditorGUILayout.ObjectField("Asset", asset, typeof(TweenAsset), false);
            if (EditorGUI.EndChangeCheck())
                assetProp.objectReferenceValue = asset;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_order"));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}