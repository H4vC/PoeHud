using System;
using System.Linq;
using PoeHUD.Models.Enums;
using PoeHUD.Poe.UI;

namespace PoeHUD.Poe.RemoteMemoryObjects
{
    public class IngameState : RemoteMemoryObject
    {
        public Camera Camera => base.GetObject<Camera>(Address + 0x15F0 + Offsets.IgsOffsetDelta);

        public IngameData Data => base.ReadObject<IngameData>(Address + 0x13C + Offsets.IgsOffset);

        public bool InGame => M.ReadInt(Address + 0x13C + Offsets.IgsOffset) != 0 && ServerData.IsInGame;

        public ServerData ServerData => base.ReadObjectAt<ServerData>(0x140 + Offsets.IgsOffset);

        public IngameUIElements IngameUi => base.ReadObjectAt<IngameUIElements>(0x5EC + Offsets.IgsOffset);

        public Element UIRoot => base.ReadObjectAt<Element>(0xC10 + Offsets.IgsOffset);

        public Element UIHover => base.ReadObjectAt<Element>(0xC24 + Offsets.IgsOffset);

        public int EntityLabelMap => M.ReadInt(Address + 68, 2528);


        public DiagnosticInfoType DiagnosticInfoType => (DiagnosticInfoType)M.ReadInt(Address + 0xC90);

        public DiagnosticElement LatencyRectangle => GetObjectAt<DiagnosticElement>(0xEB0);

        public DiagnosticElement FrameTimeRectangle => GetObjectAt<DiagnosticElement>(0x1310);

        public DiagnosticElement FPSRectangle => GetObjectAt<DiagnosticElement>(0x1540);

        /// <summary>
        /// Latency in ms
        /// </summary>
        public float CurLatency => M.ReadFloat(Address + 0xCA0);

        /// <summary>
        /// Frame time in ms
        /// </summary>
        public float CurFrameTime => M.ReadFloat(Address + 0x1100);


        public float CurFps => M.ReadFloat(Address + 0x1370);

        /// <summary>
        /// How much time client is running
        /// </summary>
        public TimeSpan TimeInGame => TimeSpan.FromMilliseconds(M.ReadInt(Address + 0xc80));
    }
}