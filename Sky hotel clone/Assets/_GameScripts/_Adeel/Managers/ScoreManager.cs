using _Adeel.Helpers;
using DG.Tweening;
using Lean.Pool;
using TMPro;
using UnityEngine;

namespace _Adeel.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI txtScore;
        
        [SerializeField] private Transform imgGem;

        [SerializeField] private bool savePrefs;
        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private Transform gemContainer, targetPoint;

        [SerializeField] private bool testingAmount;
        [SerializeField] private int testAmount;

        private int score;

        private Camera cam;

        public int Score 
        { 
            get => score;
            set 
            { 
                score = value;
                UpdateUiText();
                if (savePrefs)
                {
                    GlobalValue.TotalScore = score;
                }
            } 
        }

        private void Awake()
        {
            cam = Camera.main;
#if UNITY_EDITOR
            if (testingAmount)
            {
                GlobalValue.TotalScore = testAmount;
            }
#endif            
            if (savePrefs)
            {
                score = GlobalValue.TotalScore;
            }
            UpdateUiText();
        }

        private void UpdateUiText()
        {
            //Update UI text
             txtScore.text = score.ToString();
        }

        public void AddScore(Vector3 collectPoint, int amount)
        {
            Vector3 gemSpawnPos = cam.WorldToScreenPoint(collectPoint);
            GameObject gem = LeanPool.Spawn(gemPrefab, gemSpawnPos, Quaternion.identity, gemContainer);
            gem.transform.DOMove(targetPoint.position, 0.5f).OnComplete(() => 
            {
                imgGem.DOScale(0.1f, 0.075f).SetRelative(true).OnComplete(() => DOTween.Rewind(imgGem));
                LeanPool.Despawn(gem, 0.1f);
                Score += amount;
            });
        }
    }
}