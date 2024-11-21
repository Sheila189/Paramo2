using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    public Canvas damageCanvas;
    public float fadeSpeed = 5f;
    private bool isDamaged = false;

    void Update()
    {
        if (isDamaged)
        {
            damageCanvas.gameObject.SetActive(true);
        }
        else
        {
            if (damageCanvas.gameObject.activeSelf)
            {
                damageCanvas.gameObject.SetActive(false);
            }
        }
        isDamaged = false;
    }

    public void TakeDamage()
    {
        isDamaged = true;
    }
}
