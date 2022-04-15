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
    }
}
