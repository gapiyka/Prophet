using System.Collections.Generic;
using Items.Data;
using UnityEngine;

namespace Items.Storage
{
    [CreateAssetMenu(fileName = "ItemsRarityStorage", menuName ="ItemsSystem/ItemsRarityStorage")]
    public class ItemRarityStorage : ScriptableObject
    {
        [field: SerializeField] public List<RarityDescriptor> RarityDescriptors { get; private set; }
    }
}