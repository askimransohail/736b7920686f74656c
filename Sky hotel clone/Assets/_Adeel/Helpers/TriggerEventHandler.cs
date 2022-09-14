// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TriggerEventHandler.cs" company="WittySol">
//   Copyright (c) 2018 WittySol. All rights reserved.
// </copyright>

// <author>
//   Adeel Riaz
//   adeelwitty@gmail.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace _Adeel.Helpers
{
    public class TriggerEventHandler : MonoBehaviour
    {

        #region Variables
        public string tagToCompare = "Player";

        public UnityEvent onEnter, onExit, onStay;

        #endregion

        #region Unity Methods

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                onEnter.Invoke();
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                onExit.Invoke();
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                onStay.Invoke();
            }
        }

        #endregion
    }
}
