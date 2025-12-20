using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Melody", menuName = "MelodyData", order = 1)]
    public class MelodyData : ScriptableObject
    {
        public List<NoteData> notes;
    }
