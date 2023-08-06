//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using UnityEngine;
using UnityEngine.Events;

namespace Core.Scripts.Localizations.Unity.Base
{
    public class LocalizationInfo : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string _localizationCode;

        private LocalizationData _localizationData;
        private string _currentLanguageCode;
        public event UnityAction<string> OnSwitchLanguage;

        #endregion

        #region MonoBehavior

        private void Awake()
        {
            _localizationData = LocalizationController.GetLocalization(_localizationCode);
            _currentLanguageCode = LocalizationController.GetCurrentLanguage();
            LocalizationController.OnLanguageSwitch += delegate(Language language)
            {
                _currentLanguageCode = language.LanguageCode;
                OnSwitchLanguage?.Invoke(GetLocalization());
            };
        }

        #endregion

        /// <summary>
        /// Get localization, by localization code, which is stored in LocalizationInfo.
        /// </summary>
        /// <returns>Localization for the current language.</returns>
        public string GetLocalization()
        {
            if (_localizationData == null || _localizationData?.Data.Count == 0)
            {
                return "localization is null";
            }
            
            return _localizationData.Data.Find(data => data.Language.LanguageCode == _currentLanguageCode).Localization;
        }
    }
}
