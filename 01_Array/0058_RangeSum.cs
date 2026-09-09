// 58. 区间和
// https://kamacoder.com/problempage.php?pid=1070
// 时间复杂度：预处理 O(n)，每次查询 O(1)
// 空间复杂度：O(n)
// 核心思路：前缀和
//   - 预处理：array[i] = nums[0] + nums[1] + ... + nums[i]
//   - 查询区间 [begin, end]（1-based）的和 = array[end] - array[begin - 1]
// 关键点：前缀和把每次查询从 O(n) 降到 O(1)

using System;

namespace TestNamespace
{
    class Program
    {
        public static int Solution(int begin, int end, int[] nums)
        {
            // 构建前缀和数组：array[i] = nums[0] + nums[1] + ... + nums[i]
            int[] array = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                if (i == 0)
                {
                    array[i] = nums[i];                    // 第一个元素直接赋值
                }
                else
                {
                    array[i] = nums[i] + array[i - 1];     // 当前元素 + 前面所有的和
                }
            }
            // 核心：区间 [begin, end] 的和 = 前缀和[end] - 前缀和[begin-1]
            // 原理：array[end] 包含了 0~end 的所有元素
            //       减去 array[begin-1] 就去掉了 0~begin-1 的部分，剩下 begin~end
            return array[end] - array[begin - 1];
        }

        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int[] nums = new int[n];
            for (int i = 0; i < n; i++)
            {
                nums[i] = int.Parse(Console.ReadLine());
            }
            int res = Solution(1, 3, nums);
            Console.WriteLine(res);
        }
    }
}
