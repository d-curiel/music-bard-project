using UnityEngine;

    public class NoteLauncherComponent : MonoBehaviour
    {
        
        [Header("Note Configuration")]
        [SerializeField]
        private PlayerNoteInput.NoteType assignedNote = PlayerNoteInput.NoteType.A;
        
        [SerializeField]
        private Transform startSpot;
        
        [SerializeField]
        private Transform endSpot;
    
        public NoteObjectPool notePool;

        public void SendNote()
        {
            //TODO: Cambiar al POOLTIPADO
            GameObject note = notePool.Get(startSpot.position);
            note.GetComponent<SingleNoteComponent>().Initialize(notePool, startSpot.position, endSpot.position);
        }

        public PlayerNoteInput.NoteType GetAssignedNote()
        {
            return assignedNote;
        }
    }
