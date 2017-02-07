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
        public struct BucketState
        {
            public int b0;
            public int b1;
            public int b2;
            public TagAction curAction;
        }
        static int[] FullBacket = { 8, 5, 3 };
        static void Main(string[] args)
        {
            QueState.Enqueue(new int[]{8,0,0 });
            SeachState(QueState);
            Console.ReadKey();
        }

        static void SeachState(Queue<int[]> states)
        {
            int[][] ar = states.ToArray<int[]>();
            int[] curr = ar[states.Count - 1];
            if (curr[0] == 4 && curr[1] == 4)
            {            //找到正确解
                var rs = "";

                //int[][] ar = QueState.ToArray<int[]>();
                foreach (var t in QueState)
                {
                    rs += t[0] + "," + t[1] + "," + t[2]+ " -> ";
                }
                Console.WriteLine(rs.TrimEnd(" -> ".ToArray()));
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
                            states.Enqueue(next);
                            SeachState(states);
                            states.Dequeue();
                        }
                    }
                }
            }
        }

        static Queue<int[]> QueState = new Queue<int[]>();
        public static bool CanTakeDumpAction(int[] curr, int from, int to)
        {
            if (from >= 0 && from < 3 && to >= 0 && to < 3)
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
            int[] next = new int[3]; ;// curr.slice();        //js对象为引用传值，这里要复制一份
            Array.Copy(curr, next, curr.Length);
            var dump_water = FullBacket[to] - curr[to] > curr[from] ? curr[from] : FullBacket[to] - curr[to];            //倒水量的计算
            next[from] = curr[from]- dump_water;
            next[to] = curr[to]+ dump_water;
            return next;
        }

        static bool IsStateExist(int[] state)
        {
            int[][] ar = QueState.ToArray<int[]>();
            for (var i = 0; i < QueState.Count; i++)
            {
                if (state[0] == ar[i][0]
                    && state[1] == ar[i][1]
                    && state[2] == ar[i][2])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
