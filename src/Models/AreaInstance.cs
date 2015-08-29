using System;
using PoeHUD.Poe.RemoteMemoryObjects;

namespace PoeHUD.Models
{
    public sealed class AreaInstance
    {
        public int RealLevel { get; private set; }
        public int NominalLevel { get; private set; }
        public string Name { get; private set; }
        public int Act { get; private set; }
        public bool IsTown { get; private set; }
        public bool HasWaypoint { get; private set; }
        public int Hash { get; private set; }
        public DateTime TimeEntered = DateTime.Now;
        public AreaInstance(AreaTemplate area, int hash, int realLevel)
        {
            Hash = hash;
            RealLevel = realLevel;
            NominalLevel = area.NominalLevel;
            Name = area.Name;
            Act = area.Act;
            IsTown = area.IsTown;
            HasWaypoint = area.HasWaypoint;
        }

        public override string ToString()
        {
            return $"{Name} ({RealLevel}) #{Hash}";
        }
        public string DisplayName => String.Concat(Name, " (", RealLevel, ")");
        public static string GetTimeString(TimeSpan timeSpent)
        {
            int allsec = (int)timeSpent.TotalSeconds;
            int secs = allsec % 60;
            int mins = allsec / 60;
            int hours = mins / 60;
            mins = mins % 60;
            return String.Format(hours > 0 ? "{0}:{1:00}:{2:00}" : "{1}:{2:00}", hours, mins, secs);
        }
    }
}