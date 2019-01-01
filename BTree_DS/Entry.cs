namespace BTree
{
    using System;

    public class Entry<keyType, pointerType> : IEquatable<Entry<keyType, pointerType>>
    {
        public keyType Key { get; set; }

        public pointerType Pointer { get; set; }

        public bool Equals(Entry<keyType, pointerType> other)
        {
            return this.Key.Equals(other.Key) && this.Pointer.Equals(other.Pointer);
        }
    }
}
