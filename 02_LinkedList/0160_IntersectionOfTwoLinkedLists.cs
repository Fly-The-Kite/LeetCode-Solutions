// 160. 相交链表
// https://leetcode.cn/problems/intersection-of-two-linked-lists/
// 时间复杂度：O(m + n) —— 各遍历一遍数长度，再一起走到相交点（最多再走一遍）
// 空间复杂度：O(1) —— 只用两个游标和两个计数，没有额外容器
// 核心思路：长度对齐法（长的那条先走"长度差"步）
//   - 先各走一遍，数出两条链表的长度 countA / countB
//   - 让**长的那条**先走 |countA - countB| 步 —— 这样两个游标离尾部的距离就一样了
//   - 然后两个游标同步前进，**第一次引用相同**的节点就是相交点
//   - 走到头都没碰上 → 不相交，返回 null
// 关键点：
//   - ⭐ **必须用 `curA == curB`（引用相等），不能用 `curA.val == curB.val`**
//     LeetCode 原话：「函数返回结果后，链表必须保持其原始结构」，而且
//     **相交是指"同一个节点对象"，不是"值相等的两个节点"**。
//     用值比较，遇到 [1]、[1] 这种不相交但值相同的用例就会误判。
//   - 为什么"长度差先走"是对的：相交之后两条链是**同一条尾巴**，
//     所以从相交点往回看，两条链剩下的长度必然相等 —— 把长链多出来的那截先走掉，
//     两个游标就站在同一起跑线上，之后可以逐节点对齐比较。
//   - 为什么先数的长度、再重置游标（curA = headA; curB = headB）：
//     数长度时游标已经走到 null 了，必须重置，否则第二次遍历从空开始。
//   - 交换那一段是把"A 长"统一成一种情况，省掉写两遍对称代码。
//   - 循环条件 `while(curA != null)` 就够了：走到这一步两条链的**剩余长度相等**，
//     curA 到头意味着 curB 也到头 → 不相交。所以不需要同时判 curB。
//   - 边界天然安全：任一条为空时 count=0，gap = 另一条的长度，
//     长的那条游标正好走到 null，循环直接不进 → 返回 null，不抛异常。
//   - 本地实测（2026-09-29 跑过，**按引用身份校验**）：
//       官方示例（A=[4,1,8,4,5] B=[5,6,1,8,4,5]，交于 8）      → 交于 8
//       同一对链表 A/B 互换（走"B 更长"那条分支）                 → 交于 8
//       交于最后一个节点（A=[7,3] B=[9,3]）                      → 交于 3
//       不相交 + 长度不同（[2,6,4] / [1,5]）                     → null
//       不相交 + 长度相同（[1,2,3] / [4,5,6]）                   → null
//       两条链共用同一个头（从第一个节点就相交）                    → 返回该头
//       A 为空 / B 为空 / 两条都为空                             → null（不抛异常）
//   - 📌 另一种解法：哈希集合（先把 A 的节点全塞进 HashSet，再遍历 B 找第一个存在的）
//     思路更直白，但**空间是 O(m)**；本题的 O(1) 解法就是上面这个长度对齐法。

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution {
    public ListNode GetIntersectionNode(ListNode headA, ListNode headB) {
        // 第一遍：两条链各走一遍，数长度
        ListNode curA = headA;
        ListNode curB = headB;
        int countA = 0;
        int countB = 0;
        while(curA != null){
            curA = curA.next;
            countA++;
        }
        while(curB != null){
            curB = curB.next;
            countB++;
        }

        // 数长度时游标已经走到 null 了，必须重置回各自的头
        curA = headA;
        curB = headB;

        // 统一成"A 是长的那条"，省掉对称的两份代码：
        // 长度和游标要一起换，只换一个就全错了
        if(countB > countA){
            int temp = countA;
            countA = countB;
            countB = temp;

            ListNode tempList = curA;
            curA = curB;
            curB = tempList;
        }

        // 长的那条先走"长度差"步，让两个游标离尾部一样远
        int gap = countA - countB;
        while(gap-- > 0){
            curA = curA.next;
        }

        // 同步前进，第一次引用相同就是相交点
        // 循环条件只用 curA != null：此时剩余长度相等，A 到头 = B 也到头
        while(curA != null){
            if(curA == curB){
                return curA;
            }
            curA = curA.next;
            curB = curB.next;
        }

        // 走到头都没碰上 → 不相交
        return null;
    }
}
