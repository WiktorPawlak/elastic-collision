using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ElasticCollision.Presentation
{
    public class ViewModel : ViewModelBase
    {
        private Model CollisionModel { get; set; }
        public ObservableCollection<BallModel> Balls { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int BallsCount { get; set; } = 10;
        public ICommand AddBallsCommand { get; set; }

        public ViewModel() : this(default)
        {
        }
        public ViewModel(Model collisionModel = default)
        {
            CollisionModel = collisionModel ?? new Model();
            //Balls = new ObservableCollection<BallModel>();
            //Balls.CollectionChanged += CollectionChangedHandler;
            AddBallsCommand = new RelayCommand(() => RequestBall());
            Balls = new ObservableCollection<BallModel>();
            CollisionModel.AddFrameUpdater(Framer);
            Width = CollisionModel.Width;
            Height = CollisionModel.Height;
        }

        private void RequestBall()
        {
            foreach (var ball in CollisionModel.GiveBalls(BallsCount))
            {
                Balls.Add(ball);
            }
        }

        private void Framer(IEnumerable<BallModel> ballModels)
        {
            Balls = new ObservableCollection<BallModel>(ballModels);
            RaisePropertyChanged(nameof(Balls));
        }
    }
}
