﻿namespace ClippingAlgorithms.Models.DoubleLinkedLists;

public static class NodeExtensions
{
    // Bug: it does not increment Count of LinkedList
    public static void AddAfter<T>(this Node<T> node, T item)
    {
        var newNode = new Node<T>();
        newNode.Value = item;

        newNode.Next = node.Next;
        newNode.Prev = node;

        node.Next = newNode;
        newNode.Next.Prev = newNode;
    }
}
