# LeetCode-Solutions

C# 刷题记录，持续更新中。跟的是[代码随想录](https://programmercarl.com/)的顺序。

> **⚠️ 题号会撞车**
> 本仓库同时收录 **LeetCode** 和 **卡码网 (KamaCoder)** 两个平台的题。
> 卡码网 58「区间和」和 LeetCode 58「Length of Last Word」是完全不相干的两道题。
> 所以**卡码网的题目文件名一律以 `KamaCoder_` 开头**，一眼就能看出是哪个平台。

## 刷题进度

| 日期 | 平台 | 题号 | 题目 | 难度 | 类型 | 备注 |
|------|------|------|------|------|------|------|
| 2026-09-07 | LeetCode | 704 | 二分查找 | Easy | 数组 | 左闭右闭写法 |
| 2026-09-07 | LeetCode | 27 | 移除元素 | Easy | 数组 | 快慢双指针 |
| 2026-09-07 | LeetCode | 977 | 有序数组的平方 | Easy | 数组 | 双向双指针，从两端往中间 |
| 2026-09-08 | LeetCode | 209 | 长度最小的子数组 | Medium | 数组 | 滑动窗口 |
| 2026-09-08 | LeetCode | 59 | 螺旋矩阵II | Medium | 数组 | 模拟，四边界缩圈 |
| 2026-09-09 | KamaCoder | 58 | 区间和 | Easy | 数组 | 前缀和，预处理一次后查询 O(1) |
| 2026-09-09 | KamaCoder | 44 | 开发商购买土地 | Easy | 数组 | 前缀和进阶，横切+竖切 |
| 2026-09-17 | LeetCode | 203 | 移除链表元素 | Easy | 链表 | 哨兵节点+临时指针，res 不动 temp 动 |
| 2026-09-23 | LeetCode | 707 | 设计链表 | Medium | 链表 | 哨兵节点+size 计数，插入先连后连前 |

## 分类目录

### 数组
- [704. 二分查找](./01_Array/0704_BinarySearch.cs) · LeetCode
- [27. 移除元素](./01_Array/0027_RemoveElement.cs) · LeetCode
- [977. 有序数组的平方](./01_Array/0977_SquaresOfASortedArray.cs) · LeetCode
- [209. 长度最小的子数组](./01_Array/0209_MinimumSizeSubarraySum.cs) · LeetCode
- [59. 螺旋矩阵II](./01_Array/0059_SpiralMatrixII.cs) · LeetCode
- [58. 区间和](./01_Array/KamaCoder_0058_RangeSum.cs) · 卡码网
- [44. 开发商购买土地](./01_Array/KamaCoder_0044_DeveloperLandPurchase.cs) · 卡码网

### 链表
- [203. 移除链表元素](./02_LinkedList/0203_RemoveLinkedListElements.cs) · LeetCode
- [707. 设计链表](./02_LinkedList/0707_DesignLinkedList.cs) · LeetCode

## 命名约定

| 文件名形式 | 含义 |
|---|---|
| `NNNN_题目名.cs` | LeetCode 的题，`NNNN` 是 LeetCode 题号（4 位补零） |
| `KamaCoder_NNNN_题目名.cs` | 卡码网的题，`NNNN` 是题目页上显示的题号 |

每份 `.cs` 开头统一 6 行：题号 / 原题链接 / 时间复杂度 / 空间复杂度 / 核心思路 / 关键点。

## 本地怎么跑

LeetCode 的题只给 `Solution` 类、没有 `Main`，本地验证要自己写个驱动：

```csharp
var s = new Solution();
Console.WriteLine(s.Search(new[] { -1, 0, 3, 5, 9, 12 }, 9));   // 输出 4
```

卡码网的题自带 `Main`（读 stdin、写 stdout），`dotnet run` 之后粘贴输入即可。

两个已知的平台预置依赖：

- `0203` 里的 `ListNode` 是 LeetCode 预置的类型，本地跑需要自己补一份定义
- `0044` 的 `Main` 是 `private`（`static void Main` 无修饰符），反射调用才拿得到

本地临时工程建议放在 `_scratch/` 下（已在 `.gitignore` 里排除），别把工程文件提交上来。

## License

[MIT](./LICENSE)
