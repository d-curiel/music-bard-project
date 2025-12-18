using UnityEngine;

    public class NoteLauncherComponent : MonoBehaviour
    {
        
        [SerializeField]
        private Transform startSpot;
        
        [SerializeField]
        private Transform endSpot;
        
        [SerializeField] private float spawnMinInterval = 1f;
        [SerializeField] private float spawnMaxInterval = 3f;
    
        public NoteObjectPool notePool;

        private System.Collections.IEnumerator Start()
        {
            while (true)
            {
                SendNote();
                yield return new WaitForSeconds(Random.Range(spawnMinInterval, spawnMaxInterval));
            }
        }

        void SendNote()
        {
            GameObject note = notePool.Get(startSpot.position);
            note.GetComponent<SingleNoteComponent>().Initialize(notePool, startSpot.position, endSpot.position);
        }
    }
