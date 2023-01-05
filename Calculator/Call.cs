using System;

namespace Calculator
{
    public class Call
    {
        public Call(double phoneNumber, DateTime start, DateTime end)
        {
            PhoneNumber = phoneNumber;
            Start = start;
            End = end;
        }

        public double PhoneNumber { get; }

        public DateTime Start { get; }

        public DateTime End { get; }
    }
}

