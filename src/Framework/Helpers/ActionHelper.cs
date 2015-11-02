using System;

namespace PoeHUD.Framework.Helpers
{
    public static class ActionHelper
    {
        public static void SafeInvoke<T>(this Action<T> action, T parameter)
        {
            action?.Invoke(parameter);
        }

        public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            action?.Invoke(parameter1, parameter2, parameter3);
        }

        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        public static void ThrowIf<TException>(bool condition)
            where TException : Exception, new()
        {
            if (condition)
            {
                throw new TException();
            }
        }

        public static bool TryInvoke(this Action action)
        {
            try
            {
                action();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}