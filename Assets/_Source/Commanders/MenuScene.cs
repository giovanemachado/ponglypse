using RouteTeamStudio.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RouteTeamStudio.Commanders
{
    public class MenuScene : Commander
    {
        int _bestScore = 0;
        [SerializeField] TextMeshProUGUI _bestScoreText;

        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void Start()
        {
            _bestScore = PlayerPrefs.GetInt("score");

            if (_bestScore > 0)
            {
                _bestScoreText.text = "Best score: " + _bestScore;
                _bestScoreText.gameObject.SetActive(true);
            }
        }
    }
}
