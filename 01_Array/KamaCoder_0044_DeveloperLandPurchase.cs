// 44. 开发商购买土地
// https://kamacoder.com/problempage.php?pid=1044
// 时间复杂度：O(n*m)
// 空间复杂度：O(1)
// 核心思路：前缀和
//   - 算整个矩阵的总价值 total
//   - 横切：rowSum 累加每行，差值 = abs(total - 2 * rowSum)
//   - 竖切：colSum 累加每列，差值 = abs(total - 2 * colSum)
//   - 取所有切法中的最小差值
// 关键点：
//   - 横切遍历每行所有列元素求行和，竖切遍历每列所有行元素求列和
//   - 每次切完后用 Math.Min 更新最小值，不是覆盖
//   - Math.Abs 返回 int，不用 MathF.Abs（返回 float）

using System;

namespace TestNamespace
{
    class Program
    {
        public static int Solution(int row, int col, int[][] nums)
        {
            int total = 0;

            // 1. 算总价值
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    total += nums[i][j];
                }
            }

            int minDiff = int.MaxValue;

            // 2. 横切：rowSum = 前 i+1 行的累加和
            int rowSum = 0;
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    rowSum += nums[i][j];          // 把第 i 行所有元素加进 rowSum
                }
                int diff = Math.Abs(total - 2 * rowSum);  // 差值
                minDiff = Math.Min(minDiff, diff);         // 取最小
            }

            // 3. 竖切：colSum = 前 j+1 列的累加和
            int colSum = 0;
            for (int j = 0; j < col; j++) {
                for (int i = 0; i < row; i++) {
                    colSum += nums[i][j];          // 把第 j 列所有元素加进 colSum
                }
                int diff = Math.Abs(total - 2 * colSum);
                minDiff = Math.Min(minDiff, diff);
            }

            return minDiff;
        }

        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);

            int[][] nums = new int[n][];
            for (int i = 0; i < n; i++) {
                nums[i] = new int[m];
                string[] rowData = Console.ReadLine().Split();
                for (int j = 0; j < m; j++) {
                    nums[i][j] = int.Parse(rowData[j]);
                }
            }

            int result = Solution(n, m, nums);
            Console.WriteLine(result);
        }
    }
}
