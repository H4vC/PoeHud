using PoeHUD.Framework;
using PoeHUD.Models;

namespace PoeHUD.Poe
{
    public class Offsets
    {
        public static Offsets Regular = new Offsets { IgsOffset = 0, IgsDelta = 0, ExeName = "PathOfExile" };
        public static Offsets Steam = new Offsets { IgsOffset = 24, IgsDelta = 0, ExeName = "PathOfExileSteam" };
        /* offsets from some older steam version: 
		 	Base = 8841968;
			FileRoot = 8820476;
			MaphackFunc = 4939552;
			ZoomHackFunc = 2225383;
			AreaChangeCount = 8730996;
			Fullbright1 = 7639804;
			Fullbright2 = 8217084;
		*/


        /* maphack function
        FF D0                 - call eax
        8B 46 48              - mov eax,[esi+48]
        3B 46 4C              - cmp eax,[esi+4C]
        74 3A                 - je PathOfExile.exe+4D2FFC
        BA 04000000           - mov edx,00000004
        D9 00                 - fld dword ptr [eax]          //1 replace to  fld1  ( D9E8 )
        8B 0C 24              - mov ecx,[esp]
        D9 19                 - fstp dword ptr [ecx]
        8B 0C 24              - mov ecx,[esp]
        03 CA                 - add ecx,edx
        89 0C 24              - mov [esp],ecx
        D9 00                 - fld dword ptr [eax]          //2 (prev+0xC) replace to  fld1  ( D9E8 )
        D9 19                 - fstp dword ptr [ecx]
        8B 0C 24              - mov ecx,[esp]
        03 CA                 - add ecx,edx
        89 0C 24              - mov [esp],ecx
        D9 00                 - fld dword ptr [eax]          //3 (prev+0xC) replace to  fld1  ( D9E8 )
        D9 19                 - fstp dword ptr [ecx]
        8B 0C 24              - mov ecx,[esp]
        03 CA                 - add ecx,edx
        89 0C 24              - mov [esp],ecx
        D9 00                 - fld dword ptr [eax]         //4 (prev+0xC) replace to  fld1  ( D9E8 )
        */
        /* fullbright base (function begin here)
        55                    - push ebp
        8B EC                 - mov ebp,esp
        83 E4 F8              - and esp,-08
        6A FF                 - push -01
        68 8661EC00           - push PathOfExile.std::_Mutex::_Mutex+72D2C
        64 A1 00000000        - mov eax,fs:[00000000]
        50                    - push eax
        64 89 25 00000000     - mov fs:[00000000],esp
        81 EC A0000000        - sub esp,000000A0
        53                    - push ebx
        8B 5D 10              - mov ebx,[ebp+10]
        C7 44 24 44 00000000  - mov [esp+44],00000000
         * 
         * ......
       F3 0F59 44 24 20      - mulss xmm0,[esp+20]
       F3 0F59 25 E027FB00   - mulss xmm4,[PathOfExile.std::_Mutex::_Mutex+15F386]  -//fullbright1 <- const 1300
       83 EC 0C              - sub esp,0C
        ....
         * 
       F3 0F10 4C 24 54      - movss xmm1,[esp+54]
       F3 0F5C 0D D8680401   - subss xmm1,[PathOfExile.std::_Mutex::_Mutex+1F347E]  -//fullbright2 <- const 300
       F3 0F58 8C 24 AC000000  - addss xmm1,[esp+000000AC]
       F3 0F11 54 24 64      - movss [esp+64],xmm2
        
        */

        /* 
			64 A1 00 00 00 00          mov     eax, large fs:0
			6A FF                      push    0FFFFFFFFh
			68 90 51 4D 01             push    offset SEH_10D6970
			50                         push    eax
			64 89 25 00 00 00 00       mov     large fs:0, esp
			A1 EC 6A 70 01             mov     eax, off_1706AEC ; <--- BP IS HERE
			81 EC C8 00 00 00          sub     esp, 0C8h
			53                         push    ebx
			55                         push    ebp
			33 DB                      xor     ebx, ebx
			56                         push    esi
			57                         push    edi
			3B C3                      cmp     eax, ebx
		 */

        private static readonly Pattern basePtrPattern = new Pattern(new byte[]
        {
            100, 161, 0, 0, 0, 0, 106, 255, 104, 0, 0, 0, 0, 80, 100, 137,
            37, 0, 0, 0, 0, 161, 0, 0, 0, 0, 129, 236, 0xC8, 0, 0, 0,
            0x53, 0x55, 0x33, 0xDB, 0x56, 0x57, 0x3B, 0xC3
        }, "xxxxxxxxx????xxxxxxxxx????xxxxxxxxxxxxxx");

        private static readonly Pattern fileRootPattern = new Pattern(new byte[]
        {
            106, 255, 104, 0, 0, 0, 0, 100, 161, 0, 0, 0, 0, 80, 100, 137,
            37, 0, 0, 0, 0, 131, 236, 48, 255, 5, 0, 0, 0, 0, 83, 85,
            139, 45, 0, 0, 0, 0, 86, 184
        }, "xxx????xxxxxxxxxxxxxxxxxxx????xxxx????xx");

        private static readonly Pattern areaChangePattern = new Pattern(new byte[]
        {
            139, 9, 137, 8, 133, 201, 116, 12, 255, 65, 40, 139, 21, 0, 0, 0,
            0, 137, 81, 36, 195, 204
        }, "xxxxxxxxxxxxx????xxxxx");


        /*
			80 7E 48 00             cmp     byte ptr [esi+48h], 0
			0F 85 A4 01 00 00       jnz     loc_542F41					; we catch the last 00 byte into pattern to match 4-bytes step
			8B 46 08                mov     eax, [esi+8]
			80 B8 1C 01 00 00 00    cmp     byte ptr [eax+11Ch], 0
			75 12                   jnz     short loc_542DBB
		 */

        //private static readonly Pattern configPattern = new Pattern(@"\x53\x55\x33\xDB\x56\x57\x3B\xC3\x0F\x85\x00\x00\x00\x00\x68\x00\x00\x00\x00", "xxxxxxxxxx????x????");
        ///*
        //      53                       push    ebx
        //      55                       push    ebp
        //      33 DB                    xor     ebx, ebx
        //      56                       push    esi
        //      57                       push    edi
        //      3B C3                    cmp     eax, ebx
        //      0F 85 2A 02 00 00        jnz     loc_6358E8
        //      68 98 A1 C7 00           push    offset aProduction_con ; "production_Config.ini"
        //*/

        public int AreaChangeCount { get; private set; }
        public int Base { get; private set; }
        public string ExeName { get; private set; }
        public int FileRoot { get; private set; }
        public int IgsDelta { get; private set; }
        public int IgsOffset { get; private set; }
        //public string PoeConfigIni { get; private set; }

        public int IgsOffsetDelta
        {
            get { return IgsOffset - IgsDelta; }
        }


        public void DoPatternScans(Memory m)
        {
            int[] array = m.FindPatterns(basePtrPattern, fileRootPattern, areaChangePattern);//, configPattern);
            Base = m.ReadInt(m.AddressOfProcess + array[0] + 22) - m.AddressOfProcess;
            FileRoot = m.ReadInt(m.AddressOfProcess + array[1] + 40) - m.AddressOfProcess;
            AreaChangeCount = m.ReadInt(m.AddressOfProcess + array[2] + 13) - m.AddressOfProcess;
            //PoeConfigIni = m.ReadString(m.ReadInt(m.AddressOfProcess + array[3] + 0xF));
        }
    }
}