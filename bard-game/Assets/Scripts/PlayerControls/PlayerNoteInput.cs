using UnityEngine;
using UnityEngine.InputSystem;

[AutoRegisterInput(InputMode.Note)]
    public class PlayerNoteInput : MonoBehaviour, PlayerControls.INoteControlsActions, IInputActionHandler
    {
        // Enum para identificar las notas
        public enum NoteType { A, B, C, D }
    
        // Delegate genérico que incluye el tipo de nota
        public delegate void NoteInputPress(NoteType noteType, bool state);
        public event NoteInputPress OnNoteInputInteracted;
    
        // Estados individuales para cada nota
        private bool isAPressed = false;
        private bool isBPressed = false;
        private bool isCPressed = false;
        private bool isDPressed = false;
    
        public void OnA_Note(InputAction.CallbackContext context)
        {
            HandleNoteInput(context, ref isAPressed, NoteType.A);
        }
    
        public void OnB_Note(InputAction.CallbackContext context)
        {
            HandleNoteInput(context, ref isBPressed, NoteType.B);
        }
    
        public void OnC_Note(InputAction.CallbackContext context)
        {
            HandleNoteInput(context, ref isCPressed, NoteType.C);
        }
    
        public void OnD_Note(InputAction.CallbackContext context)
        {
            HandleNoteInput(context, ref isDPressed, NoteType.D);
        }
    
        private void HandleNoteInput(InputAction.CallbackContext context, ref bool isPressed, NoteType noteType)
        {
            if (context.performed && !isPressed)
            {
                isPressed = true;
                OnNoteInputInteracted?.Invoke(noteType, true);
            }
            else if (context.canceled && isPressed)
            {
                isPressed = false;
                OnNoteInputInteracted?.Invoke(noteType, false);
            }
        }
    
        public void Enable(PlayerControls controls)
        {
            controls.NoteControls.Enable();
            controls.NoteControls.AddCallbacks(this);
        }

        public void Disable(PlayerControls controls)
        {
            controls.NoteControls.Disable();
            controls.NoteControls.RemoveCallbacks(this);
        }
    }