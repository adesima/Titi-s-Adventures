using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelLands
{    public class CritterMover : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> path = new List<GameObject>();

        [SerializeField]
        // The speed that the critter moves along the path.
        private float speed = 1f;

        private readonly float closeDistance = 0.001f;
        private int targetIdx = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (path == null || path.Count == 0)
            {
                Debug.LogWarning($"{name} has no path");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Only run if we have at least two points to move between.
            if (path == null || path.Count < 2) return;

            Transform currTarget = path[targetIdx].transform;
            transform.position = Vector3.MoveTowards(transform.position, currTarget.transform.position, speed * Time.deltaTime);
            SetDirection();

            if (Vector3.Distance(transform.position, currTarget.position) < closeDistance)
            {
                transform.position = currTarget.position;
                targetIdx++;
                if (targetIdx == path.Count) { targetIdx = 0; }
            }
        }

        private void SetDirection()
        {
            Transform currTarget = path[targetIdx].transform;
            float yRotation = currTarget.transform.position.x < transform.position.x ? 0 : 180;
            transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
