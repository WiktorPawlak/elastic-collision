using System;

namespace ElasticCollision.Data
{
    public abstract class DataAPI
    {
        public static DataAPI CreateBallData()
        {
            return new BallData();
        }

        private class BallData : DataAPI
        {
        }
    }
}
