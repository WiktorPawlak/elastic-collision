using System;
using System.Windows.Input;

namespace ElasticCollision.Presentation
{
    public class ViewModel : ViewModelBase
    {
        private Model _collisionModel { get; set; }
        public int RadiusBox { get; set; } = 10;
        public object[] Balls;
        public ICommand AddBallCommand { get; set; }

        public ViewModel() => _collisionModel = default;
        public ViewModel(Model collisionModel = default)
        {
            _collisionModel = collisionModel ?? new Model();
            AddBallCommand = new RelayCommand(() => RequestBall());
        }

        private void RequestBall()
        {
            //RaisePropertyChanged(nameof(RadiusBox));
            _collisionModel.AddBall(RadiusBox);
        }
    }
}
