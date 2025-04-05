using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour
{
    public float height = 2f;
    public float duration = 1f;
    
    private void OnEnable()
    {
        ManageClick.OnStartAnimation += HandleStartAnimation;
    }

    private void OnDisable()
    {
        ManageClick.OnStartAnimation -= HandleStartAnimation;
    }

    private void HandleStartAnimation(Transform _parent)
    {
        transform.position = _parent.position;
        StartCoroutine(MoveUpCoroutine(_parent));
    }

    private IEnumerator MoveUpCoroutine(Transform _parent)
    {
        Vector3 start = _parent.position;
        Vector3 end = start + Vector3.up * height;
        float t = 0f;

        while (t < duration)
        {
            transform.position = Vector3.Lerp(start, end, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = end;

        ObjectPooler.Instance.ReturnToPool(gameObject);
    }
}