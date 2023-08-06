//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Localizations
{
    [Serializable]
    public class Language
    {
        #region Fields

        [SerializeField] private string _languageCode;
        [SerializeField] private string _languageName;

        #region Propeties

        public string LanguageCode => _languageCode;
        public string LanguageName => _languageName;

        #endregion

        #endregion

        public Language(string code, string name)
        {
            _languageCode = code;
            _languageName = name;
        }
        
        public static implicit operator string(Language language) => language?._languageCode;
    }
    
    [Serializable]
    public class LocalizationData
    {
        #region Fields

        [SerializeField] private string _localizationCode;
        [SerializeField] private List<LanguageData> _data;

        #region Propeties

        public string LocalizationCode => _localizationCode;
        public List<LanguageData> Data => _data;

        #endregion
        
        #endregion

        public LocalizationData(string localizationCode, List<LanguageData> languageData)
        {
            _localizationCode = localizationCode;
            _data = languageData;
        }

        internal void SetData(List<LanguageData> data)
        {
            if (data?.Count == 0)
            {
                return;
            }
            
            _data = data;
        }

        public static LocalizationData GetDefaultData(string code)
        {
            List<LanguageData> languageDates = new();

            for (int index = 0; index < LocalizationController.Languages.Length; index++)
            {
                languageDates.Add(new LanguageData(LocalizationController.Languages[index], ""));
            }

            return new LocalizationData(code, languageDates);
        }
    }

    [Serializable]
    public struct LanguageData
    {
        #region Fields

        [SerializeField] private Language _language;
        [SerializeField] private string _localization;

        #region Propeties

        public Language Language => _language;
        public string Localization => _localization;

        #endregion
        
        #endregion

        public LanguageData(Language language, string localization)
        {
            _language = language;
            _localization = localization;
        }
    }
}
