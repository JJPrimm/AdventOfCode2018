using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode
{
    public static class Day8
    {
        public static void Problem1()
        {
            var nodes = Utilities.ReadSpaceSeparatedIntArray(8);
            int index = 0;
            int totalMetadata = 0;
            {
                while (index < nodes.Length - 1)
                {
                    totalMetadata += SumNodeMetadata(nodes, index, out index);
                }
            }
            Console.WriteLine($"Total Metadata = {totalMetadata}");
        }

        public static void Problem2()
        {
            var values = Utilities.ReadSpaceSeparatedIntArray(8);
            int index = 0;
            var root = BuildNode(values, index, out index);
            Console.WriteLine($"Root value = {root.Value()}");
        }

        public static int SumNodeMetadata(int[] nodes, int index, out int nextIndex)
        {
            int metaData = 0;
            int i = index;
            int childrenCount = nodes[i++];
            int metadataCount = nodes[i++];
            for (int c = 0; c < childrenCount; c++)
            {
                metaData += SumNodeMetadata(nodes, i, out i);
            }
            for (int m = 0; m < metadataCount; m++)
            {
                metaData += nodes[i++];
            }
            nextIndex = i;
            return metaData;
        }

        public static Node BuildNode(int[] values, int index, out int nextIndex)
        {
            int i = index;
            int childrenCount = values[i++];
            int metadataCount = values[i++];
            var node = new Node()
            {
                Children = new List<Node>(),
                Metadata = new List<int>()
            };
            for (int c = 0; c < childrenCount; c++)
            {
                node.Children.Add(BuildNode(values, i, out i));
            }
            for (int m = 0; m < metadataCount; m++)
            {
                node.Metadata.Add(values[i++]);
            }
            nextIndex = i;
            return node;
        }
    }   

    public class Node
    {
        public List<Node> Children { get; set; }
        public List<int> Metadata { get; set; }
        public int Value()
        {
            if (Children.Count == 0)
            {
                return Metadata.Sum();
            }
            else
            {
                var value = 0;
                foreach (var childIndex in Metadata)
                {
                    if (childIndex <= Children.Count)
                    {
                        value += Children[childIndex - 1].Value();
                    }
                }
                return value;
            }
        }
    }
}
