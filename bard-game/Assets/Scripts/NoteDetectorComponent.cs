using System;
using UnityEngine;

    public class NoteDetectorComponent : MonoBehaviour
    {
        public Action<DetectedNoteEvent> OnNoteDetected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            DetectedNoteEvent noteEvent = new DetectedNoteEvent();
            noteEvent.isDetected = true;
            noteEvent.collider = other;
            OnNoteDetected?.Invoke(noteEvent);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            DetectedNoteEvent noteEvent = new DetectedNoteEvent();
            noteEvent.isDetected = false;
            noteEvent.collider = other;
            OnNoteDetected?.Invoke(noteEvent);
        }
    }

    public class DetectedNoteEvent
    {
        public bool isDetected;
        
        public Collider2D collider;
    }