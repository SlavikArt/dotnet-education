namespace NumberConsoleGraphics
{
    class NumberPattern
    {
        private string[][] numbers;

        public NumberPattern()
        {
            string[] zero =
            {
                    "  *****  ",
                    " *     * ",
                    " *     * ",
                    " *     * ",
                    "  *****  "
                };

            string[] one =
            {
                    "   *   ",
                    "  **   ",
                    "   *   ",
                    "   *   ",
                    " ***** "
                };

            string[] two =
            {
                    " ***** ",
                    "     * ",
                    " ***** ",
                    " *     ",
                    " ***** "
                };

            string[] three =
            {
                    " ***** ",
                    "     * ",
                    " ***** ",
                    "     * ",
                    " ***** "
                };

            string[] four =
            {
                    " *   * ",
                    " *   * ",
                    " ***** ",
                    "     * ",
                    "     * "
                };

            string[] five =
            {
                    " ***** ",
                    " *     ",
                    " ***** ",
                    "     * ",
                    " ***** "
                };

            string[] six =
            {
                    " ***** ",
                    " *     ",
                    " ***** ",
                    " *   * ",
                    " ***** "
                };

            string[] seven =
            {
                    " ***** ",
                    "     * ",
                    "    *  ",
                    "   *   ",
                    "  *    "
                };

            string[] eight =
            {
                    " ***** ",
                    " *   * ",
                    " ***** ",
                    " *   * ",
                    " ***** "
                };

            string[] nine =
            {
                    " ***** ",
                    " *   * ",
                    " ***** ",
                    "     * ",
                    " ***** "
                };

            this.numbers = new string[][]
            {
                    zero, one, two, three, four,
                    five, six, seven, eight, nine
            };
        }
        public string[] this[int index]
        {
            get => numbers[index];
        }
    }
}
