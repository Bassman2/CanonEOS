//namespace CanonEos.Internal;

//internal class CastObservableCollection<T1, T2> : ObservableCollection<T1>
//{
//    public CastObservableCollection()
//    { }

//    public CastObservableCollection(IList list) 
//    { 
//        foreach (var item in list)
//        {
//            Type type = typeof(T1);
//            ConstructorInfo? ctor = type.GetConstructor(new[] { typeof(T2) });
//            T1? instance = (T1?)ctor?.Invoke(new object[] { item });
//            Add(instance);
//        }
//    }
//}
