#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CodeWriter.Tweens
{
    [CustomPropertyDrawer(typeof(EasingType))]
    public class EasingTypeDrawer : PropertyDrawer
    {
        readonly string[] POSTFIXES = new[] { " Ease In Out", " Ease Out In", " Ease In", " Ease Out" };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);

            var selectedName = property.enumDisplayNames[property.enumValueIndex];
            if (GUI.Button(position, selectedName, EditorStyles.popup))
            {
                var menu = new GenericMenu();

                int index = 0;
                foreach (var name in property.enumDisplayNames)
                {
                    string menuItemName = name;
                    int menuItemIndex = index++;

                    foreach (var postfix in POSTFIXES)
                    {
                        if (name.EndsWith(postfix))
                        {
                            menuItemName = name.Replace(postfix, "/" + name);
                            break;
                        }
                    }

                    menu.AddItem(new GUIContent(menuItemName), selectedName == name, () =>
                    {
                        property.enumValueIndex = menuItemIndex;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }

                menu.DropDown(position);
            }

            EditorGUI.EndProperty();
        }
    }
}
#endif