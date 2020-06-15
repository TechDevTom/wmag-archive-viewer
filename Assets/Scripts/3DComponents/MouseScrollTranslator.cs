using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Tom.Translate
{
    public class MouseScrollTranslator : MonoBehaviour
    {
        #region Translation Variables
        [Header ("Translation")]
        /// <summary>
        /// The minimum position at which this Transform can be at.
        /// </summary>
        [SerializeField] private Vector3 minPosition;

        /// <summary>
        /// The maxiumum position at which this Transform can be at.
        /// </summary>
        [SerializeField] private Vector3 maxPosition;

        /// <summary>
        /// Deteremines how this Transform translates based on the ScrollWheel input.
        /// </summary>
        [SerializeField] private bool inverseTranslation;

        /// <summary>
        /// The percentage of the min/max position at which this Transform is located at.
        /// </summary>
        private float percentageOfMinMax;

        /// <summary>
        /// The value that can bsed used to multiply the speed at which the translation happens.
        /// </summary>
        [SerializeField] private float translationMultiplier;
        #endregion Translation Variables


        #region Unity Methods
        void Start()
        {
            percentageOfMinMax = 100f;
        }

        void Update()
        {
            if(Input.GetAxis("ScrollWheel") > 0 || Input.GetAxis("ScrollWheel") < 0)
            {
                Translate(Input.GetAxis("ScrollWheel") > 0);
            }
        }
        #endregion Unity Methods


        #region Translation Methods
        /// <summary>
        /// Moves this Transform along the chosen axis when the mouse wheel scrolls.
        /// </summary>
        void Translate (bool positiveMove)
        {
            percentageOfMinMax += (inverseTranslation? -Input.GetAxis("ScrollWheel") : Input.GetAxis("ScrollWheel")) * translationMultiplier;
            percentageOfMinMax = Mathf.Clamp(percentageOfMinMax, 0f, 100f);

            Vector3 newPosition = minPosition + new Vector3(((maxPosition.x - minPosition.x) / 100f) * percentageOfMinMax,
                                                            ((maxPosition.y - minPosition.y) / 100f) * percentageOfMinMax,
                                                            ((maxPosition.z - minPosition.z) / 100f) * percentageOfMinMax);
            transform.localPosition = newPosition;
        }
        #endregion Translation Methods
    }
}