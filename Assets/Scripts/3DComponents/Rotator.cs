using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Tom.Tranform
{
    public class Rotator : MonoBehaviour
    {
        #region Rotation Variables
        [Header ("Rotation")]
        /// <summary>
        /// The axis that this object should rotate along when rotating.
        /// </summary>
        [SerializeField] private Axis rotationAxis;

        /// <summary>
        /// The Vector3 value of the chosen rotationAxis.
        /// </summary>
        private Vector3 rotationAxisValue;

        /// <summary>
        /// The speed at which this Transform should rotate around the chosen axis.
        /// </summary>
        [SerializeField] private float rotationSpeed;

        /// <summary>
        /// Inverses the direction in which this Transform rotates.
        /// </summary>
        [SerializeField] private bool invertRotation;
        #endregion Rotation Variables


        #region Start Methods
        public void Start ()
        {
            switch (rotationAxis)
            {
                case Axis.X:

                    rotationAxisValue = invertRotation? -Vector3.right : Vector3.right;
                    break;

                case Axis.Y:

                    rotationAxisValue = invertRotation ?  -Vector3.up : Vector3.up;
                    break;

                case Axis.Z:

                    rotationAxisValue = invertRotation ? -Vector3.forward : Vector3.forward;
                    break;
            }
        }
        #endregion Start Methods


        #region Rotation Methds
        /// <summary>
        /// Rotates this Transform along the chosen axis.
        /// </summary>
        /// <param name="rotationValue"></param>
        public void Rotate(float rotationValue)
        {
            transform.Rotate(rotationAxisValue * ((rotationSpeed * rotationValue) * Time.deltaTime));
        }
        #endregion Rotation Methds
    }
}
