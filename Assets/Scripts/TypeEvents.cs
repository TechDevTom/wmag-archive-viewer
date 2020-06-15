using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


namespace Tom.Events
{
    [System.Serializable]
    /// <summary>
    /// An event that allows a String to be used as a parameter.
    /// </summary>
    public class StringEvent : UnityEvent<string> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a bool to be used as a parameter.
    /// </summary>
    public class BoolEvent : UnityEvent<bool> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Int to be used as a parameter.
    /// </summary>
    public class IntEvent : UnityEvent<int> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Float to be used as a parameter.
    /// </summary>
    public class FloatEvent : UnityEvent<float> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Float and an Axis to be used as parameters.
    /// </summary>
    public class FloatAxisEvent : UnityEvent<float, Axis> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Vector2 to be used as a parameter.
    /// </summary>
    public class Vector2Event : UnityEvent<Vector2> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Vector3 to be used as a parameter.
    /// </summary>
    public class Vector3Event : UnityEvent<Vector3> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a GameObject to be used as a parameter.
    /// </summary>
    public class GameObjectEvent : UnityEvent<GameObject> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Transform to be used as a parameter.
    /// </summary>
    public class TransformEvent : UnityEvent<Transform> { }

    [System.Serializable]
    /// <summary>
    /// An event that allows a Material to be used as a parameter.
    /// </summary>
    public class MaterialEvent : UnityEvent<Material> { }
}