using System;
using UnityEngine;

    public class NoteHandlerComponent : MonoBehaviour
    {
        
        private PlayerNoteInput playerInput;
    
        [Header("Note Configuration")]
        [SerializeField]
        private PlayerNoteInput.NoteType assignedNote = PlayerNoteInput.NoteType.A;
    
        [Header("Detectors")]
        [SerializeField]
        private NoteDetectorComponent goodNoteDetector;
        [SerializeField]
        private NoteDetectorComponent greatNoteDetector;
    
        public DetectedNoteEvent goodNote;
        public DetectedNoteEvent greatNote;
    
        private void Start()
        {
            playerInput = PlayerInputSystem.Instance.GetHandler<PlayerNoteInput>();
            playerInput.OnNoteInputInteracted += OnNoteInputPress;
        }
    
        private void OnDestroy()
        {
            if (playerInput)
            {
                playerInput.OnNoteInputInteracted -= OnNoteInputPress;
            }
        }
    
        private void OnEnable()
        {
            goodNoteDetector.OnNoteDetected += GoodNoteDetected;
            greatNoteDetector.OnNoteDetected += GreatNoteDetected;
        }
    
        private void OnDisable()
        {
            goodNoteDetector.OnNoteDetected -= GoodNoteDetected;
            greatNoteDetector.OnNoteDetected -= GreatNoteDetected;
        }
    
        private void OnNoteInputPress(PlayerNoteInput.NoteType noteType, bool state)
        {
            // Solo procesar si es la nota asignada a este handler
            if (noteType != assignedNote)
                return;
        
            if (state)
            {
                if (greatNote is { isDetected: true })
                {
                    greatNote.collider.gameObject.SetActive(false);
                    Debug.Log($"Great note {noteType} detected!");
                }
                else if (goodNote is { isDetected: true })
                {
                    goodNote.collider.gameObject.SetActive(false);
                    Debug.Log($"Good note {noteType} detected!");
                }
                else
                {
                    Debug.Log($"Miss on note {noteType}!");
                }
            }
        }
    
        private void GoodNoteDetected(DetectedNoteEvent noteEvent)
        {
            goodNote = noteEvent;
        }
    
        private void GreatNoteDetected(DetectedNoteEvent noteEvent)
        {
            greatNote = noteEvent;
        }
    }