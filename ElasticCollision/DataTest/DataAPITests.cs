using ElasticCollision.Data;

namespace DataTest
{
    public class DataAPITests
    {
        private DataAPI sub = DataAPI.CreateBallData();

        //[Fact]
        //public void TestAddingBalls()
        //{
        //    Assert.Empty(sub.GetState().Balls);
        //    sub.AddBalls(1, 5, 6);
        //    Assert.Equal(5, sub.GetState().Balls.First().Radius);
        //    Assert.Equal(6, sub.GetState().Balls.First().Mass);
        //    Assert.Single(sub.GetState().Balls);
        //    sub.AddBalls(10, 1, 1);
        //    Assert.Equal(11, sub.GetState().Balls.Count());
        //}
    }
}
