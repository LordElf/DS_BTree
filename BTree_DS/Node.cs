namespace BTree
{
    using System.Collections.Generic;

    public class Node<keyType, pointerType>
    {
        private int degree;

        public Node(int degree)
        {
            this.degree = degree;
            this.Children = new List<Node<keyType, pointerType>>(degree);
            this.Entries = new List<Entry<keyType, pointerType>>(degree);
        }

        public List<Node<keyType, pointerType>> Children { get; set; }

        public List<Entry<keyType, pointerType>> Entries { get; set; }

        public bool IsLeaf
        {
            get
            {
                return Children.Count == 0;
            }
        }

        public bool HasReachedMaxEntries
        {
            get
            {
                return this.Entries.Count == 2*degree - 1;
            }
        }

        public bool HasReachedMinEntries
        {
            get
            {
                return Entries.Count >= degree;
            }
        }
    }
}
