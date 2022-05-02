namespace System
{
    /// <summary>
    /// Extension methods for the <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static double ToTimestamp(this DateTime target, bool milliseconds = false)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            var diff = target - origin;
            return milliseconds ? Math.Floor(diff.TotalMilliseconds) : Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromTimestamp(this double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            if (unixTime > 9999999999) { return epoch.AddMilliseconds(unixTime); }
            else { return epoch.AddSeconds(unixTime); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="timestamp"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static bool TryToTimestamp(this DateTime target, out double timestamp, bool milliseconds = false)
        {
            try { timestamp = target.ToTimestamp(milliseconds); return true; }
            catch { timestamp = default; return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unixTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool TryFromTimestamp(this double unixTime, out DateTime dateTime)
        {
            try { dateTime = unixTime.FromTimestamp(); return true; }
            catch { dateTime = default; return false; }
        }
    }
}
