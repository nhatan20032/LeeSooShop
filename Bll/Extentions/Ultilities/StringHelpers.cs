using System.Text.RegularExpressions;
using System.Text;

namespace Bll.Extentions.Ultilities
{
    public class StringHelpers
    {
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        /// <summary>
        /// Hàm xóa chữ cái có dấu trong tiếng Việt
        /// </summary>
        /// <param name="str">Nhập chữ có dấu ở đây</param>
        /// <returns></returns>
        public static string RemoveSign4VietnameseString(string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
        /// <summary>
        /// Lọc dấu cho tiếng Việt
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A string.</returns>
        public static string LocDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace("\"", "\'");
        }
        /// <summary>
        /// Hàm xóa bỏ dấu tiếng việt<br />
        /// Remove Vietnamese Unicode
        /// </summary>
        /// <param name="title">Nội dung cần xoá</param>
        /// <param name="isSlug">Có chuyển đổi thành đường dẫn không</param>
        /// <returns>isSlug=false: default unicode<br />isSlug=true: default-unicode</returns>
        public static string RemoveVietnameseChar(string title = "Default Unicode", bool isSlug = false)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = title.Normalize(NormalizationForm.FormD);
            if (isSlug)
            {
                temp = RemoveSign4VietnameseString(temp);
                return regex.Replace(temp, string.Empty).Replace(":", "").Replace("(", "").Replace(")", "").Replace("\"", "\'").Replace(",", "").Replace(",", "").Replace(";", "").Replace("|", "").Replace(" ", "-").Replace("--", "-").ToLower();
            }
            return regex.Replace(temp, string.Empty);
        }
        /// <summary>
        /// Removes the special chars.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CleanFilename(string input)
        {
            input = RemoveSign4VietnameseString(input);
            return Regex.Replace(input, @"[^a-zA-Z0-9_\s.]", string.Empty);
        }
    }
}
