namespace BTree
{
    using System;

    public class Entry<keyType, pointerType> : IEquatable<Entry<keyType, pointerType>>
    {
        public keyType key { get; set; }

        public pointerType pointer { get; set; }

        public bool Equals(Entry<keyType, pointerType> other)
        {
            return this.key.Equals(other.key) && this.pointer.Equals(other.pointer);
        }
    }
}
