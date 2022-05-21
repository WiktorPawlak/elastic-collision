namespace ElasticCollision.Data
{

    public record Interval(double low, double high)
    {
        public double midpoint { get { return (low + high) / 2; } }
        public double length { get { return (high - low); } }

        public Interval Shrink(double r) => new Interval(low + r, high - r);

        public bool Contains(double loc) => low <= loc && loc <= high;

        public (Interval, Interval) Split()
        {
            return (this with { high = midpoint },
               this with { low = midpoint });
        }
    }
    public abstract record DirectionalInterval(double low, double high) : Interval(low, high), Section
    {
        public bool FullyContains(Ball b) => Shrink(b.Radius).Contains(Selector(b.Location));

        public bool Intersects(Ball b) => Shrink(-b.Radius).Contains(Selector(b.Location));

        protected abstract double Selector(Vector v);
        public (Section, Section) SplitSection() => (this with { high = midpoint }, this with { low = midpoint });
    }

    public record HorizontalInterval(double low, double high) : DirectionalInterval(low, high)
    {
        protected override double Selector(Vector v) => v.X;

    }
    public record VerticalInterval(double low, double high) : DirectionalInterval(low, high)
    {
        protected override double Selector(Vector v) => v.Y;
    }
}
