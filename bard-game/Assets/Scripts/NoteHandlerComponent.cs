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
    
        public bool isGoodNote = false;
        public bool isGreatNote = false;
    
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
                if (isGreatNote)
                {
                    Debug.Log($"Great note {noteType} detected!");
                }
                else if (isGoodNote)
                {
                    Debug.Log($"Good note {noteType} detected!");
                }
                else
                {
                    Debug.Log($"Miss on note {noteType}!");
                }
            }
        }
    
        private void GoodNoteDetected(bool detected)
        {
            isGoodNote = detected;
        }
    
        private void GreatNoteDetected(bool detected)
        {
            isGreatNote = detected;
        }
    }