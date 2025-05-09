using UnityEngine;
using System.Collections;

namespace Wargon.TestGame
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer healthBar;
        [SerializeField] private SpriteRenderer damageBar;

        [SerializeField] private float shrinkDelay = 0.5f;
        [SerializeField] private float shrinkSpeed = 1.0f;
        private TimeService timeService;
        private Coroutine damageCoroutine;

        public void Restart()
        {
            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
            healthBar.transform.localScale = new Vector3(1, healthBar.transform.localScale.y, healthBar.transform.localScale.y);
            damageBar.transform.localScale = new Vector3(1, healthBar.transform.localScale.y, healthBar.transform.localScale.y);
        }
        public void ApplyDamage(float currentHealth, int maxHealth)
        {
            gameObject.SetActive(true);

            var targetScale = Mathf.Clamp01(currentHealth / maxHealth);

            healthBar.transform.localScale = new Vector3(targetScale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
            damageCoroutine = StartCoroutine(AnimateDamageBar(targetScale));
        }

        private IEnumerator AnimateDamageBar(float targetScale)
        {
            yield return new WaitForSeconds(shrinkDelay);
            while (Mathf.Abs(damageBar.transform.localScale.x - targetScale) > 0.001f)
            {
                var currentScale = damageBar.transform.localScale.x;
                var newScale = Mathf.MoveTowards(currentScale, targetScale, shrinkSpeed * Time.deltaTime);
                damageBar.transform.localScale = new Vector3(newScale, damageBar.transform.localScale.y, damageBar.transform.localScale.z);
                yield return null;
            }

            damageBar.transform.localScale = new Vector3(targetScale, damageBar.transform.localScale.y, damageBar.transform.localScale.z);
            damageCoroutine = null;
        }
    }
}