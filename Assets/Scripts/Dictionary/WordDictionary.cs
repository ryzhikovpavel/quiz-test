using System.Collections.Generic;
using UnityEngine;

namespace Dictionary
{
    public class WordDictionary
    {
        private const string DictionaryResourceName = "words";
        private readonly Dictionary<int, List<string>> _words = new();

        public WordDictionary()
        {
            var resource = Resources.Load<TextAsset>(DictionaryResourceName);
            string[] words = resource.text.Split('\n');
            foreach (var word in words)
            {
                int length = word.Length;
                if (_words.TryGetValue(length, out List<string> list) == false || list is null)
                    _words[length] = list = new List<string>();
                list.Add(word);
            }
        }

        public string GetWord(int length)
        {
            int index = GetRepeatedIndexFor(length) + 1;
            SaveIndexFor(length, index);
            return _words[length][index];
        }

        private int GetRepeatedIndexFor(int length)
        {
            var index = PlayerPrefs.GetInt($"{DictionaryResourceName}_{length}", -1);
            if (index >= _words[length].Count) index = 0;
            return index;
        }

        private void SaveIndexFor(int length, int index)
        {
            PlayerPrefs.SetInt($"{DictionaryResourceName}_{length}", index);
            PlayerPrefs.Save();
        }
    }
}