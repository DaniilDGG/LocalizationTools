//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

namespace Core.Scripts.Localizations.Editor
{
    public static class LocalizationXlsxReader
    {
        private static readonly string FilePath = Path.Combine(Application.dataPath, "localization.xlsx");
        private static IWorkbook _workbook;

        [MenuItem("Localization/Import XLSX")]
        public static void ReadLocalizationInFile()
        {
            LocalizationEditor.Init();
            
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                _workbook = new XSSFWorkbook(fileStream);
            }

            var window = XlsxImportWindow.ShowWindow();
            window.OnImport += StartImport;
        }

        private static void StartImport(ParametersImport parametersImport)
        {
            var sheet = _workbook.GetSheetAt(0);

            var localizations = new List<LocalizationData>();
            var languages = new List<Language>();

            for (var index = 1; index < sheet.GetRow(0).Cells.Count; index++)
            {
                var row = sheet.GetRow(0);
                var cell = row.Cells[index];

                var language = new Language(cell.StringCellValue, "");
                
                languages.Add(language);
            }

            for (var index = 1; index < sheet.LastRowNum; index++)
            {
                var languageDates = new List<LanguageData>();

                var row = sheet.GetRow(index);
                
                for (var index2 = 1; index2 < row.Cells.Count; index2++)
                {
                    languageDates.Add(new LanguageData(languages[index2 - 1], row.Cells[index2].StringCellValue));
                }
                
                localizations.Add(new LocalizationData(row.Cells[0].StringCellValue, languageDates));
            }

            for (var index = 0; index < languages.Count; index++)
            {
                Debug.Log($"{languages[index].LanguageCode}");
            }
            
            for (var index = 0; index < localizations.Count; index++)
            {
                Debug.Log($"{localizations[index].LocalizationCode} - {JsonUtility.ToJson(localizations[index])}");
            }
            
            if (!parametersImport.ReplaceLocalizationInFile)
            {
                LocalizationEditor.LocalizationProfile.SetLocalizations(localizations);
                return;
            }

            for (var index = 0; index < localizations.Count; index++)
            {
                var data = localizations[index].Data;
                var currentData = LocalizationController.GetLocalization(localizations[index].LocalizationCode).Data;
                
                for (var index2 = 0; index2 < currentData.Count; index2++)
                {
                    if (!parametersImport.Languages.Contains(currentData[index2].Language.LanguageCode)) continue;

                    currentData[index2] = data.Find(languageData => languageData.Language.LanguageCode == currentData[index2].Language.LanguageCode);
                }
                
                LocalizationEditor.LocalizationProfile.SetLocalization(localizations[index].LocalizationCode, currentData.ToArray());
            }
        }
    }
}
