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
            private readonly DataAPI _ballData;

            public CollisionLogic(DataAPI dataLayerAPI)
            {
                _ballData = dataLayerAPI;
            }
        }
    }
}
