// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
// <copyright file="DestackingZone.cs" company="WittySol">
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
    public class DestackingZone : MonoBehaviour
    {
        [SerializeField] private DestackType destackType;
        [SerializeField] private float trigStayDelay, destackDelay;
        [SerializeField] private Transform dropPoint;

        private bool canDestack = true;

        private bool isInTrigArea;
        private Coroutine destackCoroutine;
        private PlayerStacking playerStacking;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out playerStacking))
            {
                isInTrigArea = true;
                DOVirtual.DelayedCall(trigStayDelay, () =>
                {
                    switch (destackType)
                    {
                        case DestackType.Once:
                            DestackOnce();
                            break;
                        case DestackType.Multiple:
                            destackCoroutine = StartCoroutine(CO_Process());
                            break;
                    }
                });
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out playerStacking))
            {
                isInTrigArea = false;
                if (destackCoroutine != null)
                {
                    StopCoroutine(destackCoroutine);
                    destackCoroutine = null;
                }
            }
        }

        public void DestackOnce()
        {
            if (playerStacking && canDestack)
            {
                canDestack = false;
                DOVirtual.DelayedCall(destackDelay, () => playerStacking.DestackItem(dropPoint));
                DOVirtual.DelayedCall(10f, () => canDestack = true);
            }
        }

        private IEnumerator CO_Process()
        {
            while (isInTrigArea)
            {
                yield return new WaitForSeconds(destackDelay);
                if (playerStacking)
                {
                    playerStacking.DestackItem(dropPoint);
                }
            }
        }
    }
}