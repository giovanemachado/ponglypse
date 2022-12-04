using RouteTeamStudio.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RouteTeamStudio.Commanders
{
    public class MenuScene : Commander
    {
        [SerializeField] TextMeshProUGUI _bestScoreText;

        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        protected override void Start()
        {
            base.Start();

            ShowBestScore();
        }

        void ShowBestScore()
        {
            int bestScore = PlayerPrefs.GetInt("score");

            if (bestScore > 0)
            {
                _bestScoreText.text = "Best score: " + bestScore;
                _bestScoreText.gameObject.SetActive(true);
            }
        }
    }
}
