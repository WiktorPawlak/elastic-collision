using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ElasticCollision.Presentation
{
    public class ViewModel : ViewModelBase
    {
        private Model _collisionModel { get; set; }
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
            _collisionModel = collisionModel ?? new Model();
            //Balls = new ObservableCollection<BallModel>();
            //Balls.CollectionChanged += CollectionChangedHandler;
            AddBallsCommand = new RelayCommand(() => RequestBall());
            Width = _collisionModel.Width;
            Height = _collisionModel.Height;
        }

        private void RequestBall()
        {
            Balls = new ObservableCollection<BallModel>(_collisionModel.GiveBalls(BallsCount));
            //Balls.Refresh();
        }

        //private void CollectionChangedHandler(object sender,
        //        NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        //{
        //    throw new ArgumentOutOfRangeException();
        //    //foreach (BallModel changedObject in notifyCollectionChangedEventArgs.NewItems.Cast<BallModel>())
        //    //{
        //    //    switch (notifyCollectionChangedEventArgs.Action)
        //    //    {
        //    //        case NotifyCollectionChangedAction.Add:
        //    //            RaisePropertyChanged(nameof(Balls));
        //    //            //this.CanvasMain.Children.Add(changedObject.WpfShape);
        //    //            break;
        //    //        case NotifyCollectionChangedAction.Remove:
        //    //            //this.CanvasMain.Children.Remove(changedObject.WpfShape);
        //    //            break;
        //    //        case NotifyCollectionChangedAction.Replace:
        //    //            break;
        //    //        case NotifyCollectionChangedAction.Move:
        //    //            break;
        //    //        case NotifyCollectionChangedAction.Reset:
        //    //            break;
        //    //        default:
        //    //            throw new ArgumentOutOfRangeException();
        //    //    }
        //    //}
        //}
    }
}
