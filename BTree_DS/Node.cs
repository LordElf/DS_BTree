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
            this.entries = new List<Entry<keyType, pointerType>>(degree);
        }

        public List<Node<keyType, pointerType>> Children { get; set; }

        public List<Entry<keyType, pointerType>> entries { get; set; }

        public bool IsLeaf
        {
            get
            {
                return Children.Count == 0;
            }
        }

        public bool HasReachedMaxentries
        {
            get
            {
                return this.entries.Count == 2*degree - 1;
            }
        }

        public bool HasReachedMinentries
        {
            get
            {
                return entries.Count >= degree;
            }
        }
    }
}
