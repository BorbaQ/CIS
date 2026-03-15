using UnityEngine;

public class ScaleAndDisable : MonoBehaviour
{
    public float duration = 1f;
    public float targetScale = 230f;   // set any scale you want in Inspector

    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(ScaleUp());
    }

    System.Collections.IEnumerator ScaleUp()
    {
        float time = 0f;
        Vector3 endScale = Vector3.one * targetScale;

        while (time < duration)
        {
            float t = time / duration;
            transform.localScale = Vector3.Lerp(Vector3.zero, endScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localScale = endScale;
        }

        gameObject.SetActive(false);
    }
}