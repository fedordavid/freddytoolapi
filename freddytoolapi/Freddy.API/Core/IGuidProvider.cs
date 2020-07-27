using System;

namespace Freddy.API.Core
{
    public interface IGuidProvider
    {
        Guid NewGuid();
    }

    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}