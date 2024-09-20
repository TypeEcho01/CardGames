using System.Text;

namespace CardGames
{
    public class Printer(string sep = " ", string end = "\n", bool resetConfigsAfterPrint = true)
    {
        private static readonly string nullRepresentation = "null";

        private string _sep = sep;
        public string DefaultSep = sep;

        private string _end = end;
        public string DefaultEnd = end;

        public bool ResetConfigsAfterPrint = resetConfigsAfterPrint;

        private void ResetConfigs()
        {
            _sep = DefaultSep;
            _end = DefaultEnd;
        }

        public void Print(params object?[] values)
        { 
            StringBuilder stringBuilder = new();

            // Replaces null with the nullRepresentation if null is the only value passed
            if (values == null)
                values = [nullRepresentation];

            foreach (var value in values)
            {
                if (value is not null)
                    stringBuilder.Append(value);
                else
                    // null will Append "", so Append the nullRepresentation instead
                    stringBuilder.Append(nullRepresentation);

                stringBuilder.Append(_sep);
            }

            if (values.Length > 0)
                stringBuilder.Length -= _sep.Length; // Removes the last separator

            stringBuilder.Append(_end);

            string output = stringBuilder.ToString();
            Console.Write(output);

            if (ResetConfigsAfterPrint)
            {
                ResetConfigs();
            }
        }

        public void Configure(string? sep = null, string? end = null)
        {
            if (sep is not null)
                _sep = sep;

            if (end is not null)
                _end = end;
        }
    }
}