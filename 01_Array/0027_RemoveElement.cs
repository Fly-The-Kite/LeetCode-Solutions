// 27. 移除元素
// https://leetcode.cn/problems/remove-element/
// 时间复杂度：O(n)
// 空间复杂度：O(1)
// 核心思路：双指针（快慢指针），fast 扫描数组，slow 只存不等于 val 的元素
// 关键点：fast 每步都走，slow 只在遇到合格元素时走；slow 最终值就是新数组长度

public class Solution {
    public int RemoveElement(int[] nums, int val) {
        int slow = 0;                                       // 慢指针：指向下一个要存放的位置
        for (int fast = 0; fast < nums.Length; fast++) {    // 快指针：从头到尾扫描每个元素
            if (nums[fast] != val) {                         // 如果 fast 指向的元素不等于目标值
                nums[slow++] = nums[fast];                   // 把它放到 slow 的位置，slow 前进一格
            }
        }
        return slow;                                        // slow 走过的长度就是新数组的长度
    }
}
