public class Node
{
    public int Data { get; set; }
    public bool IsBlack { get; set; }
    public Node Parent { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(int data, bool isBlack, Node parent, Node left, Node right)
    {
        Data = data;
        IsBlack = isBlack;
        Parent = parent;
        Left = left;
        Right = right;
    }
}

public class RedBlackTree
{
    private Node root;
    public RedBlackTree() { root = null; }
    public Node Search(int data)
    {
        Node node = root;
        while (node != null)
        {
            if (data < node.Data)
                node = node.Left;
            else if (data > node.Data)
                node = node.Right;
            else
                return node;
        }
        return null;
    }

    public void Insert(int data)
    {
        Node newNode = new Node(data, false, null, null, null);
        if (root == null)
        {
            root = newNode;
            root.IsBlack = true;
        }
        else
        {
            Node currentNode = root;
            while (true)
            {
                if (data < currentNode.Data)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = newNode;
                        break;
                    }
                    currentNode = currentNode.Left;
                }
                else
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = newNode;
                        break;
                    }
                    currentNode = currentNode.Right;
                }
            }
            newNode.Parent = currentNode;
            BalanceTree(newNode);
        }
    }

    private void BalanceTree(Node newNode)
    {
        Node uncle;
        while (newNode != root && newNode.Parent.IsBlack == false)
        {
            if (newNode.Parent == newNode.Parent.Parent.Right)
            {
                uncle = newNode.Parent.Parent.Left;
                if (uncle != null && uncle.IsBlack == false)
                {
                    newNode.Parent.IsBlack = true;
                    uncle.IsBlack = true;
                    newNode.Parent.Parent.IsBlack = false;
                    newNode = newNode.Parent.Parent;
                }
                else
                {
                    if (newNode == newNode.Parent.Left)
                    {
                        newNode = newNode.Parent;
                        RotateRight(newNode);
                    }
                    newNode.Parent.IsBlack = true;
                    newNode.Parent.Parent.IsBlack = false;
                    RotateLeft(newNode.Parent.Parent);
                }
            }
            else
            {
                uncle = newNode.Parent.Parent.Right;

                if (uncle != null && uncle.IsBlack == false)
                {
                    newNode.Parent.IsBlack = true;
                    uncle.IsBlack = true;
                    newNode.Parent.Parent.IsBlack = false;
                    newNode = newNode.Parent.Parent;
                }
                else
                {
                    if (newNode == newNode.Parent.Right)
                    {
                        newNode = newNode.Parent;
                        RotateLeft(newNode);
                    }
                    newNode.Parent.IsBlack = true;
                    newNode.Parent.Parent.IsBlack = false;
                    RotateRight(newNode.Parent.Parent);
                }
            }
        }
        root.IsBlack = true;
    }

    private void RotateLeft(Node node)
    {
        Node rightChild = node.Right;
        node.Right = rightChild.Left;
        if (node.Right != null)
            node.Right.Parent = node;
        rightChild.Parent = node.Parent;

        if (node.Parent == null)
            root = rightChild;
        else if (node == node.Parent.Left)
            node.Parent.Left = rightChild;
        else
            node.Parent.Right = rightChild;
        rightChild.Left = node;
        node.Parent = rightChild;
    }

    private void RotateRight(Node node)
    {
        Node leftChild = node.Left;
        node.Left = leftChild.Right;
        if (node.Left != null)
            node.Left.Parent = node;
        leftChild.Parent = node.Parent;

        if (node.Parent == null)
            root = leftChild;
        else if (node == node.Parent.Right)
            node.Parent.Right = leftChild;
        else
            node.Parent.Left = leftChild;
        leftChild.Right = node;
        node.Parent = leftChild;
    }

    public void PrintTree(Node root, string padding = "")
    {
        if (root == null)
        {
            Console.WriteLine(padding + "null");
            return;
        }

        string nodeColor = root.IsBlack ? "B" : "R";
        Console.WriteLine(padding + "(" + root.Data + ", " + nodeColor + ")");

        padding += "  ";

        PrintTree(root.Left, padding);
        PrintTree(root.Right, padding);
    }
    public void Print()
    {
        PrintTree(root);
    }
}

class Program
{
    static void Main(string[] args)
    {
        RedBlackTree rbTree = new RedBlackTree();
        rbTree.Insert(24);
        rbTree.Insert(5);
        rbTree.Insert(1);
        rbTree.Insert(15);
        rbTree.Insert(3);
        rbTree.Insert(8);
        rbTree.Print();

        Console.WriteLine();

        List<int> elementsToFind = new() { 5, 2, 3, 6 };
        foreach (int element in elementsToFind)
        {
            var result = rbTree.Search(element);
            if (result != null)
                Console.WriteLine($"Found! {result.Data} is in the tree.");
            else
                Console.WriteLine($"{element} is not found :(");
        }
    }
}
