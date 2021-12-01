namespace MyLibs.CommandLine
{
    /// <summary>
    /// A menu selection for <see cref="CommandLineUtilities.Menu(System.Collections.Generic.HashSet{MenuItem}, string, System.ConsoleColor)"/>
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// The order in which to be displayed in the menu. Duplicate order values appear in an unspecified order.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// The character to be pressed for the menu item.
        /// </summary>
        public char Hotkey { get; }

        /// <summary>
        /// The text for the menu item.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Initialize a new MenuItem/
        /// </summary>
        /// <param name="order">The order in which to be displayed in the menu. Duplicate order values appear in an unspecified order.</param>
        /// <param name="hotkey">The character to be pressed for the menu item.</param>
        /// <param name="text">The text for the menu item.</param>
        public MenuItem(int order, char hotkey, string text)
        {
            Order = order;
            Hotkey = char.ToLower(hotkey);
            Text = text;
        }

        public override string ToString() => $"{Order}|{Hotkey}|{Text}";

        /// <summary>
        /// The hash for the menu item, which is the hash for the Hotkey character.
        /// </summary>
        /// <returns>The hash for the Hotkey character.</returns>
        public override int GetHashCode() => Hotkey.GetHashCode();

        public override bool Equals(object obj) => obj != null && obj is MenuItem item && item.Hotkey == Hotkey;
    }
}
