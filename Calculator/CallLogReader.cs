using System.Globalization;

namespace MyApp;
using System.Linq;

public class CallLogReader
{
    public List<Call> ReadCalls(string fileName)
    {
        var lines = ReadLines(fileName);
        var ret = ParseLines(lines);
        return ret;

    }
    
    private List<string> ReadLines(string fileName)
    {
        try
        {
            var lines = File.ReadLines(fileName).ToList();
            return lines;
        }
        catch (Exception ex)
        {
            throw new Exception($"Reading file {fileName} failed.", ex);
        }
    }

    private List<Call> ParseLines(List<string> lines)
    {
        var ret = lines.Select(x => ParseLine(x)).Where(x => x != null).ToList();
        return ret;
    }

    private Call ParseLine(string line)
    {
        var call = TryParseLine(line);
        if (call == null)
        {
            Console.WriteLine($"Invalid line {line}. Ignored.");
        }
        return call;
    }

    private Call TryParseLine(string line)
    {
        var parts = line.Split(",");
        if (parts.Length != 3)
        {
            return null;
        }

        var phoneNumberStr = parts[0].Trim();
        var fromStr = parts[1].Trim();
        var toStr = parts[2].Trim();
        if (double.TryParse(phoneNumberStr, out var phoneNumber) && TryParseDateTime(fromStr, out var from) && TryParseDateTime(toStr, out var to))
        {
            return new Call(phoneNumber, from, to);
        }
        else
        {
            return null;
        }
    }

    private bool TryParseDateTime(string str, out DateTime dateTime)
    {
        var ret = DateTime.TryParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateTime);
        if (ret)
        {
            dateTime = parsedDateTime;
            return true;
        }
        else
        {
            dateTime = DateTime.MinValue;
            return false;
        }
    }
}