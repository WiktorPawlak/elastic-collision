using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ElasticCollision.Presentation
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private Model _collisionModel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase(Model collisionModel = default)
        {
            _collisionModel = collisionModel ?? new Model();
        }
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
