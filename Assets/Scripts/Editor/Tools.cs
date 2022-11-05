using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class Tools
    {
        [MenuItem("Tools/Compile words dictionary from file")]
        private static async void CompileWordsFromFile()
        {
            string path = EditorUtility.OpenFilePanel("Select text file", "", "txt");
            if (string.IsNullOrEmpty(path)) return;
            try
            {
                string operationName = "Compile words dictionary from " + Path.GetFileNameWithoutExtension(path);
                
                EditorUtility.DisplayProgressBar(operationName, "Reading...", 0);
                string fileContent = await File.ReadAllTextAsync(path);
                WordsReader rider = new WordsReader();
                EditorUtility.DisplayProgressBar(operationName, "Find unique words...", 0);
                HashSet<string> words = await Task.Run(()=>rider.FindUniqueWords(fileContent));

                EditorUtility.DisplayProgressBar(operationName, "Saving...", 0);
                path = Path.Combine(Application.dataPath, "Resources");
                if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                path = Path.Combine(path, "words.txt");
                await File.WriteAllLinesAsync(path, words);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }
    }
}