using UnityEngine;

public class NormalBulletHitBehaviour : BulletHitBehaviourBase
{
	[SerializeField]
	private ParticleSystem _hitParticle;

	public override void Trigger(Transform target)
    {
        if (target.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.Damage(Damage);
        }

		_hitParticle.transform.parent = null;
		_hitParticle.Play();
        Destroy(_hitParticle, 0.3f);
        Destroy(gameObject);
    }
}
