using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static ElasticCollision.Logic.Vector;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        public abstract WorldState GetCurrentState();

        // WorldObserver dostaje nowy stan świata w każdej klatce
        public delegate void WorldObserver(WorldState state);
        public abstract void AddObserver(WorldObserver del);

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
            private bool _running;
            private readonly List<WorldObserver> _watchers;
            private Task _updater;
            private readonly Vector _orientationPoint;
            private readonly Vector _worldDimensions;
            private readonly DataAPI _dataLayer;

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _dataLayer = dataLayerAPI;
                _running = false;
                _watchers = new List<WorldObserver>();
                _orientationPoint = vec(0, 0);
                _worldDimensions = vec(500, 500);
                _state = new(new List<Ball>(), new Area(_orientationPoint, _worldDimensions));
            }

            public override WorldState GetCurrentState() => _state;

            public override void AddObserver(WorldObserver del)
            {
                _watchers.Add(del);
            }

            public override void StartSimulation()
            {
                if (!_running)
                {
                    _running = true;
                    _updater = Task.Run(UpdateLoop);
                }
            }

            public override async void StopSimulation()
            {
                if (_running)
                {
                    _running = false;
                    await _updater;
                }
            }

            public override void NextTick()
            {
                _state = _state.Proceed(0.05);
                Task.Run(NotifyObservers);
            }

            private void NotifyObservers()
            {
                _watchers.ForEach(x => x.Invoke(_state));
            }

            public void UpdateLoop()
            {
                while (_running)
                {
                    Thread.Sleep(40);
                    NextTick();
                }
            }

            public override void AddBalls(int count, double radius, double mass)
            {
                if (_running)
                {
                    throw new Exception("Simulation is still running!");
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        _state = _state.AddBall(radius, mass);
                    }
                    NotifyObservers();
                }
            }
        }
    }
}
