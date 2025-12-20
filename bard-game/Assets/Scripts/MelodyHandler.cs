using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MelodyHandler : MonoBehaviour
    {
        [SerializeField]
        List<NoteLauncherComponent>  noteLauncherComponents;

        public MelodyData melody;

        private void Start()
        {
            StartCoroutine(PlayMelody());
        }

        private IEnumerator PlayMelody()
        {
            foreach (var currentNote in melody.notes)
            {
                NoteLauncherComponent launcher =
                    noteLauncherComponents.Find(launcher => launcher.GetAssignedNote() == currentNote.assignedNote);
                launcher.SendNote();
                yield return new WaitForSeconds(currentNote.duration);
            }
        }
        
        
    }

    