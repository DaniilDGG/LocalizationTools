//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using Core.Scripts.Localizations.Config;
using UnityEditor;
using UnityEngine;

namespace Core.Scripts.Localizations.Editor
{
    public static class LocalizationEditor
    {
        #region Fields

        private static LocalizationProfile _localizationProfile;

        #region Propeties

        public static LocalizationProfile LocalizationProfile => _localizationProfile;

        #endregion
        
        #endregion
        
        [MenuItem("Localization/Language settings")]
        private static void LanguagesSetting()
        {
            Init();
            LanguagesWindow.ShowWindow().OnSaveLanguages += _localizationProfile.SetLanguages;
        }

        [MenuItem("Localization/Localization settings")]
        private static void LocalizationSetting()
        {
            Init();
            LocalizationWindow.ShowWindow().OnSaveLocalization += _localizationProfile.SetLocalization;
        }

        private static void Init()
        {
            if (!_localizationProfile)
            {
                _localizationProfile = Resources.LoadAll<LocalizationProfile>("")[0];
            }
            
            _localizationProfile.InitLocalizationSystem();
        }
    }
}
