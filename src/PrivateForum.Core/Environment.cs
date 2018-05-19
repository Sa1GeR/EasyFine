
namespace PrivateForum.Core
{
    public static class EdEnvironment
    {
        public static bool IsDev { get; private set; } = Config.Get<string>("Environment") == "DEV";
    }
}
