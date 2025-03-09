using UniRx;
using Zenject;

namespace AsteroidProject
{
    public class ScoreViewModel
    {
        private ScoreCounter _scoreCounter;
        private ScoreView _scoreView;

        [Inject]
        private void Construct(ScoreCounter scoreCounter, ScoreView scoreView)
        {
            _scoreCounter = scoreCounter;
            _scoreView = scoreView;

            _scoreCounter.Score.Subscribe(RedrawScore);
        }

        private void RedrawScore(int score)
        {
            _scoreView.ChangeScoreText(score.ToString());
        }
    }
}
