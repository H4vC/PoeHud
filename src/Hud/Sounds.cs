using System;
using System.Collections.Generic;
using System.Media;
using PoeHUD.Framework;

namespace PoeHUD.Hud
{
    public static class Sounds
    {
        public static SoundPlayer AlertSound;
        public static SoundPlayer DangerSound;
        public static SoundPlayer TreasureSound;
        public static SoundPlayer AtentionSound;
        private static readonly Dictionary<string, SoundPlayer> soundLib = new Dictionary<string, SoundPlayer>();

        public static void AddSound(string name)
        {
            if (!soundLib.ContainsKey(name))
            {
                try
                {
                    var soundPlayer = new SoundPlayer($"sounds/{name}");
                    soundPlayer.Load();
                    soundLib[name] = soundPlayer;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error when loading {name} | {ex.Message}:", ex);
                }
            }
        }
        public static void Play(this SoundPlayer player, int volume)
        {
            const ushort MAX_VOLUME = 100;
            var newVolume = (ushort)((float)volume / MAX_VOLUME * ushort.MaxValue);
            var stereo = (newVolume | (uint)newVolume << 16);
            WinApi.waveOutSetVolume(IntPtr.Zero, stereo);
            player.Play();
        }
        public static SoundPlayer GetSound(string name)
        {
            return soundLib[name];
        }

        public static void LoadSounds()
        {
            AddSound("alert.wav");
            AddSound("danger.wav");
            AddSound("treasure.wav");
            AddSound("atention.wav");
            AlertSound = GetSound("alert.wav");
            DangerSound = GetSound("danger.wav");
            TreasureSound = GetSound("treasure.wav");
            AtentionSound = GetSound("atention.wav");
        }
    }
}