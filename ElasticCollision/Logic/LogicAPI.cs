using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ElasticCollision.Data;
using static ElasticCollision.Data.Vector;

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
            private readonly DataAPI _dataLayer;
            private readonly Ticker _ticker;

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _dataLayer = dataLayerAPI;
                _ticker = new(NextTick, 16);
            }

            public override WorldState GetCurrentState() => _dataLayer.GetState();

            public override void StartSimulation() => _ticker.Start();

            public override void StopSimulation() => _ticker.Stop();

            public override void NextTick()
            {
                _dataLayer.MoveBalls(0.05);
                Task.Run(() => Observable.Notify(GetCurrentState()));
            }

            public override void AddBalls(int count, double radius, double mass)
            {
                if (_ticker.running)
                {
                    _ticker.Stop();
                    _dataLayer.AddBalls(count, radius, mass);
                    _ticker.Start();
                }
                else
                {
                    _dataLayer.AddBalls(count, radius, mass);
                }
                Observable.Notify(_state);
            }
        }
    }
}
