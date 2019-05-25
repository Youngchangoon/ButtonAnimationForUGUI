using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Banana.UI.UIObject.Buttons.Editors
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScaleButton))]
    public class ScaleButtonEditor : Editor
    {
        [MenuItem("GameObject/Banana/UIObject/ScaleButton", false, 2)]
        static void Create()
        {
            // ------- Create code ---------
            var scaleButtonObj = new GameObject("ScaleButton");
            scaleButtonObj.transform.SetParent(Selection.activeTransform ?? null, false);

            var scaleButtonComponent = scaleButtonObj.AddComponent<ScaleButton>();
            var scaleButton = scaleButtonObj.GetComponent<Button>();
            scaleButton.targetGraphic = scaleButtonComponent;
            scaleButton.transition = Selectable.Transition.None;
            // ------------------------------

            Selection.activeGameObject = scaleButtonObj;
        }

        private void OnEnable()
        {
            var targetTrs = ((ScaleButton)target).GetComponent<RectTransform>();
            if (targetTrs.Find("AnimationObject") == null)
            {
                var animObj = new GameObject("AnimationObject", typeof(RectTransform));
                animObj.transform.SetParent(targetTrs);
                animObj.transform.localPosition = Vector3.zero;

                var image = new GameObject("Image", typeof(RectTransform), typeof(Image));
                var imageRectTrs = image.GetComponent<RectTransform>();
                image.transform.SetParent(animObj.transform, false);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("pressingScale"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("animationType"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}