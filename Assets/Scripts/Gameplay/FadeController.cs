using UnityEngine;
using UnityEngine.Events;

public class FadeController : MonoBehaviour
{
    public UnityEvent faded = new();
    [SerializeField] private float fadeTime = 0.25f;
    [SerializeField] private float elapsed = 0f;
    bool isFading = false;
    bool toBeDestroyed = false;
    void Update()
    {
        if (isFading)
        {
            elapsed += Time.deltaTime;
            SpriteRenderer spriteRenderer;
            if (TryGetComponent<SpriteRenderer>(out spriteRenderer))
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (fadeTime - elapsed) / fadeTime);
            }
            if (elapsed > fadeTime)
            {
                Debug.Log(elapsed);
                if (toBeDestroyed) Destroy(gameObject);
                else gameObject.SetActive(false);
                elapsed = 0f;
                faded.Invoke();
            }
        }
    }

    public void Fade(bool destroy)
    {
        isFading = true;
        toBeDestroyed = destroy;
    }
}
