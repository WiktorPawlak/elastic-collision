using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ElasticCollision.Data;
using static ElasticCollision.Logic.Vector;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        public abstract WorldState GetCurrentState();
        public Observable<WorldState> Observable = new();
        // WorldObserver dostaje nowy stan świata w każdej klatce

        public abstract void StartSimulation();
        public abstract void NextTick(); // advance simulation by one tick
        public abstract void StopSimulation();
        // może jeszcze jakieś kontrolki do FPS świata,
        // bo ΔT będzie raczej zakodowana na sztywno
        public abstract void AddBalls(int count, double radius, double mass);
        // we ball, i tak musielibyśmy korzystać z `Vector`
        // ewentualnie dać tutaj (x, y, ɸ)
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data ?? DataAPI.CreateBallData());
        }

        private class CollisionLogic : LogicAPI
        {
            private WorldState _state;
            private readonly Vector _orientationPoint;
            private readonly Vector _worldDimensions;
            private readonly DataAPI _dataLayer;
            private readonly Ticker _ticker;

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _dataLayer = dataLayerAPI;
                _orientationPoint = vec(0, 0);
                _worldDimensions = vec(500, 500);
                _state = new(new List<Ball>(), new Area(_orientationPoint, _worldDimensions));
                _ticker = new(NextTick, 5);
            }

            public override WorldState GetCurrentState() => _state;

            public override void StartSimulation() => _ticker.Start();

            public override async void StopSimulation() => _ticker.Stop();

            public override void NextTick()
            {
                _state = _state.Proceed(0.05);
                Task.Run(() => Observable.Notify(_state));
            }

            public override void AddBalls(int count, double radius, double mass)
            {
                if (_ticker.running)
                {
                    _ticker.Stop();
                    _state = _state.AddBalls(count, radius, mass);
                    _ticker.Start();
                }
                else
                {
                    _state = _state.AddBalls(count, radius, mass);
                }
                Observable.Notify(_state);
            }
        }
    }
}
