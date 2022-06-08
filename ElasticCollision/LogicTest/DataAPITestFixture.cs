using ElasticCollision.Data;

namespace LogicTest
{
    public class DataAPITestFixture : DataAPI
    {
        public override UpdateBall CheckCollision { get; set; }

        public override Area Area { get; }

        public override void AddBalls(int count, double radius, double mass)
        {
            return;
        }
    }
}
