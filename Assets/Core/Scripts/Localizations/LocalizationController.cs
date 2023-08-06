//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using UnityEngine;

namespace Core.Scripts.Localizations
{
    [Serializable]
    public static class LocalizationController
    {
        #region Fields

        private static Language[] _languages = {new Language("en", "english")};
        private static LocalizationData[] _localizations;

        private static Language _currentLanguage;
        
        #region Propeties

        public static Language[] Languages => _languages;

        #endregion

        public static event Action<Language> OnLanguageSwitch;
        
        #endregion

        public static void InitLocalization(Language[] languages, LocalizationData[] localizations)
        {
            _languages = languages;
            _localizations = localizations;

            if (_languages.Length == 0)
            {
                Debug.LogError("languages == 0! Logic aborted...");
                return;
            }
            
            SwitchLanguage(0);
        }

        /// <summary>
        /// Obtain localization when a localization code is available.
        /// </summary>
        /// <param name="localizationCode">localization code, is a unique localization identifier.</param>
        /// <returns>data for localization.</returns>
        public static LocalizationData GetLocalization(string localizationCode)
        {
            for (var index = 0; index < _localizations.Length; index++)
            {
                if (_localizations[index].LocalizationCode == localizationCode)
                {
                    return _localizations[index];
                }
            }

            return null;
        }

        public static void SwitchLanguage(int index)
        {
            _currentLanguage = _languages[index];
            OnLanguageSwitch?.Invoke(_currentLanguage);
        }

        public static Language GetCurrentLanguage() => _currentLanguage;

        public static Language GetLanguageByCode(string code)
        {
            for (var index = 0; index < _languages.Length; index++)
            {
                if (_languages[index].LanguageCode == code)
                {
                    return _languages[index];
                }
            }

            return null;
        }
    }
}
