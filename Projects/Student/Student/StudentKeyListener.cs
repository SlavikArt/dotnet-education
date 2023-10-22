namespace Student
{
    public class StudentKeyListener
    {
        public delegate void KeyHandler(string msg);
        public event KeyHandler OnEnter;
        public event KeyHandler OnSpace;
        public event KeyHandler OnEscape;
        public event KeyHandler OnF1;
        public event KeyHandler OnLeft;
        public event KeyHandler OnRight;
        public event KeyHandler OnUp;
        public event KeyHandler OnDown;

        public void Listen(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Enter:
                    OnEnter?.Invoke("Enter");
                    break;
                case ConsoleKey.Spacebar:
                    OnSpace?.Invoke("Space");
                    break;
                case ConsoleKey.Escape:
                    OnEscape?.Invoke("Escape");
                    break;
                case ConsoleKey.F1:
                    OnF1?.Invoke("F1");
                    break;
                case ConsoleKey.LeftArrow:
                    OnLeft?.Invoke("Left");
                    break;
                case ConsoleKey.RightArrow:
                    OnRight?.Invoke("Right");
                    break;
                case ConsoleKey.UpArrow:
                    OnUp?.Invoke("Up");
                    break;
                case ConsoleKey.DownArrow:
                    OnDown?.Invoke("Down");
                    break;
            }
        }
    }
}
