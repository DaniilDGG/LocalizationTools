//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Localizations
{
    [Serializable]
    public class Language : LanguageShort
    {
        #region Fields

        [SerializeField] private string _languageName;

        #region Propeties

        public string LanguageName => _languageName;

        #endregion

        #endregion

        public Language(string code, string name) : base(code)
        {
            _languageName = name;
        }
    }

    [Serializable]
    public class LanguageShort
    {
        #region Fields

        [SerializeField] private string _languageCode;

        #region Propeties

        public string LanguageCode => _languageCode;

        #endregion
        
        #endregion
        
        public LanguageShort(string code)
        {
            _languageCode = code;
        }
        
        public static implicit operator string(LanguageShort language) => language?._languageCode;
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

            for (var index = 0; index < LocalizationController.Languages.Length; index++)
            {
                languageDates.Add(new LanguageData(LocalizationController.Languages[index], ""));
            }

            return new LocalizationData(code, languageDates);
        }

        public void Replace(string a, string b)
        {
            for (var index = 0; index < _data.Count; index++)
            {
                var languageData = _data[index];
                var r = languageData.Localization.Replace(a, b);
                languageData = new LanguageData(languageData.Language, r);
                _data[index] = languageData;
            }
        }
        
        public static LocalizationData operator +(LocalizationData a, LocalizationData b)
        {
            var localizationData = a;
            
            for (var index = 0; index < localizationData._data.Count; index++)
            {
                var languageData = localizationData._data[index];
                
                languageData += b._data.Find(data =>
                    data.Language.LanguageCode == languageData.Language.LanguageCode);

                localizationData._data[index] = languageData;
            }

            return localizationData;
        }
        
        public static LocalizationData operator +(LocalizationData a, string b)
        {
            var localizationData = a;

            for (var index = 0; index < localizationData._data.Count; index++)
            {
                var languageData = localizationData._data[index];

                languageData += b;
                
                localizationData._data[index] = languageData;
            }

            return localizationData;
        }
    }

    [Serializable]
    public struct LanguageData
    {
        #region Fields

        [SerializeField] private LanguageShort _language;
        [SerializeField] private string _localization;

        #region Propeties

        public LanguageShort Language => _language;
        public string Localization => _localization;

        #endregion
        
        #endregion

        public LanguageData(LanguageShort language, string localization)
        {
            _language = language;
            _localization = localization;
        }
        
        public static LanguageData operator +(LanguageData a, LanguageData b)
        {
            a._localization += b._localization;

            return a;
        }
        
        public static LanguageData operator +(LanguageData a, string b)
        {
            a._localization += b;

            return a;
        }
    }
}
