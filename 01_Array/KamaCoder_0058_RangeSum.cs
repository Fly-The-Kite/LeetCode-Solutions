// [卡码网 58] 区间和（第九期模拟笔试）
// https://kamacoder.com/problempage.php?pid=1070
// 注意：这是卡码网的题，不是 LeetCode 58 (Length of Last Word)，两者毫无关系
// 时间复杂度：预处理 O(n)，每次查询 O(1)
// 空间复杂度：O(n)
// 核心思路：前缀和
//   - 预处理一次：prefix[i] = nums[0] + nums[1] + ... + nums[i]
//   - 查询 [begin, end]（0-based 闭区间）= prefix[end] - prefix[begin - 1]
// 关键点：
//   - 卡码网的下标是 0-based，不是 1-based
//     官方样例第一组查询就是 "0 1"，这时 prefix[begin - 1] 会变成 prefix[-1] 直接越界
//     所以 begin == 0 时必须单独返回 prefix[end]
//   - 查询组数未知，要一直读到文件结束（EOF），不是只读一组
//   - 前缀和只算一次。如果放进 Solution 里每次查询重算一遍，
//     复杂度就从「O(n) + O(1) x q」退化成 O(n x q)，n = 100000 时会超时
//   - 数据范围 0 < n <= 100000；若权值范围也很大，prefix 应改用 long

using System;

namespace TestNamespace
{
    class Program
    {
        // 预处理：prefix[i] = nums[0] + nums[1] + ... + nums[i]
        public static int[] BuildPrefix(int[] nums)
        {
            int[] prefix = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                if (i == 0)
                {
                    prefix[i] = nums[i];                    // 第一个元素直接赋值
                }
                else
                {
                    prefix[i] = nums[i] + prefix[i - 1];    // 当前元素 + 前面所有的和
                }
            }
            return prefix;
        }

        // 查询 0-based 闭区间 [begin, end] 的和
        public static int Solution(int[] prefix, int begin, int end)
        {
            // 原理：prefix[end] 包含了 0~end 的所有元素
            //       减去 prefix[begin-1] 就去掉了 0~begin-1 的部分，剩下 begin~end
            //       但 begin == 0 时没有「前一个」，prefix[-1] 会越界，此时直接返回 prefix[end]
            if (begin == 0)
            {
                return prefix[end];
            }
            return prefix[end] - prefix[begin - 1];
        }

        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int[] nums = new int[n];
            for (int i = 0; i < n; i++)
            {
                nums[i] = int.Parse(Console.ReadLine());
            }

            int[] prefix = BuildPrefix(nums);   // 整个程序只算一次

            // 查询组数未知，一直读到文件结束
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (line.Trim().Length == 0)
                {
                    continue;                   // 跳过空行
                }
                string[] parts = line.Split();
                int begin = int.Parse(parts[0]);
                int end = int.Parse(parts[1]);
                Console.WriteLine(Solution(prefix, begin, end));
            }
        }
    }
}
