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
        public Point Point { get {  return new Point(X, Y); } }
        public Section? Section { get; set; }
        public int nodeNumber = 1;
        public string description { get; set; }

        public int HierarchyNumber { get; set; } = 0;

        public bool isRightNode { get; set; }
        public bool isLeftNode { get; set; }
        public bool isBottomNode { get; set; }
        public bool isTopNode { get; set; }
        


        public Node(int x, int y, Section section)
        {
            X = x;
            Y = y;
            Section = section;
            
        }
        public Node(int x, int y)
        {
            X = x;
            Y = y;
            

        }
        public Node(int x, int y, Section section, bool isTop, bool isRight)
        {
            X = x;
            Y = y;
            Section = section;
            this.isTopNode = isTop;
            this.isRightNode = isRight;

        }
        public void InsertNodeAfter(int x, int y, Section section)
        {
            // Create a new node to insert
            Node newNode = new Node(x, y, section);
            newNode.nodeNumber = nodeNumber++;
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
       
        private bool _isRightNode;
        private bool _isLeftNode;
        private bool _isTopNode;
        private bool _isBottomNode;

        public bool IsRightNode {
            get => _isRightNode;
            set {
                if (value) {
                    _isRightNode = true;
                    _isLeftNode = false; 
                }
                else {
                    _isRightNode = false;
                }
            }
        }

        public bool IsLeftNode   {
            get => _isLeftNode;
            set {
                if (value) {
                    _isLeftNode = true;
                    _isRightNode = false; 
                }
                else  {
                    _isLeftNode = false;
                }
            }
        }

        public bool IsTopNode { 
            get => _isTopNode;
            set  {
                if (value) {
                    _isTopNode = true;
                    _isBottomNode = false; 
                }
                else {
                    _isTopNode = false;
                }
            }
        }

        public bool IsBottomNode
        {
            get => _isBottomNode;
            set  {
                if (value) {
                    _isBottomNode = true;
                    _isTopNode = false; 
                }
                else {
                    _isBottomNode = false;
                }
            }
        }

           
        

    }
}
