using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        private ObservableCollection<Ball> _balls;
        public abstract void CreateBall(double radius, double mass, Vector loc, Vector velocity);
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data == null ? DataAPI.CreateBallData() : data);
        }

        private class CollisionLogic : LogicAPI
        {
            private readonly DataAPI _ballData;

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _ballData = dataLayerAPI;
                _balls = new ObservableCollection<Ball>();
                _balls.CollectionChanged += CollectionChangedHandler;
            }

            public override void CreateBall(double radius, double mass, Vector loc, Vector velocity)
            {
                Ball ball = new(radius, mass, loc, velocity);
                //if (this._collisionSolver.IsPositionOccupied(ball))
                //{
                //    throw new ArgumentException("Position is already ocupied.");
                //}
                _balls.Add(ball);
            }

            private void CollectionChangedHandler(object sender,
                  NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
            {
                foreach (Ball changedObject in notifyCollectionChangedEventArgs.NewItems.Cast<Ball>())
                {
                    switch (notifyCollectionChangedEventArgs.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this.CanvasMain.Children.Add(changedObject.WpfShape);
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            //this.CanvasMain.Children.Remove(changedObject.WpfShape);
                            break;
                        case NotifyCollectionChangedAction.Replace:
                            break;
                        case NotifyCollectionChangedAction.Move:
                            break;
                        case NotifyCollectionChangedAction.Reset:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}
