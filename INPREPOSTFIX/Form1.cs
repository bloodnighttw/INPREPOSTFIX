using System;
using System.Windows.Forms;

namespace INPREPOSTFIX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BinaryTree _top;
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            var value = Convert.ToInt32(textBox1.Text);
            if (_top == null)
                _top = new BinaryTree(value);
            else
                _top.Add(value);

            textBox2.Text = _top.ShowInfix();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var value = Convert.ToInt32(textBox1.Text);
            var topParent = new BinaryTree(Int32.MaxValue);
            topParent.Add(_top);
            topParent.Remove(value);
            _top = topParent.Left;
            textBox2.Text = _top.ShowInfix();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = _top.ShowInfix();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = _top.ShowPrefix();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = _top.ShowPostfix();
        }
    }

    class BinaryTree
    {
        private int _value;
        internal BinaryTree Left;
        private BinaryTree _right;
        
        public BinaryTree(int value)
        {
            _value = value;
        }

        public void Add(BinaryTree t)
        {
            Left = t;
        }

        public void Add(int value)
        {
            if(value == _value)
                return;
            
            if (value > _value)
            {
                if (_right == null)
                    _right = new BinaryTree(value);
                else 
                    _right.Add(value);
            }
            else
            {
                if (Left == null)
                    Left = new BinaryTree(value);
                else 
                    Left.Add(value);
            }
        }

        private static int Max(BinaryTree tree)
        {
            while (true)
            {
                if (tree._right == null) return tree._value;
                tree = tree._right;
            }
        }

        public void Remove(int target)
        {
            BinaryTree targgetBinaryTree = null;
            var tag = 0;//0 nothing 1 left 2 right  
            if (Left?._value == target)
            {
                targgetBinaryTree = Left;
                tag = 1;
            }
            else if (_right?._value == target)
            {
                targgetBinaryTree = _right;
                tag = 2;
            }
            else if(target > _value)
                _right?.Remove(target);
            else 
                Left?.Remove(target);
            

            if (targgetBinaryTree?.Left != null && targgetBinaryTree?._right != null)
            {
                var max = Max(targgetBinaryTree?.Left);
                targgetBinaryTree?.Remove(max);
                var temp = new BinaryTree(max)
                {
                    Left = targgetBinaryTree?.Left,
                    _right = targgetBinaryTree?._right
                };

                targgetBinaryTree = temp;

            }else if (targgetBinaryTree?.Left != null && targgetBinaryTree?._right == null)
            {
                targgetBinaryTree = targgetBinaryTree?.Left;
            }else if (targgetBinaryTree?.Left == null && targgetBinaryTree?._right != null)
            {
                targgetBinaryTree = targgetBinaryTree?._right;
            }
            else
            {
                targgetBinaryTree = null;
            }

            switch (tag)
            {
                case 1:
                    Left = targgetBinaryTree;
                    break;
                case 2:
                    _right = targgetBinaryTree;
                    break;
            }

        }

        public string ShowInfix()
        {
            return (Left?.ShowInfix() ?? "") + " " + _value  + " " + (_right?.ShowInfix() ?? "");
        }
        
        public string ShowPrefix()
        {
            return _value + " " + (Left?.ShowPrefix() ?? "")  + " " + (_right?.ShowPrefix() ?? "");
        }
        
        public string ShowPostfix()
        {
            return   (Left?.ShowPostfix() ?? "") + " " + (_right?.ShowPostfix() ?? "") + " " + _value ;
        }
    }
    
    
}