using Microsoft.International.Converters.PinYinConverter;

namespace MyCompanyName.Identity
{
    public static class StringExtensions
    {
        /// <summary> 
        /// 汉字转化为拼音
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>全拼</returns> 
        public static string GetPinyin(this string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    var chineseChar = new ChineseChar(obj);
                    var t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1)?.ToUpper();
                    r += t[1..]?.ToLower();
                }
                catch { r += obj.ToString(); }
            }
            return r;
        }

        /// <summary> 
        /// 汉字转化为拼音首字母
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>首字母</returns> 
        public static string GetFirstPinyin(this string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    var chineseChar = new ChineseChar(obj);
                    var t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }
    }
}
