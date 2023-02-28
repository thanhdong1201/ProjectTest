using UnityEngine;

public class EnemyState : MonoBehaviour
{     
    public CircleCollider2D[] circleCollider2Ds;
    [HideInInspector] public bool isEnableCollider;

    private void Update()
    {
        if (isEnableCollider)
        {
            EnableCollider();
        }
    }

    private void EnableCollider()
    {
        foreach (var CircleCollider2D in circleCollider2Ds)
        {
            if(CircleCollider2D != null)
            {
                CircleCollider2D.enabled = true;
            }
            
        }
    }
}
