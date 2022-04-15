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
        public abstract void addBallse(int count);
        // we ball, i tak musielibyśmy korzystać z `Vector`
        // ewentualnie dać tutaj (x, y, ɸ)
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data ?? DataAPI.CreateBallData());
        }

        private class CollisionLogic(DataAPI dataLayerAPI)
        {
        private Random _randomNumPool = new Random();
        private readonly DataAPI _ballData;
        public CollisionLogic(DataAPI dataLayerAPI)
        {
            _ballData = dataLayerAPI;
        }

        private Vector GetRandomLocation(int width, int height)
        {
            Vector loc = new Vector(
                _randomNumPool.Next(0, width),
                _randomNumPool.Next(0, height)
                );
            return loc;
        }
    }
}
