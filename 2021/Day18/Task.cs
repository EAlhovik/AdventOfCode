using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021.Day18
{
    class Task : BaseTask<int>
    {
        public class Node
        {
            public int? Value { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public int Magnitude()
            {
                if (Value.HasValue)
                {
                    return Value.Value;
                }
                else
                {
                    return Left.Magnitude() * 3 + Right.Magnitude() * 2;
                }
            }

            private int GetTreeDepth(Node parent)
            {
                return parent.Value.HasValue
                    ? 0
                    : Math.Max(GetTreeDepth(parent.Left), GetTreeDepth(parent.Right)) + 1;
            }

            internal void UpdateLeft(Node left)
            {
                var leftNode = GetLeftUpNode(left);
                if(leftNode != null)
                {
                    leftNode.Value += left.Value.Value;
                }
            }
            private Node GetLeftUpNode(Node node)
            {
                if(node.Parent == null)
                {
                    return null;
                }
                else if (ReferenceEquals(node.Parent.Left, node))
                {
                    return GetLeftUpNode(node.Parent);
                }
                else
                {
                    return GetRightDown(node.Parent.Left);
                }
            }

            private Node GetRightDown(Node node)
            {
                if (node.Value.HasValue)
                {
                    return node;
                }
                else
                {
                    return GetRightDown(node.Right);
                }
            }

            internal void UpdateRight(Node right)
            {
                var rightNode = GetRightUpNode(right);
                if (rightNode != null)
                {
                    rightNode.Value += right.Value.Value;
                }
            }

            private Node GetRightUpNode(Node node)
            {
                if (node.Parent == null)
                {
                    return null;
                }
                else if (ReferenceEquals(node.Parent.Right, node))
                {
                    return GetRightUpNode(node.Parent);
                }
                else
                {
                    return GetLeftDown(node.Parent.Right);
                }
            }

            private Node GetLeftDown(Node node)
            {
                if (node.Value.HasValue)
                {
                    return node;
                }
                else
                {
                    return GetLeftDown(node.Left);
                }
            }
        }

        public override int ExpectedPart1Test { get; set; } = 4140;
        public override int ExpectedPart2Test { get; set; } = 3993;
        public override int SolvePart1(IEnumerable<string> input)
        {
            var values = input.Select((p, index) => ((p, index))).ToList();


            var result = values.Where(p => p.index != 0).Aggregate(Parse(values[0].p, null), (workingSentence, next) => Reduce(workingSentence, Parse(next.p, null)));
            return result.Magnitude();
        }
        public override int SolvePart2(IEnumerable<string> input)
        {
            var values = input.ToList();
            var maxMagnitude = int.MinValue;
            for (int i = 0; i < values.Count - 1; i++)
            {
                for (int j = i + 1; j < values.Count; j++)
                {
                    maxMagnitude = Math.Max(maxMagnitude, Reduce(Parse(values[i], null), Parse(values[j], null)).Magnitude());
                    maxMagnitude = Math.Max(maxMagnitude, Reduce(Parse(values[j], null), Parse(values[i], null)).Magnitude());
                }
            }
            return maxMagnitude;
        }

        private Node Reduce(Node a, Node b)
        {
            var node = new Node
            {
                Left = a,
                Right = b
            };
            a.Parent = node;
            b.Parent = node;
            var reduction = true;
            while (reduction)
            {
                reduction = Explode(node, 4);

                if (!reduction)
                {
                    if (Split(node))
                    {
                        reduction = true;
                    }
                }
            }
            return node;
        }
        private bool Split(Node parent)
        {
            if (parent.Value.HasValue)
            {
                if(parent.Value > 9)
                {
                    parent.Left = new Node
                    {
                        Value = parent.Value / 2,
                        Parent = parent
                    };
                    parent.Right = new Node
                    {
                        Value = parent.Value - parent.Left.Value,
                        Parent = parent
                    };
                    parent.Value = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Split(parent.Left) || Split(parent.Right);
            }
        }

        private bool Explode(Node node, int index, bool hadExplode = false)
        {
            if(index == 0 && node.Left != null)
            {
                node.UpdateLeft(node.Left);
                node.UpdateRight(node.Right);
                node.Value = 0;
                node.Left = null;
                node.Right = null;
                hadExplode = true;
            }
            //var s = node.GetTreeDepth();
            if (!node.Value.HasValue)
            {
                hadExplode = Explode(node.Left, index - 1, hadExplode) || hadExplode;
                hadExplode = Explode(node.Right, index - 1, hadExplode) || hadExplode;
            }
            return hadExplode;
        }

        private Node Parse(string value, Node parent = null)
        {
            if(int.TryParse(value, out int nodeValue))
            {
                return new Node { Value = nodeValue, Parent = parent };
            }
            value = value.Substring(1, value.Length - 2);
            var open = 0;
            var close = 0;
            var index = 0;
            for (; index < value.Length; index++)
            {
                if (value[index] == ',' && open == close) break;
                if (value[index] == '[') open++;
                if (value[index] == ']') close++;
            }
            var left = value.Substring(0, index);
            var right = value.Substring(index + 1, value.Length - index - 1);

            var newParent = new Node
            {
                Parent = parent
            };

            newParent.Left = Parse(left, newParent);
            newParent.Right = Parse(right, newParent);
            return newParent;
        }

    }
}
