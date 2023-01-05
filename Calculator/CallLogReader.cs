using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Calculator
{
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
            var startStr = parts[1].Trim();
            var endStr = parts[2].Trim();
            if (double.TryParse(phoneNumberStr, out var phoneNumber) && TryParseDateTime(startStr, out var start) &&
                TryParseDateTime(endStr, out var end))
            {
                if (start <= end)
                {
                    return new Call(phoneNumber, start, end);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private bool TryParseDateTime(string str, out DateTime dateTime)
        {
            var ret = DateTime.TryParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var parsedDateTime);
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
}