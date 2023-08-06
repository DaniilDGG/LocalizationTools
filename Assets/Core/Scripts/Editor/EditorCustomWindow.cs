//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Scripts.Editor
{
    public class EditorCustomWindow<T> : EditorWindow where T : EditorCustomWindow<T>
    {
        #region Fields

        private static T _editorCustomWindow;

        protected VisualElement Root;

        #endregion
        
        /// <summary>
        /// Show custom editor window.
        /// </summary>
        public static T ShowWindow()
        {
            if (_editorCustomWindow)
            {
                return _editorCustomWindow;
            }
            
            _editorCustomWindow = GetWindow<T>();
            _editorCustomWindow.titleContent = new GUIContent("Editor Custom Window");

            return _editorCustomWindow;
        }

        protected TextField CreateTextInput(string text, string label) => CreateTextInput(text, label, Root);
        
        protected TextField CreateTextInput(string text, string label, VisualElement visualElement)
        {
            var textElement = new TextField(label, 30, false, false, ' ')
            {
                value = text
            };

            visualElement.Add(textElement);

            return textElement;
        }
    }
}
