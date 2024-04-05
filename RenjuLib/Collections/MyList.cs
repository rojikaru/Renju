namespace RenjuLib.Collections;

public class MyList<T> : IList<T>
{
    private T[] _items;
    private int _capacity;

    public MyList()
        : this(4)
    {
    }

    public MyList(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _capacity = capacity;
        _items = new T[capacity];

        Count = 0;
    }

    public MyList(IEnumerable<T> collection)
        : this(4)
    {
        foreach (var item in collection)
            Add(item);
    }

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void Resize()
    {
        if (Count < _capacity)
            return;

        _capacity *= 2;
        var newItems = new T[_capacity];
        _items.CopyTo(newItems, 0);
        _items = newItems;
    }

    public void Add(T item)
    {
        Resize();

        _items[Count] = item;
        Count++;
        
        Resize();
    }

    public void Clear()
    {
        _items = new T[_capacity];
        Count = 0;
    }

    public bool Contains(T item) => _items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex)
        => Array.Copy(_items, 0, array, arrayIndex, Count);

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index == -1)
            return false;
        
        int size = Count;
        RemoveAt(index);
        return size != Count;
    }

    public int Count { get; private set; }
    public bool IsReadOnly => false;

    public int IndexOf(T item)
    {
        if (item is null)
            return -1;
        
        for (var i = 0; i < Count; i++)
            if (item.Equals(_items[i]))
                return i;
        
        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Resize();

        for (var i = Count; i > index; i--)
            _items[i] = _items[i - 1];
        
        _items[index] = item;
        Count++;
        
        Resize();
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        for (var i = index; i < Count - 1; i++)
            _items[i] = _items[i + 1];
        
        Count--;
        Resize();
    }

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
}
