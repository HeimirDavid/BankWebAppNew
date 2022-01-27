namespace BankWebApp.Data
{
    public static class Countries
    {
        public static Dictionary<string, string> _Countries = new Dictionary<string, string>
        {
            {  "Choose an option...", "Error" },
            { "Sweden", "SE" },
            {  "Denmark", "DK" },
            {  "Norway", "NO" },
            {  "Finland", "FI" },
        };

        public static Dictionary<string, string> CountryFlags = new Dictionary<string, string>
        {
            { "Sweden", "/images/flags/se.svg" },
            { "Norway", "/images/flags/no.svg" },
            { "Finland", "/images/flags/fi.svg" },
            { "Denmark", "/images/flags/dk.svg" },
        };

    }
}
