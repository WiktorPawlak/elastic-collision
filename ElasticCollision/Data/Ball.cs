namespace ElasticCollision.Data
{

    public record Ball
    {

        public double radius { get; }
        public double mass { get; }
        public Vector location { get; }
        public Vector velocity { get; }
    }
}
