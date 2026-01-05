using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelLands
{    public class BirdMover : MonoBehaviour
    {
        public enum MoveStyle
        {
            HOP,
            FLY
        }

        [SerializeField]
        private List<GameObject> path = new List<GameObject>();

        [SerializeField]
        // The speed that the critter moves along the path.
        private float speed = 1f;

        [SerializeField]
        private MoveStyle moveStyle;

        [SerializeField]
        [HideInInspector]
        // Modifier to move speed used by animations to only move the critter
        // on certain frames. Use `speed` not `animationMoveSpeed` to control
        // the speed the critter moves along the path.
        private float animationMoveSpeed = 1f;

        private readonly float closeDistance = 0.001f;
        private int targetIdx = 0;
        private Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();

            if (path == null || path.Count == 0)
            {
                Debug.LogWarning($"{name} has no path");
            }
        }

        void Update()
        {
            // Only run if we have at least two points to move between.
            if (path == null || path.Count < 2) return;

            // Set the animator based on our move style.
            if (anim != null)
            {
                anim.SetBool("Flying", moveStyle == MoveStyle.FLY);
            }

            Transform currTarget = path[targetIdx].transform;
            transform.position = Vector3.MoveTowards(transform.position, currTarget.transform.position, speed * Time.deltaTime * animationMoveSpeed);
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
