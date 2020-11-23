namespace Roman.Ambinder.LifeCycleComponents.Common.DataTypes
{
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