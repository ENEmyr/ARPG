using System;
using System.Collections.Generic;

public class CircularList<T> : List<T>
{
    private int index;

    public CircularList() : this(0) { }

    public CircularList(int idx)
    {
        if (idx < 0)
            throw new Exception("Index must greater than 0");
        index = idx;
    }

    public CircularList(IEnumerable<T> collection)
    {
        this.AddRange(collection);
        index = this.Count;
    }

    public T Current()
    {
        return this[index];
    }

    public T Next()
    {
        index++;
        index %= Count;

        return this[index];
    }

    public T Previous()
    {
        index--;
        if (index < 0)
            index = Count - 1;

        return this[index];
    }

    public void Reset()
    {
        index = 0;
    }

    public void MoveToEnd()
    {
        index = Count - 1;
    }

    public new void Add(T item)
    {
        index++;
        base.Add(item);
    }

    public new void Remove(T item)
    {
        int itemIndex = base.IndexOf(item);
        if (itemIndex == index)
            index = index == 0 ? index : index - 1;
        base.Remove(item);
    }
}
