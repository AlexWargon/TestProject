using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wargon.UI;

namespace Wargon.TestGame
{
    public class LosePopup : Popup
    {
        [SerializeField] private Button restartGameBtn;
        public override void OnCreate()
        {
            restartGameBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }
    }
}