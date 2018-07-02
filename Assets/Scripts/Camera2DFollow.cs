using System;
using UnityEngine;

// this comes from the Unity Standard Assets
namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
		[Tooltip ("0 faz o personagem ficar centralizado verticalmente na tela")]
		public float ajustePlayerVertical = 1.5f;

		// private variables
        float m_OffsetZ;
        Vector3 m_LastTargetPosition;
        Vector3 m_CurrentVelocity;
        Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
			m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;

			// if target not set, then set it to the player
			if (target==null) {
				target = GameObject.FindGameObjectWithTag("Player").transform;
			}

			if (target==null)
				Debug.LogError("Target not set on Camera2DFollow.");

        }

        // Update is called once per frame
		private void Update()
        {
			if (target == null)
				return;

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
				m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

			//Linha original
         //   Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;

			//alteracao feita por Mel, ajuste para colocar a camera um pouco acima do player ao invez de centralizar nele
			Vector3 aheadTargetPos = new Vector3(target.position.x, target.position.y + ajustePlayerVertical, target.position.z) + m_LookAheadPos + Vector3.forward*m_OffsetZ;

            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
