using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Node Parent { get; set; }
        public Node Child { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void InsertNodeAfter(int x, int y)
        {
            // Create a new node to insert
            Node newNode = new Node(x, y);

            // Update relationships
            newNode.Parent = this;
            newNode.Child = Child;
            if (Child != null)
            {
                Child.Parent = newNode;
            }
            Child = newNode;
        }
        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString();
        }
    }
}
