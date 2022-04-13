using System;
using System.ComponentModel;

namespace ElasticCollision.Presentation
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel(Model collisionModel = default)
        {
            CollisionModel = collisionModel ?? new Model();
        }

        private Model CollisionModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
