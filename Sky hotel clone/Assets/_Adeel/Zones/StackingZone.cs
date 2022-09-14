// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
// <copyright file="StackingZone.cs" company="WittySol">
//   Copyright (c) 2022 WittySol. All rights reserved.
// </copyright>

// <author>
//   Adeel Riaz
//   adeelwitty@gmail.com
// </author>
// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_

using System.Collections;
using _Adeel.Player;
using DG.Tweening;
using UnityEngine;


namespace _Adeel.Zones
{
    public class StackingZone : MonoBehaviour
    {
        [SerializeField] private float trigStayDelay, stackDelay;
        [SerializeField] private Transform stackPoint;

        private bool isInTrigArea;
        private Coroutine stackCoroutine;
        private PlayerStacking playerStacking;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out playerStacking))
            {
                isInTrigArea = true;
                DOVirtual.DelayedCall(trigStayDelay, () =>
                {
                    stackCoroutine = StartCoroutine(CO_Process());
                });
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out playerStacking))
            {
                isInTrigArea = false;
                if (stackCoroutine != null)
                {
                    StopCoroutine(stackCoroutine);
                    stackCoroutine = null;
                }
            }
        }


        private IEnumerator CO_Process()
        {
            while (isInTrigArea)
            {
                yield return new WaitForSeconds(stackDelay);
                if (playerStacking)
                {
                    playerStacking.StackItem(stackPoint);
                }
            }
        }
    }
}