// 704. 二分查找
// https://leetcode.cn/problems/binary-search/
// 时间复杂度：O(log n)
// 空间复杂度：O(1)
// 核心思路：每次取中间值比较，缩小一半搜索范围
// 区间定义：左闭右闭 [left, right]
// 关键点：while 用 <= 因为 left==right 时区间仍然有效；right=mid-1 因为 mid 已排除
//
// 左闭右开 [left, right) 写法对比：
//   int right = nums.Length;        // 不 -1，因为右开
//   while (left < right)           // 不用 <=，因为 left==right 时区间无效
//   right = mid;                   // 不 -1，因为 mid 不在区间内
//   其余逻辑相同，面试写任意一种即可，左闭右闭更直观

public class Solution {
    public int Search(int[] nums, int target) {
        int left = 0;
        int right = nums.Length - 1;          // 右闭，所以 -1
        while(left <= right){                  // left==right 仍有意义，用 <=
            int mid = (left + right) / 2;      // 面试可写 left + (right-left)/2 防溢出
            if(nums[mid] > target){
                right = mid - 1;              // target 在左半，mid 已比较过所以 -1
            }
            else if(nums[mid] < target){
                left = mid + 1;               // target 在右半，mid 已比较过所以 +1
            }
            else{
                return mid;                   // 找到了，返回下标
            }
        }
        return -1;                             // 没找到
    }
}
