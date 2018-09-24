using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelBlock : MonoBehaviour
{
    [SerializeField]
    private bool showBlockBounds = false;

    [Header("Block Properties")]
    [SerializeField]
    private float blockWidth;

    public float GetBlockWidth()
    {
        return blockWidth;
    }

    public void HideBlockBounds()
    {
        showBlockBounds = false;
    }

    private void OnDrawGizmos()
    {
        if(showBlockBounds)
        {
            // Drawing bounds of the level block
            Vector3 offsetHorizontal = new Vector3(blockWidth, 0, 0);
            Vector3 offsetVertical = new Vector3(0, 50, 0);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position - offsetHorizontal, transform.position - offsetHorizontal + offsetVertical);
            Gizmos.DrawLine(transform.position + offsetHorizontal, transform.position + offsetHorizontal + offsetVertical);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position - offsetHorizontal, transform.position + offsetHorizontal);
        }
    }
}
