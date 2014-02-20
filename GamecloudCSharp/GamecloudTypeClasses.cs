using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gamecloud
{
    /// <summary>
    /// The basic class for Items in Gamecloud results
    /// </summary>
    public class ItemGamecloud
    {
        public int gainCount { get; set; }
        public int loseCount { get; set; }
        public int total { get; set; }
    }

    /// <summary>
    /// The basic class for Achivements in Gamecloud results
    /// </summary>
    public class AchievementGamecloud
    {
        public int count { get; set; }
        public List<DateTime> eventTimes { get; set; }
    }

    /// <summary>
    /// The basic class for Events in Gamecloud results
    /// </summary>
    public class EventGamecloud
    {
        public int count { get; set; }
        public List<DateTime> eventTimes { get; set; }
    }
}
