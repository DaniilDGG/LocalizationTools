//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Core.Scripts.File_Stream
{
    public static class FileEditor
    {
        private static readonly string PersistentDataPath = Path.Combine(Application.persistentDataPath, "Data");

        public static void Write(string name, string text, Action<Exception> onError = null)
        {
            CheckDirectory(onError);
            Write(name, text, PersistentDataPath, onError);
        }

        public static async void Write(string name, string text, string path, Action<Exception> onError = null)
        {
            CheckDirectory(onError);
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    using var fs = File.Create(Path.Combine(path, name));
                
                    var info = new UTF8Encoding(true).GetBytes(text);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                });
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
            }
        }

        public static string Read(string name, Action<Exception> onError = null) => Read(name, PersistentDataPath, onError);

        public static string Read(string name, string path, Action<Exception> onError = null)
        {
            CheckDirectory(onError);
            if (!File.Exists(Path.Combine(path, name)))
            {
                onError?.Invoke(new Exception("File not exists. Logic aborted..."));
                return "";
            }

            var fileText = "";
            
            try
            {
                using var sr = File.OpenText(Path.Combine(path, name));
            
                fileText = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
            }
            
            return fileText;
        }

        public static bool Exists(string name)
        {
            return File.Exists(Path.Combine(PersistentDataPath, name));
        }

        public static string GetDataPath(string nameFile)
        {
            return Path.Combine(PersistentDataPath, nameFile);
        }

        private static void CheckDirectory(Action<Exception> onError)
        {
            try
            {
                if (!System.IO.Directory.Exists(PersistentDataPath))
                {
                    System.IO.Directory.CreateDirectory(PersistentDataPath);
                }
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
            }
        }

        public static void Delete(string fileName, Action<Exception> onError = null)
        {
            CheckDirectory(onError);
            try
            {
                File.Delete(Path.Combine(PersistentDataPath, fileName));
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
            }
        }
    }
}