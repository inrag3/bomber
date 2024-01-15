using UnityEngine;


// Player -> Instantiate Bomb -> (Как получить информацию о том, как дамажить других?)
[RequireComponent(typeof(BoxCollider))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _damage = 50;
    [SerializeField] private int _range = 3;
    [SerializeField] private float _lifeTime = 3f;
    
    private const string Bomber = "Bomber";    

    private BoxCollider _collider;
    
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
        //Запуск какой-то анимации
    }
    
    // TODO вынести детекцию в отдельный компонент
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Bomber))
        {
            _collider.isTrigger = false;
        }
    }

    private void OnDestroy()
    {
        //Нужно какой-то explosion реализовать
        var colliders = new Collider[6];
        var size = Physics.OverlapSphereNonAlloc(transform.position, 1.5f, colliders, _layerMask);
        print(size);
        for (var i = 0; i < size; i++)
        {
            if (!colliders[i].TryGetComponent(out IDamageable damageable)) //Если нет в _layerMask IDamageable
                continue;
            damageable.TakeDamage(_damage);
        }
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
#endif
    
}



