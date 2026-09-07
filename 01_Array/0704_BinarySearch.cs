// 704. 二分查找
// https://leetcode.cn/problems/binary-search/
// 时间复杂度：O(log n)
// 空间复杂度：O(1)
// 区间定义：左闭右闭 [left, right]

public class Solution {
    public int Search(int[] nums, int target) {
        int left = 0;
        int right = nums.Length - 1;
        while(left <= right){
            int mid = (left + right)/2;
            if(nums[mid] > target){
                right = mid - 1;
            }
            else if(nums[mid] < target){
                left = mid + 1;
            }
            else{
                return mid;
            }
        }
        return -1;
    }
}
