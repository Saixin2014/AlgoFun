using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sai.Cos.DivideWater
{
    class Program
    {
        public struct TagAction
        {
            public int from;
            public int to;
            public int water;
        }

        static int[] FullBacket = { 8, 5, 3 };
        static Stack<int[]> StackState = new Stack<int[]>();//存放数组数据的堆栈
        static int ResultCount = 0;//求解的计数
        static void Main(string[] args)
        {
            StackState.Push(new int[] { 8, 0, 0 });
            SeachState(StackState);
            Console.ReadKey();
        }

        static void SeachState(Stack<int[]> states)
        {
            //int[][] ar = states.ToArray<int[]>();
            int[] curr = states.Peek();
            if (curr[0] == 4 && curr[1] == 4)
            {            //找到正确解
                var rs = "";
                ResultCount++;
                int[][] ar = StackState.ToArray<int[]>();
                for (int i = ar.Length - 1; i >= 0; i--)
                {
                    var t = ar[i];
                    rs += t[0] + "," + t[1] + "," + t[2] + " -> ";
                }
                Console.WriteLine("第"+ResultCount+"个移动方案："+rs.TrimEnd(" -> ".ToArray()));
                Console.WriteLine();
                return;
            }
            for (var j = 0; j < 3; j++)
            {                //所有的倒水方案即为桶编号的全排列
                for (var i = 0; i < 3; i++)
                {
                    if (CanTakeDumpAction(curr, i, j))
                    {
                        var next = DumpWater(curr, i, j);
                        if (!IsStateExist(next))
                        {        //找到新状态
                            states.Push(next);
                            SeachState(states);
                            states.Pop();
                        }
                    }
                }
            }
        }

        public static bool CanTakeDumpAction(int[] curr, int from, int to)
        {
            if (from >= 0 && from < 3
                && to >= 0 && to < 3)
            {
                if (from != to && curr[from] > 0 && curr[to] < FullBacket[to])
                {
                    return true;
                }
            }
            return false;
        }

        static int[] DumpWater(int[] curr, int from, int to)
        {
            int[] next = new int[3];
            Array.Copy(curr, next, curr.Length);
            var dump_water = FullBacket[to] - curr[to] > curr[from] ? curr[from] : FullBacket[to] - curr[to]; //倒水量的计算
            next[from] = curr[from] - dump_water;
            next[to] = curr[to] + dump_water;
            return next;
        }

        static bool IsStateExist(int[] state)
        {
            foreach (var ar in StackState)
            {
                if (state[0] == ar[0]
                    && state[1] == ar[1]
                    && state[2] == ar[2])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
