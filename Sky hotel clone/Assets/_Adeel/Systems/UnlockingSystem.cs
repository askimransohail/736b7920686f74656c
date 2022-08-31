// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
// <copyright file="UnlockingSystem.cs" company="WittySol">
//   Copyright (c) 2022 WittySol. All rights reserved.
// </copyright>

// <author>
//   Adeel Riaz
//   adeelwitty@gmail.com
// </author>
// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_

using System.Collections;
using _Adeel.Managers;
using _Adeel.Player;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Adeel.Systems
{
    public class UnlockingSystem : MonoBehaviour
    {
        [SerializeField] private string prefKey;
        [SerializeField] private GameObject unlockProgressObj;
        [SerializeField] private GameObject unlockItemPrefab;
        [SerializeField] private SpriteRenderer progressBar;
        [SerializeField] private TextMeshPro txtUnlockPrice;
        [SerializeField] private int unlockPrice, unlockRemainPrice;
        [SerializeField] private float unlockIterationDelay = 0.1f, trigStayDelay = 0.5f;
        private bool isUnlocked, isInTrigArea;

        [SerializeField] private Collider trigCollider;

        // [Range(0, 1)][SerializeField] private float testProgress;

        private static readonly int Arc1 = Shader.PropertyToID("_Arc1");

        void Start()
        {
            isUnlocked = PlayerPrefs.GetInt(prefKey, 0) > 0;
            if (isUnlocked)
            {
                Unlocked();
            }
            else
            {
                unlockRemainPrice = PlayerPrefs.GetInt(prefKey + "_remainPrice", unlockPrice);
                txtUnlockPrice.text = unlockRemainPrice.ToString("00");
                UnlockProgress();
            }
        }

        private void OnValidate()
        {
            // progressBar.material.SetFloat(Arc1, 360f - testProgress * 360f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerMovement playerMovement) || isUnlocked) return;

            isInTrigArea = true;
            DOVirtual.DelayedCall(trigStayDelay, () => StartCoroutine(CO_Unlock(unlockIterationDelay)));
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement playerMovement))
            {
                isInTrigArea = false;
            }
        }

        IEnumerator CO_Unlock(float delay)
        {
            while (isInTrigArea)
            {
                yield return new WaitForSeconds(delay);

                if (ScoreManager.Ins.Score > 0)
                {
                    unlockRemainPrice--;
                    ScoreManager.Ins.Score--;

                    PlayerPrefs.SetInt(prefKey + "_remainPrice", unlockRemainPrice);
                    UnlockProgress();
                    txtUnlockPrice.text = unlockRemainPrice.ToString("00");

                    if (unlockRemainPrice <= 0)
                    {
                        UnlockItem();
                        break;
                    }
                }
            }
            yield return null;
        }



        private void UnlockProgress()
        {
            float progress = Mathf.Abs(1f - (float)unlockRemainPrice / (float)unlockPrice);
            progressBar.material.SetFloat(Arc1, 360f - progress * 360f);
        }

        public void Unlocked()
        {
            unlockProgressObj.SetActive(false);
            unlockItemPrefab.SetActive(true);
            if (trigCollider != null)
            {
                trigCollider.enabled = false;
            }
        }

        public void UnlockItem()
        {
            PlayerPrefs.SetInt(prefKey, 1);
            Unlocked();
            // GameObject item = Instantiate(unlockItemPrefab, transform.position + (Vector3.up * 0.25f), Quaternion.identity);
            unlockItemPrefab.transform.DOScale(0f, 0.5f).From().SetEase(Ease.OutBounce).SetDelay(0.25f);
        }
    }
}