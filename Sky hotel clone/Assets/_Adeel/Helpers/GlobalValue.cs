using UnityEngine;

namespace _Adeel.Helpers
{
    public class GlobalValue : MonoBehaviour
    {
        private static string Score = "coins";
    
        public static int TotalScore
        {
            get => PlayerPrefs.GetInt(Score); 
            set => PlayerPrefs.SetInt(Score, value); 
        }
    }
}
