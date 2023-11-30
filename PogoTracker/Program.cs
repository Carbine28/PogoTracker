namespace PogoTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pogoRequest = new PoGoRequest();
            var pogoTrackerDb = new PogoTrackerDb();
            PogoTracker pogoTracker  = new(pogoRequest, pogoTrackerDb);
            pogoTracker.Run();
        }
    }
}
