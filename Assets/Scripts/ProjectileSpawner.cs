using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private ObjectPooler pooler;
    [SerializeField] private float speed = 20;
    [SerializeField] private float lerpDuration = 0.1f;
    [SerializeField] private float startRotationAngle = 20;
    [SerializeField] private float targetRotationAngle = -20;
    private bool rotating;

    private void Start()
    {
        pooler = ObjectPooler.Instance;

        transform.rotation = transform.parent.rotation;
    }

    public void Shoot(Transform pos)
    {
        rb = pooler.SpawnFromPool("Projectile", pos.position, pos.rotation).GetComponent<Rigidbody>();

        rb.velocity = pos.TransformDirection(Vector3.forward * 20);
    }

    public void Shoot(Vector3 pos, Quaternion rot)
    {
        GameObject obj = pooler.SpawnFromPool("Projectile", pos, rot);
        rb = obj.GetComponent<Rigidbody>();

        StartCoroutine(Rotate90());

        rb.velocity = transform.forward * speed;

        StartCoroutine(WaitForSeconds(0.1f, obj));
    }

    private IEnumerator WaitForSeconds(float duration, GameObject obj)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }

    private IEnumerator Rotate90()
    {
        rotating = true;
        float timeElapsed = 0;
        Quaternion startRotation = transform.rotation * Quaternion.Euler(0, startRotationAngle, 0);
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, targetRotationAngle, 0);
        while (timeElapsed < lerpDuration)
        {
            transform.rotation = Quaternion.RotateTowards(startRotation, targetRotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        rotating = false;
    }
}