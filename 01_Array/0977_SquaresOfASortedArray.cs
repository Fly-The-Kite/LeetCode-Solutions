// 977. 有序数组的平方
// https://leetcode.cn/problems/squares-of-a-sorted-array/
// 时间复杂度：O(n)
// 空间复杂度：O(n)
// 核心思路：双指针从两端往中间走，因为平方最大的数一定在数组两端（负数平方可能很大）
// 关键点：结果数组从后往前填（k 从末尾开始），每次取两端平方较大的那个放进去

public class Solution {
    public int[] SortedSquares(int[] nums) {
        int[] res = new int[nums.Length];        // 结果数组，存排序后的平方值
        int k = nums.Length - 1;                 // k 指向结果数组末尾，从后往前填
        int left = 0;                            // 左指针，指向数组开头
        int right = nums.Length - 1;             // 右指针，指向数组末尾

        for (int i = 0; i < nums.Length; i++) {
            if (nums[left] * nums[left] <= nums[right] * nums[right]) {
                res[k--] = nums[right] * nums[right];  // 右边平方大，放进结果末尾
                right--;                                 // 右指针左移
            } else {
                res[k--] = nums[left] * nums[left];    // 左边平方大，放进结果末尾
                left++;                                  // 左指针右移
            }
        }
        return res;
    }
}
