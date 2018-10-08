using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Sequence")]
    [CreateAssetMenu(fileName = "UI Animation", menuName = "UI Animation Asset")]
    public class SequenceAnimationAsset : TweenAsset
    {

        public TweenAsset[] m_sequence = new TweenAsset[0];

        public TweenAsset[] sequence
        {
            get { return m_sequence; }
        }

        public override string Title
        {
            get { return "Sequence"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            foreach (var asset in m_sequence)
                asset.RegisterInjections(list);
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var tween = new TweenSequence();
            foreach (var item in m_sequence)
            {
                tween.Add(item.CreateTween(injector));
            }
            return tween;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SequenceAnimationAsset))]
    public class SequenceAnimationAssetEditor : SequenceAnimationAssetEditorBase
    {
        protected override SerializedProperty Sequence
        {
            get { return serializedObject.FindProperty("m_sequence"); }
        }
    }

    public abstract class SequenceAnimationAssetEditorBase : Editor
    {
        private static readonly string[] m_excludedProps = new string[] { "m_Script", "m_sequence", "m_parallel" };

        private static readonly Color[] m_colors = new Color[]
        {
            new Color(1.0f, 1.0f, 1.0f),

            new Color(0.8f, 1.0f, 1.0f),
            new Color(1.0f, 0.8f, 1.0f),
            new Color(1.0f, 1.0f, 0.8f),

            new Color(0.8f, 0.8f, 1.0f),
            new Color(0.8f, 1.0f, 0.8f),
            new Color(1.0f, 0.8f, 0.8f),
        };

        private readonly static GUIContent m_moveUpContent = new GUIContent("▲", "Move Up in list");
        private readonly static GUIContent m_moveDownContent = new GUIContent("▼", "Move Down in list");
        private readonly static GUIContent m_removeContent = new GUIContent("X", "Remove from list");
        private readonly static GUIContent m_addContent = new GUIContent("+", "Add new animation to list");
        private readonly static GUIContent m_copyContent = new GUIContent("C", "Copy asset to copybuffer");
        private readonly static GUIContent m_pasteContent = new GUIContent("P", "Paste asset copy from copybuffer");

        private GUIStyle m_nameStyle;
        private GUIStyle m_boxStyle;
        private Dictionary<Object, Editor> m_editors = new Dictionary<Object, Editor>();

        protected abstract SerializedProperty Sequence { get; }

        private static int m_depth = 0;

        private void InitStyles()
        {
            m_boxStyle = new GUIStyle(EditorStyles.helpBox);
            m_boxStyle.padding = new RectOffset(1, 1, 4, 4);

            //m_titleStyle = new GUIStyle(EditorStyles.miniLabel);
            //m_titleStyle.padding = new RectOffset(2, 2, 2, 2);

            m_nameStyle = new GUIStyle(EditorStyles.miniTextField);
            m_nameStyle.margin = new RectOffset(0, 0, 3, 3);
            m_nameStyle.padding = new RectOffset(2, 2, 2, 2);
        }

        private void OnDisable()
        {
            foreach (var editor in m_editors)
            {
                DestroyImmediate(editor.Value);
            }
        }

        private static TweenAsset m_copyBuffer;

        public override void OnInspectorGUI()
        {
            if (m_boxStyle == null)
            {
                InitStyles();
            }

            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, m_excludedProps);

            if (m_depth == 0 && target is ParallelAnimationAsset)
            {
                EditorGUILayout.HelpBox("Root is Parallel", MessageType.Info);
            }

            if (m_depth == 0 && m_copyBuffer != null)
            {
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.ObjectField("CopyBuffer", m_copyBuffer, typeof(TweenAsset), false);
                if (GUILayout.Button("Clear", EditorStyles.miniButton, GUILayout.Width(40))) m_copyBuffer = null;
                GUILayout.EndHorizontal();
            }

            var oldColor = GUI.backgroundColor;
            GUI.backgroundColor = m_colors[m_depth % m_colors.Length];
            ++m_depth;

            for (int i = 0; i < Sequence.arraySize; i++)
            {
                var prop = Sequence.GetArrayElementAtIndex(i);
                if (prop.objectReferenceValue == null)
                {
                    Sequence.DeleteArrayElementAtIndex(i);
                    continue;
                }

                Assert.IsNotNull(prop.objectReferenceValue);
                var asset = (TweenAsset)prop.objectReferenceValue;

                bool enabled = (asset.hideFlags & HideFlags.NotEditable) == 0;

                using (new GUILayout.VerticalScope(m_boxStyle))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        var prefix = enabled ? "▼ " : "► ";
                        var newEnabled = GUILayout.Toggle(enabled, prefix + asset.Title, EditorStyles.miniLabel, GUILayout.ExpandWidth(true));
                        if (newEnabled != enabled)
                        {
                            if (newEnabled) asset.hideFlags &= ~HideFlags.NotEditable;
                            else asset.hideFlags |= HideFlags.NotEditable;
                            GUI.FocusControl(null);
                            EditorUtility.SetDirty(asset);
                        }
                        
                        var newName = EditorGUILayout.DelayedTextField(GUIContent.none, asset.name, m_nameStyle, GUILayout.MinWidth(10), GUILayout.ExpandWidth(true));
                        if (newName != asset.name)
                        {
                            Rename(asset, newName);
                            GUI.FocusControl(null);
                        }
                        
                        var oldEnabled = GUI.enabled;
                        GUI.enabled = oldEnabled && i > 0;
                        if (GUILayout.Button(m_moveUpContent, EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                        {
                            Sequence.MoveArrayElement(i, i - 1);
                        }

                        GUI.enabled = oldEnabled && i < Sequence.arraySize - 1;
                        if (GUILayout.Button(m_moveDownContent, EditorStyles.miniButtonMid, GUILayout.Width(20)))
                        {
                            Sequence.MoveArrayElement(i, i + 1);
                        }

                        var sequence = asset as SequenceAnimationAsset;
                        var parallel = asset as ParallelAnimationAsset;
                        var hasSubAssets = (sequence != null && sequence.sequence.Any()) || (parallel != null && parallel.parallel.Any());

                        GUI.enabled = oldEnabled && !hasSubAssets;
                        if (GUILayout.Button(m_copyContent, EditorStyles.miniButtonMid, GUILayout.Width(20)))
                        {
                            m_copyBuffer = (TweenAsset)asset;
                        }

                        GUI.enabled = oldEnabled && !hasSubAssets;
                        if (GUILayout.Button(m_removeContent, EditorStyles.miniButtonRight, GUILayout.Width(20)))
                        {
                            RemoveItem(new RemoveItemRequest { index = i });
                            continue;
                        }
                        GUI.enabled = oldEnabled;
                    }

                    if (enabled)
                    {
                        Editor editor;
                        if (!m_editors.TryGetValue(asset, out editor))
                        {
                            m_editors.Add(asset, editor = Editor.CreateEditor(asset));
                        }

                        editor.OnInspectorGUI();
                    }
                }
            }

            bool canPaste = m_copyBuffer != null;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(m_addContent, canPaste ? EditorStyles.miniButtonLeft : EditorStyles.miniButton))
            {
                var types = System.AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(asm => asm.GetTypes())
                    .Where(type => type.GetCustomAttributes(typeof(TweenMenuItemAttribute), false).Length > 0);

                var menu = new GenericMenu();
                foreach (var type in types)
                {
                    var items = type.GetCustomAttributes(typeof(TweenMenuItemAttribute), false);
                    foreach (var itemObj in items)
                    {
                        var item = (TweenMenuItemAttribute)itemObj;
                        menu.AddItem(new GUIContent(item.Name), false, AddItemByRequest, new AddItemRequest
                        {
                            type = type,
                            attr = item,
                        });
                    }
                }
                menu.ShowAsContext();
            }

            if (canPaste && GUILayout.Button(m_pasteContent, EditorStyles.miniButtonRight, GUILayout.Width(20)))
            {
                AddItemByPrototype(m_copyBuffer);
                m_copyBuffer = null;
            }

            GUILayout.EndHorizontal();

            GUI.backgroundColor = oldColor;
            --m_depth;

            serializedObject.ApplyModifiedProperties();
        }

        private void Rename(Object asset, string name)
        {
            Assert.IsNotNull(asset);

            try
            {
                asset.name = name;
                EditorUtility.SetDirty(asset);
                ReimportMainAsset();
            }
            catch
            {
                EditorUtility.DisplayDialog("UI Animation Asset", "Renaming failed. Invalid asset name", "Ok");
            }
        }

        private void RemoveItem(RemoveItemRequest request)
        {
            Assert.IsNotNull(request);

            var prop = Sequence.GetArrayElementAtIndex(request.index);
            var asset = prop.objectReferenceValue;
            Assert.IsNotNull(asset);

            if (!EditorUtility.DisplayDialog("UI Animation Asset", "Are you really want to delete " + asset.name + "?", "Delete", "Cancel"))
                return;

            prop.objectReferenceValue = null;
            Sequence.DeleteArrayElementAtIndex(request.index);

            ScriptableObject.DestroyImmediate(asset, true);

            ReimportMainAsset();
        }

        private void AddItemByRequest(object requestObj)
        {
            Assert.IsNotNull(requestObj);
            var request = (AddItemRequest)requestObj;
            var asset = ScriptableObject.CreateInstance(request.type);
            AddItemInternal(asset);
        }

        private void AddItemByPrototype(ScriptableObject prototype)
        {
            Assert.IsNotNull(prototype);
            var asset = ScriptableObject.Instantiate(prototype);
            AddItemInternal(asset);
        }

        private void AddItemInternal(ScriptableObject prototype)
        {
            Assert.IsNotNull(prototype);

            var asset = ScriptableObject.Instantiate(prototype);
            Assert.IsNotNull(asset);
            asset.name = "Untitled";
            asset.hideFlags |= HideFlags.HideInHierarchy;

            var mainPath = AssetDatabase.GetAssetPath(target);
            var mainAsset = AssetDatabase.LoadMainAssetAtPath(mainPath);
            Assert.IsNotNull(mainAsset);
            AssetDatabase.AddObjectToAsset(asset, mainAsset);

            var index = Sequence.arraySize;
            Sequence.InsertArrayElementAtIndex(index);
            Sequence.GetArrayElementAtIndex(index).objectReferenceValue = asset;
            Sequence.serializedObject.ApplyModifiedProperties();

            ReimportMainAsset();
        }

        private void ReimportMainAsset()
        {
            var path = AssetDatabase.GetAssetPath(target);
            AssetDatabase.ImportAsset(path);
        }

        private class AddItemRequest
        {
            public System.Type type;
            public TweenMenuItemAttribute attr;
        }

        private class RemoveItemRequest
        {
            public int index;
        }
    }
#endif
}