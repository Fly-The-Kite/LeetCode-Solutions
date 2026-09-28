// 206. 反转链表
// https://leetcode.cn/problems/reverse-linked-list/
// 时间复杂度：O(n)
// 空间复杂度：O(1)
// 核心思路：三指针迭代（pre / cur / temp），边走边掉头
//   - pre 指向"已经反转好的那一段"的头，初始为 null
//   - cur 指向"正要处理"的节点，初始为 head
//   - temp 先存下 cur.next（因为下一步 cur.next 就要被改写）
//   - 每轮做四件事：存后继 → 掉头 → pre 前进 → cur 前进
// 关键点：
//   - 循环条件必须是 cur != null，不是 cur.next != null
//     因为反转动作改的就是 cur.next，拿它当判断条件会自相矛盾。
//     实测（2026-09-28 本地跑过）：
//       []        → 抛 NullReferenceException（cur 是 null，还去读 cur.next）
//       [1]       → []
//       [1,2]     → [1]
//       [1,2,3]   → [2,1]
//       [1,2,3,4,5] → [4,3,2,1]
//     统一规律：**永远少处理最后一个节点**。
//     原因是「处理最后一个节点」这一步，正是"把新链表的尾巴接成 null"的时刻；
//     漏掉它，最后一个节点就整个掉队了（不在返回的链表里）
//   - 空链表 head = null 时，cur != null 则一轮都不进，直接 return null，天然正确
//   - 返回 pre 而不是 cur：循环结束时 cur 已经变成 null 了
//   - 三指针骨架是所有链表题的地基，反转/找中点/判环/合并都用它
//   - 边界用例至少要跑这四个：[] · [1] · [1,2] · [1,2,3]

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
    public ListNode ReverseList(ListNode head) {
        // pre：已经反转好的那一段的头，一开始还没有反转好的部分，所以是 null
        ListNode pre = null;

        // cur：正要处理的节点，从头开始
        ListNode cur = head;

        // 遍历条件：手上还有节点就处理它
        // 注意是 cur != null，不是 cur.next != null（原因见文件头「关键点」）
        while (cur != null) {
            // 第 1 步：先把后继存下来
            // 必须放在最前面 —— 因为第 2 步就会把 cur.next 覆盖掉，
            // 不先存，后面的节点就找不回来了
            ListNode temp = cur.next;

            // 第 2 步：掉头
            // 让当前节点指回前一个，这就是"反转"这个动作本身
            cur.next = pre;

            // 第 3 步：pre 前进到当前节点
            // 反转头变成了刚处理完的这个节点
            pre = cur;

            // 第 4 步：cur 前进到下一个待处理节点
            // 用的是第 1 步存下的 temp，不是已经被改掉的 cur.next
            cur = temp;
        }

        // 循环结束时 cur 是 null，能返回的只有 pre
        // 而 pre 正好指向原链表的最后一个节点，也就是新链表的头
        return pre;
    }
}
