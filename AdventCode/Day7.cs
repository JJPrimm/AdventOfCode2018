using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public static class Day7
    {

        public static void Problem1()
        {
            var input = Utilities.ReadStringArray(7);

            string workflow = "";

            List<Step> steps = new List<Step>();

            foreach(var inputString in input)
            {
                var id = Convert.ToChar(inputString.Split(' ')[7]);
                var prerequisite = Convert.ToChar(inputString.Split(' ')[1]);

                if (!steps.Exists(s => s.Id == id))
                {
                    steps.Add(new Step()
                    {
                        Id = id,
                        Prerequisites = new List<char>() { prerequisite }
                    });

                }
                else
                {
                    steps.Single(s => s.Id == id).Prerequisites.Add(prerequisite);
                }
                if (!steps.Exists(s => s.Id == prerequisite))
                {
                    steps.Add(new Step()
                    {
                        Id = prerequisite,
                        Prerequisites = new List<char>()
                    });
                }
            }

            while (steps.Count > 0)
            {
                var nextStep = steps.Where(s => s.Ready())
                    .OrderBy(s => s.Id)
                    .First();

                workflow = workflow + nextStep.Id;
                steps.ForEach(s => s.Prerequisites.Remove(nextStep.Id));
                steps.Remove(nextStep);
            }
            Console.WriteLine(workflow);
        }

        public static void Problem2()
        {
            var input = Utilities.ReadStringArray(7);
            var charToInt = Utilities.CharToInt();

            List<Step> steps = new List<Step>();

            foreach (var inputString in input)
            {
                var id = Convert.ToChar(inputString.Split(' ')[7]);
                var prerequisite = Convert.ToChar(inputString.Split(' ')[1]);

                if (!steps.Exists(s => s.Id == id))
                {
                    steps.Add(new Step()
                    {
                        Id = id,
                        Ticks = 60 + charToInt[id],
                        Prerequisites = new List<char>() { prerequisite }
                    });

                }
                else
                {
                    steps.Single(s => s.Id == id).Prerequisites.Add(prerequisite);
                }
                if (!steps.Exists(s => s.Id == prerequisite))
                {
                    steps.Add(new Step()
                    {
                        Id = prerequisite,
                        Ticks = 60 + charToInt[prerequisite],
                        Prerequisites = new List<char>()
                    });
                }
            }

            //scenario loaded... start working
            int ticks = 0;
            int assignedWorkers = 0;
            while (steps.Count > 0)
            {
                //assign work
                foreach(var step in steps.Where(s => s.Ready() && s.Working == false).OrderBy(s => s.Id))
                {
                    if (assignedWorkers < 5)
                    {
                        step.Working = true;
                        assignedWorkers++;
                    }
                    else
                    {
                        break;
                    }
                }

                //do work
                ticks++;
                steps.Where(s => s.Working)
                    .ToList()
                    .ForEach(s => s.Ticks = s.Ticks - 1);

                //cleanup

                foreach (var completedStep in steps.Where(s => s.Working && s.Ticks == 0))
                {
                    steps.ForEach(s => s.Prerequisites.Remove(completedStep.Id));
                    assignedWorkers--;
                }
                steps.RemoveAll(s => s.Working && s.Ticks == 0);
            }
            Console.WriteLine(ticks);
        }
    }

    public class Step
    {
        public char Id { get; set; }

        public List<char> Prerequisites { get; set; }

        public int Ticks;

        public bool Working { get; set; } = false;

        public bool Ready()
        {
            return Prerequisites.Count() == 0;
        }        
    }
}
