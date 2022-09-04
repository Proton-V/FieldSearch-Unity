using UnityEngine;

namespace FieldSearch.Samples
{
    /// <summary>
    /// Sample monobehaviour without specific inspector
    /// </summary>
    public class SampleDefaultMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform Receiver;
        [SerializeField] private Transform Sender;
    }
}
