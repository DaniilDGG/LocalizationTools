//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using Core.Scripts.Localizations.Unity.Base;
using TMPro;
using UnityEngine;

namespace Core.Scripts.Localizations.Unity
{
    [RequireComponent(typeof(LocalizationInfo))]
    public class TMPTextLocalization : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TMP_Text _tmpText;
        
        private LocalizationInfo _localizationInfo;

        #endregion

        private void Awake()
        {
            _localizationInfo.OnSwitchLanguage += delegate(string text) { _tmpText.text = text; };
            _tmpText.text = _localizationInfo.GetLocalization();
        }
    }
}
