// 142. 环形链表 II
// https://leetcode.cn/problems/linked-list-cycle-ii/
// 时间复杂度：O(n) —— 快指针最多走 2n 步；判定相遇后 index1/index2 再走的总步数也不超过 n
// 空间复杂度：O(1) —— 只有 slow / fast / index1 / index2 四个指针，没有额外容器
// 核心思路：快慢指针（Floyd 判圈）两段式
//   第一段 · 判有没有环：slow 每次走 1 步，fast 每次走 2 步 ——
//     有环 → 两人都会进环，fast 每轮净追 1 步，必然在环内追上 slow（相遇点不一定是入环点）
//     无环 → fast 先走到链表末尾的 null，循环结束，返回 null
//   第二段 · 找入环点：从 head 出发一个 index1，从相遇点出发一个 index2，**两人每次都走 1 步**，
//     它们第一次相遇的节点就是入环的第一个节点
// 关键点：
//   - ⭐ **循环条件必须是 `fast != null && fast.next != null`，两个 `&&` 都不能少**
//     `fast != null` 保证能读 `fast.next`；`fast.next != null` 保证能读 `fast.next.next`。
//     循环体第一行就要执行 `fast.next.next`，所以进循环前这两层必须都为真。
//   - 🛑 **我踩过的坑（2026-09-30）**：一开始写成 `while (slow != null)`，
//     本地驱动直接抛 `System.NullReferenceException: Object reference not set to an instance of an object.`
//     原因：**跑得快的是 fast，先变成 null 的也是 fast**。slow 走到中点时 fast 早已出界，
//     循环却还在跑 → 下一轮 `fast.next.next` 就是在读 null 的成员。
//     `slow != null` 这个条件**根本不保护循环体里被解引用的那个东西**。
//     ✅ 规律（可以套用到所有双指针题）：**循环条件要判"循环体里第一个被解引用的指针"** ——
//        谁会被 `x.next` / `x.next.next` 读，就判谁；走得快的那个才需要判两层。
//   - 为什么第二段是对的（经典恒等式，记这个就够）：
//     设 head 到入环点距离 = x，入环点到相遇点 = y，相遇点绕回入环点 = z，环长 = y + z。
//       相遇时：slow 走了 x + y；fast 走了 x + y + n(y + z)，且 fast 步数是 slow 的 2 倍
//       ⇒ 2(x + y) = x + y + n(y + z) ⇒ **x = (n − 1)(y + z) + z**
//     即：从 head 走 x 步到入环点 = 从相遇点走 z 步绕回入环点（前面多绕的 (n−1) 圈不影响落点）。
//     所以 index1（从 head 走）和 index2（从相遇点走）同时出发、同速前进，必然在入环点相遇。
//   - `if (slow == fast)` 必须放在**两个指针都移动之后**：初始时 slow == fast == head，
//     放在移动之前会在第一步就误判成"相遇"。
//   - 相遇之后才创建 index1 / index2，用的是**当时的 slow**（它就是相遇点，也是环内的一点）。
//   - 本题（142）返回入环的第一个节点；**141 只返回 bool**，签名是 `HasCycle` ——
//     同一个算法，输出不同，别把两道题的文件搞混。
//   - 本地实测（2026-09-30，在 _scratch 工程里真跑过，返回节点**按引用身份**核对）：
//       [3,2,0,-4] 尾接 pos=1（入环点 2）      → 返回节点 val=2，且确为入环点 ✅
//       [1,2] 尾接 pos=0（入环点 1）           → 返回节点 val=1 ✅
//       [1] 尾接 pos=0（自己指自己）           → 返回节点 val=1，无异常 ✅
//       [1,2,3,4,5] 无环                       → null ✅
//       [1] 无环 / 空链表 null                  → null，不抛异常 ✅
//       入环点就是 head（尾接 pos=0）           → 返回 head 本体 ✅
//   - 📌 另一种解法：哈希集合（遍历时把节点塞进 HashSet，第一个重复出现的节点就是入环点）
//     思路更直白、也更容易一遍写对，但**空间 O(n)**；本题的 O(1) 解法就是上面这个快慢指针。

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution {
    public ListNode DetectCycle(ListNode head) {
        // slow 每次 1 步、fast 每次 2 步
        ListNode fast = head;
        ListNode slow = head;

        // ⭐ 判的是 fast 的两层：它跑得快、先出界，而且循环体第一行就要读 fast.next.next
        while(fast != null && fast.next != null){
            slow = slow.next;
            fast = fast.next.next;

            // 相遇 = 有环。必须放在两个指针都移动之后（初始时两者都是 head，会误判）
            if(slow == fast){
                // 第二段：一个从 head 出发，一个从相遇点出发，同速前进 → 相遇处就是入环点
                // （推导见文件头「关键点」里的恒等式 x = (n−1)(y+z) + z）
                ListNode index1 = head;
                ListNode index2 = slow;
                while(index1 != index2){
                    index1 = index1.next;
                    index2 = index2.next;
                }
                return index1;
            }
        }

        // fast 走到末尾（无环）→ 没有环
        return null;
    }
}
