using System.Collections;
using UnityEngine;

public class LockOnTargetUI : MonoBehaviour
{
    private PlayerCamera _playerCamera;
    private float _crosHairScale = 0.1f;
    private Transform _target;

    public void Construct(PlayerCamera playerCamera)
    {
        _playerCamera = playerCamera;
        _playerCamera.LockedTarget += PlayerCameraOnLockedTarget;
        _playerCamera.UnlockedTarget += OnUnlockedTarget;
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        _playerCamera.LockedTarget -= PlayerCameraOnLockedTarget;
        _playerCamera.UnlockedTarget -= OnUnlockedTarget;   
    }

    private void OnUnlockedTarget()
    {
        gameObject.SetActive(false);
        StopCoroutine(LockAtTarget());
    }

    private void PlayerCameraOnLockedTarget(Transform target)
    {
        gameObject.SetActive(true);
        _target = target;
        StartCoroutine(LockAtTarget());
    }

    private IEnumerator LockAtTarget()
    {
        while (gameObject.activeInHierarchy)
        {
            transform.position = _target.position;
            Vector3 dir = _playerCamera.transform.position - transform.position;
            var newDir = new Vector3(dir.x, 0, dir.z);
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.localScale = Vector3.one *
                                   ((_playerCamera.transform.position - transform.position).magnitude * _crosHairScale);
            
            yield return null;
        }
    }
}
