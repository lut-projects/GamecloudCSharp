using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamecloud
{
    
    public class EventClass
    {
        public string playerId { get; set; }
        public string characterId { get; set; }
        public string hash { get; set; }
        public string callType { get; set; }
        public string authkey { get; set; }

        public EventClass(string authkey, string hash, string playerId, string characterId)
        {
            this.callType = "gameDataSave";
            this.authkey = authkey;
            this.hash = hash;
            this.playerId = playerId;
            this.characterId = characterId;
        }
    }
}
