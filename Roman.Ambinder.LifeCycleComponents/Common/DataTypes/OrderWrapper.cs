namespace Roman.Ambinder.LifeCycleComponents.Common.DataTypes
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "U2U1004:Public value types should implement equality", Justification = "<Pending>")]
    public readonly struct OrderWrapper<T>
    {
        public OrderWrapper(T value, int order = 0)
        {
            Value = value;
            Order = order;
        }

        public static implicit operator T(in OrderWrapper<T> wrapper) => wrapper.Value;

        public T Value { get; }
        public int Order { get; }
    }
}