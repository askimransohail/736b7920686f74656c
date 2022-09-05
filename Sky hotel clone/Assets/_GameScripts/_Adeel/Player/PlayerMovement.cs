// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
// <copyright file="PlayerMovement.cs" company="WittySol">
//   Copyright (c) 2022 WittySol. All rights reserved.
// </copyright>

// <author>
//   Adeel Riaz
//   adeelwitty@gmail.com
// </author>
// _+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_

using UnityEngine;

namespace _Adeel.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private bool canMove = true;
        [SerializeField] private float moveSpeed = 7f, smoothTime = 0.1f, turnSpeed = 8f;
        [SerializeField] private DynamicJoystick dynamicJoystick;
        private Rigidbody rb;
        private Vector3 velocity;
        private float smoothInputMagnitude, smoothMoveVelocity;
        private float angle, horizontal, vertical;

        private Animator animator;

        #region Unity Methods

        // Start is called before the first frame update
        void Start()
        {
            CacheComponents();
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        #endregion

        private void CacheComponents()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            if (dynamicJoystick == null)
            {
                dynamicJoystick = FindObjectOfType<DynamicJoystick>();
            }
        }

        private void GetInput()
        {
            Vector3 inputDir = Vector3.zero;
            if (canMove)
            {
                horizontal = dynamicJoystick.Horizontal;
                vertical = dynamicJoystick.Vertical;
                inputDir = new Vector3(horizontal, 0, vertical).normalized;
            }

            float inputMagnitude = inputDir.magnitude;
            smoothInputMagnitude =
                Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothTime);

            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
            
            animator.SetFloat("MoveSpeed", smoothInputMagnitude);

            velocity = transform.forward * moveSpeed * smoothInputMagnitude;
        }

        private void MovePlayer()
        {
            rb.MoveRotation(Quaternion.Euler(Vector3.up * angle));
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }
    }
}