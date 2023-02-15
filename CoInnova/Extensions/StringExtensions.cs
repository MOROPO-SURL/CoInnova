namespace MOROPO.UIC.CoInnova.Extensions
{
    public static class StringExtensions
    {
        public static string AsContentTypeToMDICssClass(this string target)
        {
            switch (target)
            {
                case string x when x.Contains("pdf"):
                    return "mdi-file-pdf";

                case string x when x.Contains("word"):
                    return "mdi-file-word";

                case string x when x.Contains("excel") || x.Contains("sheet"):
                    return "mdi-file-excel";

                case string x when x.Contains("image"):
                    return "mdi-file-image";

                default:
                    return "mdi-file";
            }
        }

        public static string AsContentTypeToExtension(this string target)
        {
            switch (target)
            {
                case "application/pdf":
                    return "pdf";

                case "application/msword":
                    return "doc";

                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return "docx";

                case "application/vnd.ms-excel":
                    return "xls";

                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return "xlsx";

                case string x when x.StartsWith("image/"):
                default:
                    return target.Split('/')[1].ToLower();
            }
        }
    }
}
