using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Wargon.TestGame
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image damageBar;

        [SerializeField] private float shrinkDelay = 0.1f;
        [SerializeField] private float shrinkSpeed = 1.0f;

        private Coroutine damageCoroutine;

        public void ApplyDamage(int damage, int currentHealth, int maxHealth)
        {
            var targetFill = Mathf.Clamp01(currentHealth / maxHealth);
            healthBar.fillAmount = targetFill;

            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);

            damageCoroutine = StartCoroutine(AnimateDamageBar(targetFill));
        }

        private IEnumerator AnimateDamageBar(float targetFill)
        {
            yield return new WaitForSeconds(shrinkDelay);

            while (damageBar.fillAmount > targetFill)
            {
                damageBar.fillAmount -= Time.deltaTime * shrinkSpeed;
                if (damageBar.fillAmount < targetFill)
                    damageBar.fillAmount = targetFill;

                yield return null;
            }

            damageCoroutine = null;
        }
    }
}