using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Config", menuName = "Config", order = 0)]
    public class Config : ScriptableObject
    {
        [SerializeField] private int wordMinLetters;
        [SerializeField] private int wordMaxLetters;
        [SerializeField] private int maxErrorPerWord;

        public int WordMinLetters => wordMinLetters;
        public int WordMaxLetters => wordMaxLetters;
        public int MaxErrorPerWord => maxErrorPerWord;
    }
}