using System.Text.RegularExpressions;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }

        public static string GetSlug(string str)
        {
            /** Troca os caracteres acentuados por n�o acentuados **/
            string[] acentos = new string[] { "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "�", "�", "�", "�", "�", "�", "�", "�", "\"", "'", ".", ",", "-", ":", "(", ")", "�", "|", "\\\\", "�", "_", "@", "#", "!", "$", "%", "&", "*", ";", "/", "<", ">", "?", "[", "]", "{", "}", "=", "+", "�", "�", "`", "^", "~" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os caracteres especiais da string por " " **/
            //str = Regex.Replace(str, @"[^\w\.@-]", " ",
            //                    RegexOptions.None, TimeSpan.FromSeconds(1.5));

            return str.Trim();
        }
    }
}