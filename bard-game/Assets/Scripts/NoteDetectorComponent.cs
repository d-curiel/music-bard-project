using System;
using UnityEngine;

    public class NoteDetectorComponent : MonoBehaviour
    {
        public Action<bool> OnNoteDetected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnNoteDetected?.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnNoteDetected?.Invoke(false);
        }
    }