using System;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using UnityEngine;

    public class SingleNoteComponent : MonoBehaviour
    {
        private Vector3 startSpot;
        private Vector3 endSpot;

        [SerializeField]
        private float speed = 1f;
        
        private NoteObjectPool pool;

        public void Initialize(NoteObjectPool objectPool, Vector3 startPos, Vector3 endPos)
        {
            pool = objectPool;
            startSpot = startPos;
            endSpot = endPos;
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            gameObject.transform.position = startSpot;
            gameObject.transform.DOMove(endSpot, speed, false).SetEase(Ease.Linear).onComplete = () => pool.Return(gameObject);
        }

    }