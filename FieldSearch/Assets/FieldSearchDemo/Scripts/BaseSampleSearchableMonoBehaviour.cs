using UnityEngine;

namespace FieldSearch.Samples
{
    /// <summary>
    /// Base class for <see cref="SampleDefaultMonoBehaviour"/>
    /// </summary>
    public class BaseSampleSearchableMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform[] Receiver2;
        [SerializeField] private Transform Sender2;
    }
}
