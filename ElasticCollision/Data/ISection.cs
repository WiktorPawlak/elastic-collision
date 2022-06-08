namespace ElasticCollision.Data
{
    // abstraction over areas and intervals,
    // provides methods to check whether ball fits within and intersects with.
    public interface ISection
    {
        bool FullyContains(Ball b);

        bool Intersects(Ball b);

        (ISection, ISection) SplitSection();
    }
}
