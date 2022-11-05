using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Editor
{
    public class WordsReader
    {
        private const int MinAllowed = 'A';
        private const int MaxAllowed = 'Z';
        private const char EmptyChar = ' ';

        private char[] _buffer = new char[64];
        
        public HashSet<string> FindUniqueWords(string text)
        {
            text = ClearForbiddenCharacters(text.ToUpper());
            string[] words = text.Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> hash = new HashSet<string>();

            foreach (var word in words)
            {
                if (hash.Contains(word)) continue;
                hash.Add(word);
            }

            return hash;
        }

        private string ClearForbiddenCharacters(string text)
        {
            char[] characters = text.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                ref char c = ref characters[i];
                if (c >= MinAllowed && c <= MaxAllowed) continue;
                c = EmptyChar;
            }
            return new string(characters);
        }
    }
}