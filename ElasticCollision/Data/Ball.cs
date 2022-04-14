namespace ElasticCollision.Data
{

    public record Ball(
         double radius,
         double mass,
         Vector location,
         Vector velocity
    );
}
