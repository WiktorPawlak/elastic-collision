using System;
using System.Windows.Input;

namespace ElasticCollision.Presentation
{
    public class ViewModel : ViewModelBase
    {
        private Model _collisionModel { get; set; }
        public int RadiusBox { get; set; } = 10;
        public ICommand AddBallCommand { get; set; }

        public ViewModel() => _collisionModel = default;
        public ViewModel(Model collisionModel = default)
        {
            _collisionModel = collisionModel ?? new Model();
            AddBallCommand = new RelayCommand(() => GenerateBall());
        }

        private void GenerateBall()
        {
            RaisePropertyChanged(nameof(RadiusBox));
            throw new NotImplementedException();
        }
    }
}
