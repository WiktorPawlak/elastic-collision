using System;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {

        public abstract WorldState GetState();

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
            private readonly DataAPI _ballData;
            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _ballData = dataLayerAPI;
            }


            public override WorldState GetState()
            {
                throw new NotImplementedException();
            }

            public override void AddWatcher(WorldWatcher del)
            {
                throw new NotImplementedException();
            }

            public override void StartSimulation()
            {
                throw new NotImplementedException();
            }

            public override void StopSimulation()
            {
                throw new NotImplementedException();
            }

            public override void CreateBall(Ball newOne)
            {
                throw new NotImplementedException();
            }

            public override void addBalls(int count, double radius, double mass)
            {
                throw new NotImplementedException();
            }
        }
    }
}
