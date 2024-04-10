using System.Collections.Concurrent;
using Websocket.Model;

namespace Websocket.Cache
{
    public class CacheVote
    {
        private static  ConnectHost voted;

        public bool IsVote() { 
            if (voted == null)
            {
                return false;
            }
            return true;
        }
        public ConnectHost GetVote()
        {
            return voted;
        }
        public void SetVote( ConnectHost vote)
        {
            voted = null;
            voted = vote;
        }

    }
}
