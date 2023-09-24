using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    
    private Camera _playerCamera;
    private Enemy _enemy;
    private float _startHealth;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _enemy.Health.StateChanged += UpdateValue;
        _startHealth = _enemy.Health.BaseValue;
        _healthBar.fillOrigin = 1;
    }

    public void Construct(Camera camera)
    {
        _playerCamera = camera;
        StartCoroutine(LockAtTarget());
    }
    private void UpdateValue() => 
        _healthBar.fillAmount = _enemy.Health.Value / _startHealth;

    private IEnumerator LockAtTarget()
    {
        while (gameObject.activeInHierarchy)
        {
            Vector3 dir = _playerCamera.transform.position - transform.position;
            var newDir = new Vector3(dir.x, 0, dir.z);
            transform.rotation = Quaternion.LookRotation(newDir);

            yield return null;
        }
    }
    private void OnDestroy() => 
        _enemy.Health.StateChanged -= UpdateValue;
}
