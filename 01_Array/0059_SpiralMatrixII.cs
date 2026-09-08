// 59. 螺旋矩阵II
// https://leetcode.cn/problems/spiral-matrix-ii/
// 时间复杂度：O(n²)
// 空间复杂度：O(n²)
// 核心思路：模拟，四个边界不断往内缩，每圈填四条边
// 关键点：
//   - 四个方向循环的起止条件：递增用 i<=x，递减用 i>=x
//   - 每填完一条边，对应边界缩一格（top++ / right-- / bottom-- / left++）
//   - 用 num <= n*n 作为终止条件，避免奇数中心点的边界判断问题
//   - 锯齿数组 int[][] 需要逐行初始化：res[i] = new int[n]

public class Solution {
    public int[][] GenerateMatrix(int n) {
        // 锯齿数组声明 + 逐行初始化
        int[][] res = new int[n][];
        for (int i = 0; i < n; i++) {
            res[i] = new int[n];
        }

        int num = 1;
        int top = 0;
        int bottom = n - 1;
        int left = 0;
        int right = n - 1;

        while (num <= n * n) {
            // 1. 从左到右填上边（行固定=top，列递增）
            for (int i = left; i <= right; i++) {
                res[top][i] = num++;
            }
            top++;

            // 2. 从上到下填右边（列固定=right，行递增）
            for (int i = top; i <= bottom; i++) {
                res[i][right] = num++;
            }
            right--;

            // 3. 从右到左填下边（行固定=bottom，列递减）
            for (int i = right; i >= left; i--) {
                res[bottom][i] = num++;
            }
            bottom--;

            // 4. 从下到上填左边（列固定=left，行递减）
            for (int i = bottom; i >= top; i--) {
                res[i][left] = num++;
            }
            left++;
        }

        return res;
    }
}
