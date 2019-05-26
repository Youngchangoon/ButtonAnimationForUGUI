using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Banana.UI.UIObject.Buttons.Editors
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PressingButton))]
    public class PressingButtonEditor : Editor
    {
        [MenuItem("GameObject/Banana/UIObject/PressingButton", false, 2)]
        static void Create()
        {
            // ------- Create code ---------
            var pressingButtonObj = new GameObject("PressingButton");
            pressingButtonObj.transform.SetParent(Selection.activeTransform ?? null, false);
            
            var pressingButtonCompoennt = pressingButtonObj.AddComponent<PressingButton>();
            var scaleButton = pressingButtonObj.GetComponent<Button>();
            scaleButton.targetGraphic = pressingButtonCompoennt;
            scaleButton.transition = Selectable.Transition.None;
            // ------------------------------

            Selection.activeGameObject = pressingButtonObj;
        }

        private void OnEnable()
        {
            var targetButton = (PressingButton)target;
            var targetTrs = targetButton.GetComponent<RectTransform>();
            
            if (targetTrs.Find("ShadowImage") == null)
            {
                var shadowObj = new GameObject("ShadowImage", typeof(RectTransform), typeof(Image));
                shadowObj.transform.SetParent(targetTrs);
                shadowObj.transform.localPosition = new Vector2(0, -targetButton.PressingDown);
                shadowObj.GetComponent<Image>().color = targetButton.ShadowColor;
            }

            if (targetTrs.Find("MainImage") == null)
            {
                var mainObj = new GameObject("MainImage", typeof(RectTransform), typeof(Image));
                mainObj.transform.SetParent(targetTrs);
                mainObj.transform.localPosition = Vector3.zero;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("pressingDown"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("buttonSprite"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shadowColor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isSmooth"));
            
            if (GUILayout.Button("Accept Image"))
            {
                var button = ((PressingButton)target);
                button.AcceptImages();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}