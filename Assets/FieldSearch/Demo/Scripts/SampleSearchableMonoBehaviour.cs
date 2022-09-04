using UnityEngine;

namespace FieldSearch.Samples
{
    /// <summary>
    /// Sample monobehaviour with specific inspector
    /// </summary>
    public class SampleSearchableMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform Receiver;
        [SerializeField] private Transform Sender;
    }
}
