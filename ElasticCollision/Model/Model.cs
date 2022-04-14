using ElasticCollision.Logic;
using System;
using System.Collections.Specialized;

namespace ElasticCollision.Presentation
{
    public class Model
    {
        private readonly LogicAPI _collisionLogic = default;

        public Model(LogicAPI collisionLogic = null)
        {
            _collisionLogic = collisionLogic ?? LogicAPI.CreateCollisionLogic();
            _collisionLogic.CollectionChangedHandler += CollectionChangedHandler;
        }
        private void CollectionChangedHandler(object sender,
                          NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            //foreach (PhysicalBall changedObject in notifyCollectionChangedEventArgs.NewItems.Cast<PhysicalBall>())
            //{
            //    switch (notifyCollectionChangedEventArgs.Action)
            //    {
            //        case NotifyCollectionChangedAction.Add:
            //            this.CanvasMain.Children.Add(changedObject.WpfShape);
            //            break;
            //        case NotifyCollectionChangedAction.Remove:
            //            this.CanvasMain.Children.Remove(changedObject.WpfShape);
            //            break;
            //        case NotifyCollectionChangedAction.Replace:
            //            break;
            //        case NotifyCollectionChangedAction.Move:
            //            break;
            //        case NotifyCollectionChangedAction.Reset:
            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }
            //}
        }
    }
}
