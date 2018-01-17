using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGridManager : MonoBehaviour
{

    public LayerMask m_ObstacleMask;
    public Vector2 m_vGridSize;
    public float m_fHalfNodeWidth;
    public GameObject m_gCapsule;

    Node[,] m_aGrid;

    void Start()
    {
        InitGrid();
    }

    void Update()
    {
                
    }
    void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(m_vGridSize.x, 1, m_vGridSize.y));

        if (m_aGrid != null)
        {
            foreach (Node n in m_aGrid)
            {
                if (n.m_bIsBlocked)
                    Gizmos.color = Color.red;
                if (n == CheckCapsule())
                    Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(n.m_vPosition, new Vector3(1, 1, 1));
                Gizmos.color = Color.white;
            }
        }
    }

    void InitGrid()
    {
        m_aGrid = new Node[(int)m_vGridSize.x, (int)m_vGridSize.y];

        for (int x = 0; x < m_vGridSize.x; x++)
        {           
            for (int y = 0; y < m_vGridSize.y; y++)
            {
                Vector3 pos = new Vector3(m_vGridSize.x / 2 - m_fHalfNodeWidth - x * m_fHalfNodeWidth * 2, 0, (m_vGridSize.y / 2 - m_fHalfNodeWidth - y * m_fHalfNodeWidth * 2));
                bool blocked = Physics.CheckSphere(pos, m_fHalfNodeWidth, m_ObstacleMask);
                m_aGrid[x, y] = new Node(blocked, pos);
                
            }
        }


    }

    Node CheckCapsule()
    {
        Vector3 capsPos = m_gCapsule.transform.position;

        float capsX = (-capsPos.x + m_vGridSize.x / 2) / m_vGridSize.x;
        float capsY = (-capsPos.z + m_vGridSize.y / 2) / m_vGridSize.y;

        capsX = Mathf.Clamp01(capsX);
        capsY = Mathf.Clamp01(capsY);

        int x = Mathf.RoundToInt((m_vGridSize.x -1) * capsX);
        int y = Mathf.RoundToInt((m_vGridSize.y -1) * capsY);

        return m_aGrid[x, y];


    }

}
