using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCTemp
{
    public static class Day4
    {
        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(4);

            var shifts = BuildShifts(input);

            var guards = shifts.Select(s => s.Guard).Distinct();
            int guardId = 0;
            int maxAsleep = 0;
            foreach(var guard in guards)
            {
                var asleep = shifts.Where(s => s.Guard == guard)
                    .Select(s => s.MinutesSlept())
                    .Sum();
                if (asleep > maxAsleep)
                {
                    guardId = guard;
                    maxAsleep = asleep;
                }
            }
            int maxMinute = 0;
            maxAsleep = 0;
            for (int min = 0; min < 60; min++)
            {
                var asleep = shifts.Where(s => s.Guard == guardId).Select(s => s.WasAsleep(min) ? 1 : 0).Sum();
                if (asleep > maxAsleep)
                {
                    maxAsleep = asleep;
                    maxMinute = min;
                }
            }
            Console.WriteLine($"The answer to 4-1 is {guardId}x{maxMinute}={guardId * maxMinute}");
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(4);

            var shifts = BuildShifts(input);

            var guards = shifts.Select(s => s.Guard).Distinct();
            int guardId = 0;
            int maxMinute = 0;
            int maxAsleep = 0;
            foreach (var guard in guards)
            {
               for (int min = 0; min < 60; min++)
                {
                    var asleep = shifts.Where(s => s.Guard == guard).Select(s => s.WasAsleep(min) ? 1 : 0).Sum();
                    if (asleep > maxAsleep)
                    {
                        guardId = guard;
                        maxAsleep = asleep;
                        maxMinute = min;
                    }
                }
            }
            
            Console.WriteLine($"The answer to 4-2 is {guardId}x{maxMinute}={guardId * maxMinute}");
        }

        public static List<Shift> BuildShifts(string[] input)
        {
            var inputRecords = input.Select(ir => new InputRecord()
            {
                TimeStamp = Convert.ToDateTime(ir.Substring(1, 16)),
                Guard = (Int32.TryParse((ir + "#####").Split(new char[] { ' ', '#' })[4], out int gId)) ? gId : 0,
                InputRecordType = (ir.Contains("falls asleep")) ? InputRecordType.FellAsleep : (ir.Contains("wakes up")) ? InputRecordType.Awoke : InputRecordType.ShiftStart
            })
            .ToArray();

            inputRecords = inputRecords.OrderBy(ir => ir.TimeStamp).ToArray();

            List<Shift> shifts = new List<Shift>();
            Shift currentShift = new Shift();
            int i = 0;
            while (i < inputRecords.Count())
            {
                if (inputRecords[i].TimeStamp.Hour == 23)
                {
                    inputRecords[i].TimeStamp.AddMinutes(60 - inputRecords[i].TimeStamp.Minute);
                }
                switch (inputRecords[i].InputRecordType)
                {
                    case InputRecordType.ShiftStart:
                        currentShift = new Shift()
                        {
                            Guard = inputRecords[i].Guard,
                            Date = inputRecords[i].TimeStamp,
                            SleepRecords = new List<SleepRecord>()
                        };
                        shifts.Add(currentShift);
                        break;
                    case InputRecordType.FellAsleep:
                        var sleepRecord = new SleepRecord()
                        {
                            AsleepMinute = inputRecords[i].TimeStamp.Minute
                        };
                        if (i + 1 < inputRecords.Count() && inputRecords[i + 1].InputRecordType == InputRecordType.Awoke)
                        {
                            i++;
                            sleepRecord.AwakeMinute = inputRecords[i].TimeStamp.Minute;
                        }
                        else
                        {
                            sleepRecord.AwakeMinute = 60;
                        }
                        currentShift.SleepRecords.Add(sleepRecord);
                        break;
                    default:
                        throw new Exception("Should never hit this");
                }
                i++;
            }
            return shifts;
        }
    }

    public class InputRecord
    {
        public DateTime TimeStamp { get; set; }
        public int Guard { get; set; }
        public InputRecordType InputRecordType { get; set; }
    }

    public enum InputRecordType
    {
        ShiftStart,
        FellAsleep,
        Awoke,
    }

    public class Shift
    {
        public DateTime Date { get; set; }
        public int Guard { get; set; }
        public List<SleepRecord> SleepRecords { get; set; }
        public int MinutesSlept()
        {
            return SleepRecords.Sum(sr => sr.AwakeMinute - sr.AsleepMinute);
        }

        public bool WasAsleep(int minute)
        {
            foreach (var sleepRecord in SleepRecords)
            {
                if (minute >= sleepRecord.AsleepMinute && minute < sleepRecord.AwakeMinute)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class SleepRecord
    {
        public int AsleepMinute { get; set; }
        public int AwakeMinute { get; set; }
    }
}
