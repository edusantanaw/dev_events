using Entity;

namespace Persistence
{
    public class DevEventDbContext
    {
        public List<DevEvent> Events { get; set; }

        public DevEventDbContext()
        {
            Events = new List<DevEvent>();
        }
    }
}