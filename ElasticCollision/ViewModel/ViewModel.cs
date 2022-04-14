using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ElasticCollision.Presentation
{
    public class ViewModel : ViewModelBase
    {
        public ICommand AddBallCommand { get; set; }

        public ViewModel(Model collisionModel = default) : base(collisionModel)
        {
            AddBallCommand = new RelayCommand(() => GenerateBall());
        }

        private void GenerateBall()
        {
            throw new NotImplementedException();
        }
    }
}
