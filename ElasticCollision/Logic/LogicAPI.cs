using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static ElasticCollision.Logic.Vector;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {

        public abstract WorldState GetCurrentState();

        // WorldWatcher dostaje nowy stan świata w każdej klatce
        public delegate void WorldWatcher(WorldState state);
        public abstract void AddWatcher(WorldWatcher del);

        public abstract void StartSimulation();
        public abstract void StopSimulation();
        // może jeszcze jakieś kontrolki do FPS świata,
        // bo ΔT będzie raczej zakodowana na sztywno
        public abstract void CreateBall(Ball newOne);
        public abstract void addBalls(int count, double radius, double mass);
        // we ball, i tak musielibyśmy korzystać z `Vector`
        // ewentualnie dać tutaj (x, y, ɸ)
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data ?? DataAPI.CreateBallData());
        }

        private class CollisionLogic : LogicAPI
        {
            private WorldState state;
            private bool running;
            private List<WorldWatcher> watchers;
            private Task updater;

            private readonly DataAPI _useless;
            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _useless = dataLayerAPI;
                running = false;
                watchers = new List<WorldWatcher>();
                state = new(new List<Ball>(), new Area(vec(0, 0), vec(1000, 1000)));
            }

            public override WorldState GetCurrentState() => state;

            public override void AddWatcher(WorldWatcher del)
            {
                watchers.Add(del);
            }

            public override void StartSimulation()
            {
                if (!running)
                {
                    running = true;
                    updater = Task.Run(updateLoop);
                }
            }

            public override async void StopSimulation()
            {
                if (running)
                {
                    running = false;
                    await updater;
                }
            }

            public void nextTick()
            {
                state = state.Proceed(0.01);
            }

            private void notifyObservers()
            {
                watchers.ForEach(x => x.Invoke(state));
            }

            public void updateLoop()
            {
                while (running)
                {
                    Thread.Sleep(10);
                    nextTick();
                    notifyObservers();
                }
            }

            public override void CreateBall(Ball newOne)
            {
                if (running)
                {
                    throw new Exception("nie teraz");
                }
                else
                {
                    state = state.AddBall(newOne);
                }
            }

            public override void addBalls(int count, double radius, double mass)
            {
                if (running)
                {
                    throw new Exception("nie teraz");
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        state = state.AddBall(radius, mass);
                    }
                }
            }

        }
    }
}
