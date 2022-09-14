// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
// <copyright file="PlayerStacking.cs" company="WittySol">
//   Copyright (c) 2022 WittySol. All rights reserved.
// </copyright>

// <author>
//   Adeel Riaz
//   adeelwitty@gmail.com
// </author>
// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_

using DG.Tweening;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;

namespace _Adeel.Player
{
    public class PlayerStacking : MonoBehaviour
    {
        [SerializeField] private float stackItemHeight = 0.5f, stackTime = 1f, destackTime = 1f;
        [SerializeField] private int maxStackLimit;
        [SerializeField] private GameObject stackItemPrefab;
        [SerializeField] private Transform stackPoint, stackContainer;
        [SerializeField] private FastIKFabric ikLeftHand, ikRightHand;
        
        [SerializeField] private List<GameObject> stackItemsList;
        
        private float height;
        private bool ikState;

        // _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+ //

        #region Stacking & Destacking

        public void StackItem(Transform origin)
        {
            if (stackItemsList.Count < maxStackLimit)
            {
                var item = Instantiate(stackItemPrefab, stackContainer, true);
                item.transform.position = origin.position;

                var targetPoint = stackPoint.localPosition + Vector3.up * height;
                item.transform.DOLocalJump(targetPoint, 0.75f, 1, stackTime);
                item.transform.localRotation = Quaternion.identity;
                item.transform.DOPunchScale(targetPoint, 0.1f, 3, 0.1f);

                height += stackItemHeight;
                stackItemsList.Add(item);

                if (!ikState)
                {
                    SetIkState(true);
                }
            }
        }

        public void DestackItem(Transform target)
        {
            if (stackItemsList.Count > 0)
            {
                var item = stackItemsList[stackItemsList.Count - 1];
                item.transform.parent = null;
                item.transform.DOJump(target.position, 1.5f, 1, destackTime)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => item.SetActive(false));

                stackItemsList.Remove(item);
                height -= stackItemHeight;
                
                SetIkState(stackItemsList.Count > 0);
            }
        }

        private void SetIkState(bool state)
        {
            ikLeftHand.enabled = state;
            ikRightHand.enabled = state;

            ikState = state;
        }

        #endregion
    }
}