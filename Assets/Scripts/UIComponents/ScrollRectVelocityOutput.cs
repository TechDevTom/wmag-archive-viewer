using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Tom.Events;


namespace Tom.UI
{
    public class ScrollRectVelocityOutput : MonoBehaviour
    {
        #region UI Variables
        [Header("UI")]
        /// <summary>
        /// The ScrollRect used to transform a 3D object.
        /// </summary>
        [SerializeField] private ScrollRect scrollRect;
        #endregion UI Variables

        #region Event Variables
        /// <summary>
        /// An event used to broadcast the Horizontal Velocity of a ScrollRect.
        /// </summary>
        [SerializeField] private FloatEvent onVelocityHorizontalChanged;

        /// <summary>
        /// An event used to broadcast the Vertical Velocity of a ScrollRect.
        /// </summary>
        [SerializeField] private FloatEvent onVelocityVerticalChanged;
        #endregion Event Variables


        #region Unity Methods
        void Start ()
        {
            scrollRect.onValueChanged.AddListener(OutputVelocity);
        }
        #endregion Unity Methods


        #region 3D Methods
        /// <summary>
        /// Transforms the related 3D object based on the input from the related ScrollRect.
        /// </summary>
        void OutputVelocity (Vector2 newValues)
        {
            onVelocityHorizontalChanged.Invoke(scrollRect.velocity.x);
            onVelocityVerticalChanged.Invoke(scrollRect.velocity.y);
        }
        #endregion 3D Methods
    }
}