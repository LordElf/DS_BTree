namespace BTree
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;

    /// <typeparam name="keyType">Type of BTree key.</typeparam>
    /// <typeparam name="pointerType">Type of BTree pointer associated with each key.</typeparam>
    public class BTree<keyType, pointerType> where keyType : IComparable<keyType>
    {
        public BTree(int degree)
        {
            if (degree < 2)
            {
                throw new ArgumentException("BTree degree must be at least 2", "degree");
            }

            root = new Node<keyType, pointerType>(degree);
            Degree = degree;
            Height = 1;
        }

        public Node<keyType, pointerType> root { get; private set; }

        public int Degree { get; private set; }

        public int Height { get; private set; }

        /// <summary>
        /// Searches a key in the BTree, returning the entry with it and with the pointer.
        /// </summary>
        /// <param name="key">key being searched.</param>
        /// <returns>Entry for that key, null otherwise.</returns>
        public Entry<keyType, pointerType> Search(keyType key, ref int index, ref Node<keyType, pointerType> refNode)
        {
            refNode = root;
            return SearchInternal(ref refNode, key, ref index);
        }

        /// <summary>
        /// Helper method that search for a key in a given BTree.
        /// </summary>
        /// <param name="node">Node used to start the search.</param>
        /// <param name="key">key to be searched.</param>
        /// <returns>Entry object with key information if found, null otherwise.</returns>
        private Entry<keyType, pointerType> SearchInternal(ref Node<keyType, pointerType> node, keyType key, ref int index) 
        {
            index = node.entries.TakeWhile(entry => key.CompareTo(entry.key) > 0).Count();

            if (index < node.entries.Count && node.entries[index].key.CompareTo(key) == 0)
            {
                return node.entries[index];
            }
            if (!node.IsLeaf)
            {
                node = node.Children[index];
                return SearchInternal(ref node, key, ref index);
            }
            else return null;
        }

        /// <summary>
        /// Inserts a new key associated with a pointer in the BTree. This
        /// operation splits nodes as required to keep the BTree properties.
        /// </summary>
        /// <param name="newkey">Key to be inserted.</param>
        /// <param name="newpointer">Pointer to be associated with inserted key.</param>
        public bool Insert(keyType newkey, pointerType newpointer)
        {
            int index = 0;
            Node<keyType, pointerType> buffer = null;
            if (Search(newkey, ref index, ref buffer) == null)
            {
                //需要在InsertNonFull中再次查找来确认位置//need to reidentify the position of node in InsertNonFull
                //buffer的改变不会改变其指向的对象//the change of buffer cannot change the object it refers to
                if (!root.HasReachedMaxentries)
                {
                    InsertNonFull(root, newkey, newpointer);
                    return true;
                }

                Node<keyType, pointerType> buffer2 = root;
                root = new Node<keyType, pointerType>(Degree);
                root.Children.Add(buffer2);
                SplitChild(root, 0, buffer2);
                InsertNonFull(root, newkey, newpointer);

                Height++;
                return true;
            }
            else return false;
        }

        private void InsertNonFull(Node<keyType, pointerType> node, keyType newKey, pointerType newPointer)
        {
            int positionToInsert = node.entries.TakeWhile(entry => newKey.CompareTo(entry.key) >= 0).Count();

            // leaf node
            if (node.IsLeaf)
            {
                node.entries.Insert(positionToInsert, new Entry<keyType, pointerType>() { key = newKey, pointer = newPointer });
                return;
            }

            // non-leaf
            Node<keyType, pointerType> child = node.Children[positionToInsert];
            if (child.HasReachedMaxentries)
            {
                SplitChild(node, positionToInsert, child);
                if (newKey.CompareTo(node.entries[positionToInsert].key) > 0)
                {
                    positionToInsert++;
                }
            }

            InsertNonFull(node.Children[positionToInsert], newKey, newPointer);
        }

        /// <summary>
        /// Helper method that splits a full node into two nodes.
        /// </summary>
        /// <param name="parentNode">Parent node that contains node to be split.</param>
        /// <param name="nodeToBeSplitIndex">Index of the node to be split within parent.</param>
        /// <param name="nodeToBeSplit">Node to be split.</param>
        private void SplitChild(Node<keyType, pointerType> parentNode, int nodeToBeSplitIndex, Node<keyType, pointerType> nodeToBeSplit)
        {
            var newNode = new Node<keyType, pointerType>(Degree);

            parentNode.entries.Insert(nodeToBeSplitIndex, nodeToBeSplit.entries[Degree - 1]);
            parentNode.Children.Insert(nodeToBeSplitIndex + 1, newNode);

            newNode.entries.AddRange(nodeToBeSplit.entries.GetRange(Degree, Degree - 1));

            // remove also entries[Degree - 1], which is the one to move up to the parent
            nodeToBeSplit.entries.RemoveRange(Degree - 1, Degree);

            if (!nodeToBeSplit.IsLeaf)
            {
                newNode.Children.AddRange(nodeToBeSplit.Children.GetRange(Degree, Degree));
                nodeToBeSplit.Children.RemoveRange(Degree, Degree);
            }
        }



        /// <summary>
        /// Deletes a key from the BTree. This operations moves keys and nodes
        /// as required to keep the BTree properties.
        /// </summary>
        /// <param name="keyToDelete">key to be deleted.</param>
        public bool Delete(keyType keyToDelete)
        {
            int index = 0;
            Node<keyType,pointerType> buffer = null;
            if (Search(keyToDelete, ref index, ref buffer) == null)
                return false;
            DeleteInternal(buffer, keyToDelete, index);

            // if root's last entry was moved to a child node, remove it
            if (root.entries.Count == 0 && !root.IsLeaf)
            {
                root = root.Children.Single();
                Height--;
            }
            return true;
        }

        /// <summary>
        /// Internal method to delete keys from the BTree
        /// </summary>
        /// <param name="node">Node to use to start search for the key.</param>
        /// <param name="keyToDelete">key to be deleted.</param>
        private void DeleteInternal(Node<keyType, pointerType> node, keyType keyToDelete, int index)
        {

            int i = node.entries.TakeWhile(entry => keyToDelete.CompareTo(entry.key) > 0).Count();

            // found key in node, so delete if from it
            if (i < node.entries.Count && node.entries[i].key.CompareTo(keyToDelete) == 0)
            {
                DeletekeyFromNode(node, keyToDelete, i);
                return;
            }

            // delete key from subtree
            if (!node.IsLeaf)
            {
                DeletekeyFromSubtree(node, keyToDelete, i);
            }
        }

        /// <summary>
        /// Helper method that deletes a key from a subtree.
        /// </summary>
        /// <param name="parentNode">Parent node used to start search for the key.</param>
        /// <param name="keyToDelete">key to be deleted.</param>
        /// <param name="subtreeIndexInNode">Index of subtree node in the parent node.</param>
        private void DeletekeyFromSubtree(Node<keyType, pointerType> parentNode, keyType keyToDelete, int subtreeIndexInNode)
        {
            Node<keyType, pointerType> childNode = parentNode.Children[subtreeIndexInNode];

            // node has reached min # of entries, and removing any from it will break the btree property,
            // so block makes sure that the "child" has at least "degree" # of nodes by moving an 
            // entry from a sibling node or merging nodes
            if (childNode.HasReachedMinentries && !childNode.HasReachedMaxentries)
            {
                int leftIndex = subtreeIndexInNode - 1;
                Node<keyType, pointerType> leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] : null;

                int rightIndex = subtreeIndexInNode + 1;
                Node<keyType, pointerType> rightSibling = subtreeIndexInNode < parentNode.Children.Count - 1
                                                ? parentNode.Children[rightIndex]
                                                : null;
                
                if (leftSibling != null && leftSibling.entries.Count > Degree - 1)
                {
                    // left sibling has a node to spare, so moves one node from left sibling 
                    // into parent's node and one node from parent into current node ("child")
                    childNode.entries.Insert(0, parentNode.entries[subtreeIndexInNode]);
                    parentNode.entries[subtreeIndexInNode] = leftSibling.entries.Last();
                    leftSibling.entries.RemoveAt(leftSibling.entries.Count - 1);

                    if (!leftSibling.IsLeaf)
                    {
                        childNode.Children.Insert(0, leftSibling.Children.Last());
                        leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
                    }
                }
                else if (rightSibling != null && rightSibling.entries.Count > Degree - 1)
                {
                    // right sibling has a node to spare, so moves one node from right sibling 
                    // into parent's node and one node from parent into current node ("child")
                    childNode.entries.Add(parentNode.entries[subtreeIndexInNode]);
                    parentNode.entries[subtreeIndexInNode] = rightSibling.entries.First();
                    rightSibling.entries.RemoveAt(0);

                    if (!rightSibling.IsLeaf)
                    {
                        childNode.Children.Add(rightSibling.Children.First());
                        rightSibling.Children.RemoveAt(0);
                    }
                }
                else
                {
                    // block merges either left or right sibling into the current node "child"
                    if (leftSibling != null)
                    {
                        childNode.entries.Insert(0, parentNode.entries[subtreeIndexInNode]);
                        var oldentries = childNode.entries;
                        childNode.entries = leftSibling.entries;
                        childNode.entries.AddRange(oldentries);
                        if (!leftSibling.IsLeaf)
                        {
                            var oldChildren = childNode.Children;
                            childNode.Children = leftSibling.Children;
                            childNode.Children.AddRange(oldChildren);
                        }

                        parentNode.Children.RemoveAt(leftIndex);
                        parentNode.entries.RemoveAt(subtreeIndexInNode);
                    }
                    else
                    {
                        Debug.Assert(rightSibling != null, "Node should have at least one sibling");
                        childNode.entries.Add(parentNode.entries[subtreeIndexInNode]);
                        childNode.entries.AddRange(rightSibling.entries);
                        if (!rightSibling.IsLeaf)
                        {
                            childNode.Children.AddRange(rightSibling.Children);
                        }

                        parentNode.Children.RemoveAt(rightIndex);
                        parentNode.entries.RemoveAt(subtreeIndexInNode);
                    }
                }
            }

            // at point, we know that "child" has at least "degree" nodes, so we can
            // move on - guarantees that if any node needs to be removed from it to
            // guarantee BTree's property, we will be fine with that
            DeleteInternal(childNode, keyToDelete, subtreeIndexInNode);
        }
        
        /// <summary>
        /// Helper method that deletes key from a node that contains it, be this
        /// node a leaf node or an internal node.
        /// </summary>
        /// <param name="node">Node that contains the key.</param>
        /// <param name="keyToDelete">key to be deleted.</param>
        /// <param name="keyIndexInNode">Index of key within the node.</param>
        private void DeletekeyFromNode(Node<keyType, pointerType> node, keyType keyToDelete, int keyIndexInNode)
        {
            // if leaf, just remove it from the list of entries (we're guaranteed to have
            // at least "degree" # of entries, to BTree property is maintained
            if (node.IsLeaf)
            {
                node.entries.RemoveAt(keyIndexInNode);
                return;
            }

            Node<keyType, pointerType> predecessorChild = node.Children[keyIndexInNode];
            if (predecessorChild.entries.Count >= Degree)
            {
                Entry<keyType, pointerType> predecessor = DeletePredecessor(predecessorChild);
                node.entries[keyIndexInNode] = predecessor;
            }
            else
            {
                Node<keyType, pointerType> successorChild = node.Children[keyIndexInNode + 1];
                if (successorChild.entries.Count >= Degree)
                {
                    Entry<keyType, pointerType> successor = DeleteSuccessor(predecessorChild);
                    node.entries[keyIndexInNode] = successor;
                }
                else
                {
                    predecessorChild.entries.Add(node.entries[keyIndexInNode]);
                    predecessorChild.entries.AddRange(successorChild.entries);
                    predecessorChild.Children.AddRange(successorChild.Children);

                    node.entries.RemoveAt(keyIndexInNode);
                    node.Children.RemoveAt(keyIndexInNode + 1);

                    //DeleteInternal(predecessorChild, keyToDelete);
                }
            }
        }

        /// <summary>
        /// Helper method that deletes a predecessor key (i.e. rightmost key) for a given node.
        /// </summary>
        /// <param name="node">Node for which the predecessor will be deleted.</param>
        /// <returns>Predecessor entry that got deleted.</returns>
        private Entry<keyType, pointerType> DeletePredecessor(Node<keyType, pointerType> node)
        {
            if (node.IsLeaf)
            {
                var result = node.entries[node.entries.Count - 1];
                node.entries.RemoveAt(node.entries.Count - 1);
                return result;
            }

            return DeletePredecessor(node.Children.Last());
        }

        /// <summary>
        /// Helper method that deletes a successor key (i.e. leftmost key) for a given node.
        /// </summary>
        /// <param name="node">Node for which the successor will be deleted.</param>
        /// <returns>Successor entry that got deleted.</returns>
        private Entry<keyType, pointerType> DeleteSuccessor(Node<keyType, pointerType> node)
        {
            if (node.IsLeaf)
            {
                var result = node.entries[0];
                node.entries.RemoveAt(0);
                return result;
            }

            return DeletePredecessor(node.Children.First());
        }

        

        
        
        public void print(TextBox textBox)
        {
            if (root.entries.Count > 0)
                print(root, "", root.entries.Count <= 1, textBox);
            else
                textBox.AppendText("error");
        }
        private void print(Node<keyType, pointerType> node, string prefix, bool isTail, TextBox textBox)
        {

            if (!node.IsLeaf)
            {
                for (int index = 0; index < node.Children.Count; ++index)
                {
                    print(node.Children[index], prefix + (isTail ? "     " : "│    "), isTail, textBox);//"┌──"
                    if (index < node.entries.Count)
                    {
                        textBox.AppendText(prefix + (isTail ? "└──" : "├──") + (node.entries[index].key) + '\n');
                    }
                }
            }
            else
            {
                for (int i = 0; i < node.entries.Count; ++i)
                {
                    if (0 == i)
                    {
                        textBox.AppendText(prefix + "┌──" + (node.entries[i].key) + '\n');
                    }
                    else
                    {
                        textBox.AppendText(prefix + ((i == node.entries.Count - 1) ? "└──" : "│    ") + (node.entries[i].key) + '\n');
                    }
                }
            }
        }
    }
}
