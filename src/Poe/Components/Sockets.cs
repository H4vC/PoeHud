using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoeHUD.Poe.Components
{
    public class Sockets : Component
    {
        public int LargestLinkSize
        {
            get
            {
                if (Address == 0)
                {
                    return 0;
                }
                int num = M.ReadInt(Address + 60);
                int num2 = M.ReadInt(Address + 64);
                int num3 = num2 - num;
                if (num3 <= 0 || num3 > 6)
                {
                    return 0;
                }
                int num4 = 0;
                for (int i = 0; i < num3; i++)
                {
                    int num5 = M.ReadByte(num + i);
                    if (num5 > num4)
                    {
                        num4 = num5;
                    }
                }
                return num4;
            }
        }

        public List<int[]> Links
        {
            get
            {
                var list = new List<int[]>();
                if (Address == 0)
                {
                    return list;
                }
                int num = M.ReadInt(Address + 60);
                int num2 = M.ReadInt(Address + 64);
                int num3 = num2 - num;
                if (num3 <= 0 || num3 > 6)
                {
                    return list;
                }
                int num4 = 0;
                List<int> socketList = SocketList;
                for (int i = 0; i < num3; i++)
                {
                    int num5 = M.ReadByte(num + i);
                    var array = new int[num5];
                    for (int j = 0; j < num5; j++)
                    {
                        array[j] = socketList[j + num4];
                    }
                    list.Add(array);
                    num4 += num5;
                }
                return list;
            }
        }

        public List<int> SocketList
        {
            get
            {
                var list = new List<int>();
                if (Address == 0)
                {
                    return list;
                }
                int num = Address + 12;
                for (int i = 0; i < 6; i++)
                {
                    int num2 = M.ReadInt(num);
                    if (num2 >= 1 && num2 <= 4)
                    {
                        list.Add(M.ReadInt(num));
                    }
                    num += 4;
                }
                return list;
            }
        }

        public int NumberOfSockets => SocketList.Count;

        public bool IsRGB
        {
            get
            {
                if (Address == 0)
                {
                    return false;
                }
                foreach (var current in Links)
                {
                    if (current.Length >= 3 && current.Contains(1) && current.Contains(2) && current.Contains(3))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public List<string> SocketGroup
        {
            get
            {
                var list = new List<string>();
                foreach (var current in Links)
                {
                    var sb = new StringBuilder();
                    foreach (var color in current)
                    {
                        switch (color)
                        {
                            case 1:
                                sb.Append("R"); break;
                            case 2:
                                sb.Append("G"); break;
                            case 3:
                                sb.Append("B"); break;
                            case 4:
                                sb.Append("W"); break;
                        }
                    }
                    list.Add(sb.ToString());
                }
                return list;
            }
        }
    }
}