using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using RouteTeamStudio.Gameplay.Players;
using UnityEngine.SceneManagement;

namespace RouteTeamStudio.Commanders
{
    public class GameScene : Commander
    {
        protected override void Awake()
        {
            base.Awake();

            Being.OnBeingDie += OnBeingDie;
        }

        private void OnDestroy()
        {
            Being.OnBeingDie -= OnBeingDie;
        }

        private void OnBeingDie(Being being)
        {
            Player playerDead = being.GetComponent<Player>();

            if (!playerDead)
            {
                return;
            }

            SceneManager.LoadScene("MenuScene");
        }
    }
}
