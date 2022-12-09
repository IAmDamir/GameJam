using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static ObjectPooler;

public class Spear : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Vector3 target;
    private Rigidbody rb;
    [SerializeField] private float speed;
    private GameObject obj;

    public void Shoot(Vector3 pos, Quaternion rot)
    {
        if (obj == null)
        {
            obj = Instantiate(prefab, pos, rot);
            rb = obj.GetComponent<Rigidbody>();

            rb.velocity = transform.forward * speed;

            StartCoroutine(WaitForSeconds(0.5f));
        }
        else if (!obj.activeSelf)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);

            rb.velocity = transform.forward * speed;

            StartCoroutine(WaitForSeconds(0.5f));
        }
    }

    private IEnumerator WaitForSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
    }
}