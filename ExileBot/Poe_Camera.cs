using BotFramework;
using System;
namespace ExileBot
{
	public class Poe_Camera : RemoteMemoryObject
	{
		public float Width
		{
			get
			{
				return (float)this.m.ReadInt(this.address + 4);
			}
		}
		public float Height
		{
			get
			{
				return (float)this.m.ReadInt(this.address + 8);
			}
		}
		public float ZFar
		{
			get
			{
				return this.m.ReadFloat(this.address + 392);
			}
			set
			{
				this.m.WriteFloat(this.address + 392, value);
			}
		}
		public Vec3 Position
		{
			get
			{
				return new Vec3(this.m.ReadFloat(this.address + 256), this.m.ReadFloat(this.address + 260), this.m.ReadFloat(this.address + 264));
			}
		}
        public unsafe Vec2 WorldToScreen(Vec3 vec3)
        {
            double num2;
            double num3;
            double num4;
            int addr = base.address + 0xbc;
            fixed (byte* numRef = base.m.ReadBytes(addr, 0x40))
            {
                float* numPtr = (float*)numRef;
                double num5 = (((numPtr[12] * vec3.x) + (numPtr[0x1c] * vec3.y)) + (numPtr[0x2c] * vec3.z)) + numPtr[60];
                num2 = ((double)((((numPtr[0] * vec3.x) + (numPtr[0x10] * vec3.y)) + (numPtr[0x20] * vec3.z)) + numPtr[0x30])) / num5;
                num3 = ((double)((((numPtr[4] * vec3.x) + (numPtr[20] * vec3.y)) + (numPtr[0x24] * vec3.z)) + numPtr[0x34])) / num5;
                num4 = ((double)((((numPtr[8] * vec3.x) + (numPtr[0x18] * vec3.y)) + (numPtr[40] * vec3.z)) + numPtr[0x38])) / num5;
            }
            if (((num4 < 0.0) || (Math.Abs(num2) > 1.0)) || (Math.Abs(num3) > 1.0))
            {
                return Vec2.Empty;
            }
            num2 = ((num2 + 1.0) * 0.5) * this.Width;
            num3 = ((1.0 - num3) * 0.5) * this.Height;
            return new Vec2((int)Math.Round(num2), (int)Math.Round(num3));
        }

	}
}
