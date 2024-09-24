using System.Collections;

namespace CanonEos;

internal class MergedObservableCollection<T> : ObservableCollection<T> 
{
    public MergedObservableCollection()
    { }

    public MergedObservableCollection(params IList[] colList) : this((IEnumerable<IList>)colList)
    { }

    public MergedObservableCollection(IEnumerable<IList> colList)
    {
        foreach (var col in colList)
        {
            Merge(col);
        }
    }

    public void Merge(IList col)
    {
        foreach (var item in col)
        {
            this.Add((T)item);
        }
        if (col is INotifyCollectionChanged notify)
        {
            notify.CollectionChanged += OnCollectionChanged;
        }
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
        case NotifyCollectionChangedAction.Add:
            foreach (var item in e.NewItems!)
            {
                this.Add((T)item);
            }
            break;
        case NotifyCollectionChangedAction.Remove:
            foreach (var item in e.OldItems!)
            {
                this.Remove((T)item);
            }
            break;
        case NotifyCollectionChangedAction.Replace:
        case NotifyCollectionChangedAction.Move:
        case NotifyCollectionChangedAction.Reset:
            throw new NotImplementedException();
        }
    }
}
