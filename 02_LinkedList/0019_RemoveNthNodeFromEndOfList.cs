// 19. 删除链表的倒数第 N 个结点
// https://leetcode.cn/problems/remove-nth-node-from-end-of-list/
// 时间复杂度：O(n) —— 快指针先走 n+1 步，然后快慢一起走到尾，链表只扫一遍
// 空间复杂度：O(1) —— 只用哨兵节点 + 快慢两个指针，没有额外容器
// 核心思路：哨兵节点 + 快慢指针（固定间隔）
//   - newList 是哨兵（dummy）节点，放在 head 前面，next 指向 head
//   - 快指针先走 n+1 步（不是 n 步），这样它和慢指针之间就固定隔开 n 个节点
//   - 然后快慢一起前进，快指针到尾（null）时，慢指针正好停在"要删的节点的前一个"
//   - 最后 slow.next = slow.next.next 跳过目标节点
// 关键点：
//   - 为什么快指针走 n+1 步：最终要让慢指针停在"前一个"（删节点必须拿到前驱）。
//     多走 1 步正是把这个位置差留出来。
//   - 哨兵节点的作用：**统一"删的正好是头节点"这种情况**
//     如果从 head 出发找前驱，删头时前驱根本不存在，得单独写一段分支；
//     有了哨兵，删头就等于"删中间某个节点"，一套代码走完。
//     实测 [1] n=1 和 [1,2,3,4,5] n=5，删的都是头 → 返回 newList.next 天然正确。
//   - 循环条件 fast != null（不是 fast.next != null）：要让快指针真的走到链表外面，
//     慢指针才会落在前驱上。
//   - 最后那个 if(slow.next != null) 是防御性写法 —— 正常流程下 slow.next 一定非空
//     （因为 n 合法时 slow 停在前驱上），加上它不影响结果。
//   - ⚠️ 已知边界：空链表（head = null）会抛 NullReferenceException ——
//     哨兵后面没有节点，fast 第一步就取到 null.next。
//     LeetCode 的用例保证 n 合法（1 <= n <= 链表长度），所以不影响提交；
//     但心里要知道这一条（对比 206 反转链表：那题的写法空链表天然安全）。
//   - 本地实测（2026-09-29 跑过）：
//       [1]           n=1 → []
//       [1,2]         n=1 → [1]    （删尾）
//       [1,2]         n=2 → [2]    （删头）
//       [1,2,3,4,5]   n=2 → [1,2,3,5]   （官方示例）
//       [1,2,3,4,5]   n=1 → [1,2,3,4]   （删尾）
//       [1,2,3,4,5]   n=5 → [2,3,4,5]   （删头，n = 链表长度）

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
    public ListNode RemoveNthFromEnd(ListNode head, int n) {
        // 哨兵节点：挂在 head 前面，让"删头"不用单独处理
        ListNode newList = new ListNode(0);
        newList.next = head;

        // 快慢指针都从哨兵出发
        ListNode fast = newList;
        ListNode slow = newList;

        // 快指针先走 n+1 步（多走的 1 步是给哨兵留的），
        // 走完之后 fast 和 slow 之间正好隔着 n 个节点
        for(int i=0;i<=n;i++){
            fast = fast.next;
        }

        // 快慢一起走，直到快指针走出链表
        // 结束时 slow 停在"倒数第 n 个节点的前一个"
        while(fast != null){
            fast = fast.next;
            slow = slow.next;
        }

        // 跳过目标节点（防御性判空：n 合法时 slow.next 一定非空）
        if(slow.next != null){
            slow.next=slow.next.next;
        }

        // 返回哨兵的下一个 —— 就算删的是头节点，这里也是对的
        return newList.next;
    }
}
