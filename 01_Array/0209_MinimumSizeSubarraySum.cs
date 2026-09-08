// 209. 长度最小的子数组
// https://leetcode.cn/problems/minimum-size-subarray-sum/
// 时间复杂度：O(n)
// 空间复杂度：O(1)
// 核心思路：滑动窗口（本质是双指针的变种）
//   - right 指针向右扩展窗口，把元素累加进 sum
//   - 当 sum >= target 时，left 指针向右收缩窗口，寻找更短的满足条件的子数组
//   - 每次满足条件时记录窗口长度，取最小值
// 关键点：while(sum >= target) 是收缩窗口的条件，不是 if

public class Solution {
    public int MinSubArrayLen(int target, int[] nums) {
        int left = 0;
        int sum = 0;
        int length = int.MaxValue;               // 记录最短子数组长度，初始设为最大值

        for (int right = 0; right < nums.Length; right++) {
            sum += nums[right];                   // 右指针扩展窗口，加入新元素

            while (sum >= target) {              // 满足条件时收缩窗口
                length = Math.Min(length, right - left + 1);  // 记录当前窗口长度
                sum -= nums[left];                // 左指针收缩，移出左边界元素
                left++;                          // 左指针右移
            }
        }

        return length == int.MaxValue ? 0 : length;  // 如果从未找到满足条件的子数组，返回0
    }
}
