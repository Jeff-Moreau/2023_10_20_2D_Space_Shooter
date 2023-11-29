using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private ProjectileSO mProjectileData = null;
    [SerializeField] private Rigidbody2D mRigidbody = null;

    private GameObject mPlayer;

    private void OnEnable()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        var direction = mPlayer.transform.position - transform.position;
        mRigidbody.velocity = new Vector2(direction.x, direction.y).normalized * mProjectileData.GetSpeed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
