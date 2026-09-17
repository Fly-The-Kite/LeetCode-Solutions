// 203. 移除链表元素
// https://leetcode.cn/problems/remove-linked-list-elements/
// 时间复杂度：O(n)
// 空间复杂度：O(1)
// 核心思路：虚拟头节点（哨兵节点）+ 临时指针遍历
//   - 创建哨兵节点 res，res.next 指向 head，统一处理头节点删除的情况
//   - 用 temp 指针遍历，检查 temp.next 是否需要删除
//   - 需要删除：temp.next = temp.next.next（跳过目标节点）
//   - 不需要删除：temp = temp.next（temp 往前走）
// 关键点：
//   - 必须用 temp 遍历，不能用 res 遍历
//     res 是"锚"，负责守住起点；temp 是"工人"，负责移动操作
//     如果用 res 遍历，res 会跑到链表中间，最后 return res.next 就丢了前半段
//   - return res.next 而不是 return head，因为 head 可能已经被删除了

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode RemoveElements(ListNode head, int val) {
        // 哨兵节点：val=0，next 指向 head
        // 作用：统一处理头节点需要删除的情况，不用特判
        ListNode res = new ListNode(0, head);

        // 临时指针：从哨兵开始遍历
        // 为什么不直接用 res？因为 res 要守住起点，不能动
        ListNode temp = res;

        // 遍历条件：temp.next != null（看的是下一个节点）
        while (temp.next != null) {
            if (temp.next.val == val) {
                // 下一个节点要删除 → 跳过它，temp 不动
                // 因为新的 temp.next 可能也等于 val，需要再检查一次
                temp.next = temp.next.next;
            } else {
                // 下一个节点不删 → temp 往前走
                temp = temp.next;
            }
        }

        // 返回 res.next 而不是 head
        // 因为 head 可能已经被删除了，res.next 才是新链表的头
        return res.next;
    }
}
