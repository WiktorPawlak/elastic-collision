using ElasticCollision.Data;
using System;

namespace ElasticCollision.Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateCollisionLogic(DataAPI data = default)
        {
            return new CollisionLogic(data == null ? DataAPI.CreateBallData() : data);
        }

        private class CollisionLogic : LogicAPI
        {
            public CollisionLogic(DataAPI dataLayerAPI)
            {
                BallData = dataLayerAPI;
            }

            private readonly DataAPI BallData;
        }
    }
}
