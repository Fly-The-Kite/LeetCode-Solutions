// 707. 设计链表
// https://leetcode.cn/problems/design-linked-list/
// 时间复杂度：Get/Add/Delete 均为 O(index)
// 空间复杂度：O(1)（除链表本身存储外）
// 核心思路：哨兵节点（虚拟头节点）+ size 计数
//   - 哨兵节点 list 始终在链表头部，统一处理头节点的插入/删除
//   - count 记录链表中真实节点数量（不含哨兵）
//   - 所有操作都从哨兵开始遍历，找到目标位置的前一个节点再操作
// 关键点：
//   - 插入操作：先连新节点的 next（tmp2.next = tmp1.next），再连前面（tmp1.next = tmp2）
//     顺序不能反，否则会丢失原来的 tmp1.next
//   - 删除操作：tmp1.next = tmp1.next.next，直接跳过目标节点
//   - AddAtHead 和 AddAtTail 都复用 AddAtIndex，避免重复代码
//   - Get 方法从哨兵走 index+1 步，因为哨兵是第 0 个位置，真实节点从第 1 步开始

// 链表节点定义
public class LinkList {
    public int val;        // 节点的值
    public LinkList next;  // 指向下一个节点的引用

    public LinkList(int val) {
        this.val = val;
    }
}

public class MyLinkedList {

    public LinkList list;  // 哨兵节点（虚拟头节点），val 无意义，next 指向真正的头节点
    public int count;      // 链表中真实节点的数量（不含哨兵）

    // 构造函数：初始化哨兵节点，链表为空
    public MyLinkedList() {
        list = new LinkList(0);  // 哨兵节点，val=0 无实际意义
        count = 0;               // 初始没有真实节点
    }

    // 获取第 index 个节点的值，index 从 0 开始
    // 无效 index 返回 -1
    public int Get(int index) {
        // 边界检查：index 超出范围或为负
        if (count <= index || index < 0) {
            return -1;
        }

        LinkList current = list;  // 从哨兵开始
        // 走 index+1 步到达第 index 个真实节点
        // 因为哨兵是第 0 个位置，真实节点从第 1 步开始
        for (int i = 0; i <= index; i++) {
            current = current.next;
        }
        return current.val;
    }

    // 在链表头部插入新节点
    // 复用 AddAtIndex(0, val)
    public void AddAtHead(int val) {
        AddAtIndex(0, val);
    }

    // 在链表尾部追加新节点
    // 复用 AddAtIndex(count, val)，count 就是尾节点之后的位置
    public void AddAtTail(int val) {
        AddAtIndex(count, val);
    }

    // 在第 index 个位置插入新节点
    // 如果 index > count，不插入；如果 index < 0，插在头部
    public void AddAtIndex(int index, int val) {
        // index 超出链表长度，不插入
        if (index > count) return;
        // index 为负，插在头部
        index = Math.Max(0, index);

        count++;  // 节点数+1

        // 找到第 index 个位置的前一个节点（即 index-1 位置）
        LinkList tmp1 = list;
        for (int i = 0; i < index; i++) {
            tmp1 = tmp1.next;
        }

        // 创建新节点
        LinkList tmp2 = new LinkList(val);

        // 插入操作：先连后面，再连前面
        // 顺序不能反！如果先 tmp1.next = tmp2，原来的 tmp1.next 就丢了
        tmp2.next = tmp1.next;   // 新节点的 next 指向原来的第 index 个节点
        tmp1.next = tmp2;        // 前一个节点的 next 指向新节点
    }

    // 删除第 index 个节点
    public void DeleteAtIndex(int index) {
        // 边界检查
        if (index >= count || index < 0) return;

        // 找到第 index 个位置的前一个节点
        LinkList tmp1 = list;
        for (int i = 0; i < index; i++) {
            tmp1 = tmp1.next;
        }

        // 直接跳过目标节点：前一个节点的 next 指向目标节点的 next
        tmp1.next = tmp1.next.next;

        count--;  // 节点数-1
    }
}
