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
        #endregion Translation Variables


        #region Unity Methods
        void Start()
        {
        
        }

        void Update()
        {
            if(Input.GetAxis("ScrollWheel") > 0 || Input.GetAxis("ScrollWheel") < 0)
            {
                Translate(Input.GetAxis("ScrollWheel") > 0);
                    Debug.Log(Input.GetAxis("ScrollWheel"));
            }
        }
        #endregion Unity Methods


        #region Translation Methods
        /// <summary>
        /// Moves this Transform along the chosen axis when the mouse wheel scrolls.
        /// </summary>
        void Translate (bool positiveMove)
        {
        //    transform.Translate()
        }
        #endregion Translation Methods
    }
}